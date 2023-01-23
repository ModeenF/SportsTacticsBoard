// Sports Tactics Board
//
// http://sportstacticsbd.sourceforge.net/
// http://sourceforge.net/projects/sportstacticsbd/
// 
// Sports Tactics Board is a utility that allows coaches, trainers and 
// officials to describe sports tactics, strategies and positioning using 
// a magnetic or chalk-board style approach.
// 
// Copyright (C) 2021- Fredrik Modéen
// Copyright (C) 2006-2010 Robert Turner
// 
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
using SportsTacticsBoard.Classes;
using SportsTacticsBoard.Resources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace SportsTacticsBoard
{
    public partial class MainForm : Form, ICustomLabelProvider
    {
        private SavedLayoutManager commonSavedLayoutManager;
        private SavedLayoutManager userSavedLayoutManager;
        private LayoutSequence currentSequence;
        private int positionInSequence;
        private string fileName = "";
        private string fileFilter;
        private string saveAsImageFileFilter;
        private string originalCaption;
        private bool sequenceDirty;

        private bool IsDocumentDirty
        {
            get
            {
                return sequenceDirty || fieldControl.IsViewDirty || fieldControl.IsDirty;
            }
        }

        public MainForm()
        {
            commonSavedLayoutManager = new SavedLayoutManager(SavedLayoutManager.CommonLayoutPath);
            userSavedLayoutManager = new SavedLayoutManager(SavedLayoutManager.UserLayoutPath);
            InitializeComponent();

            originalCaption = Text;

            fieldControl.CustomLabelProvider = this;
            fieldControl.IsDirtyChanged += new EventHandler(fieldControl_IsDirtyChanged);
            fieldControl.IsViewDirtyChanged += new EventHandler(fieldControl_IsViewDirtyChanged);
            fieldControl.KeyDown += new KeyEventHandler(fieldControl_KeyDown);

            ResourceManager resourceManager = ResourceManager.GetInstance();
            resourceManager.DefaultCulture = Appsettings.Settings?.DefaultLanguage;

            SetLanguage();
        }

        void fieldControl_IsViewDirtyChanged(object sender, EventArgs e)
        {
            UpdateFileMenuItems();
        }

        private void recordNewPositionButton_Click(object sender, EventArgs e)
        {
            if (null != currentSequence)
            {
                positionInSequence = RecordPositionToSequence(false, positionInSequence + 1, currentSequence);
                fieldControl.SetNextLayout(GetNextLayout());
                UpdateSequenceControls();
            }
        }

        private FieldLayout GetNextLayout()
        {
            return currentSequence.GetLayout(positionInSequence + 1);
        }

        private void fieldControl_IsDirtyChanged(object sender, EventArgs e)
        {
            UpdateSequenceControls();
            UpdateFileMenuItems();
        }

        private void commonSavedLayoutMenuItem_Click(object sender, EventArgs e)
        {
            StopPlayingSequence();
            ToolStripMenuItem mi = (ToolStripMenuItem)sender;
            FieldLayout layout = commonSavedLayoutManager.GetLayoutForMenuItem(mi, SafeGetCurrentFieldTypeTag());
            fieldControl.SetLayout(layout);
        }

        private void userSavedLayoutMenuItem_Click(object sender, EventArgs e)
        {
            StopPlayingSequence();
            ToolStripMenuItem mi = (ToolStripMenuItem)sender;
            FieldLayout layout = userSavedLayoutManager.GetLayoutForMenuItem(mi, SafeGetCurrentFieldTypeTag());
            fieldControl.SetLayout(layout);
        }

        private void UpdateFileMenuItems()
        {
            saveSequenceAsMenuItem.Enabled = (fieldControl.FieldType != null);
            saveSequenceMenuItem.Enabled = (fieldControl.FieldType != null) && (IsDocumentDirty || string.IsNullOrEmpty(fileName));
        }

        private void UpdateLayoutMenuItems()
        {
            saveCurrentLayoutMenuItem.Enabled = (fieldControl.FieldType != null);
        }

        private void UpdateCommonSavedLayoutMenuItems()
        {
            commonSavedLayoutManager.UpdateMenu(commonSavedLayoutsMenuItem, SafeGetCurrentFieldTypeTag(), new EventHandler(commonSavedLayoutMenuItem_Click));
        }

        private void UpdateUserSavedLayoutMenuItems()
        {
            if (userSavedLayoutManager.UpdateMenu(userSavedLayoutsMenuItem, SafeGetCurrentFieldTypeTag(), new EventHandler(userSavedLayoutMenuItem_Click)))
            {
                removeSavedLayoutMenuItem.Enabled = true;
            }
            else
            {
                removeSavedLayoutMenuItem.Enabled = false;
            } // endif
        }

        private string SafeGetCurrentFieldTypeTag()
        {
            if (fieldControl.FieldType == null)
            {
                return null;
            }
            return fieldControl.FieldType.Tag;
        }

        private void SaveCurrentLayout()
        {
            userSavedLayoutManager.SaveCurrentLayout(fieldControl.FieldLayoutAsFieldObjects, SafeGetCurrentFieldTypeTag());
            UpdateUserSavedLayoutMenuItems();
        }

        private void UpdateSequenceControls()
        {
            if (null != currentSequence)
            {
                ResourceManager resourceManager = ResourceManager.GetInstance();
                if (currentSequence.NumberOfLayouts == 0)
                {
                    currentLayoutNumber.Text = resourceManager.LocalizationResource.CurrentLayoutNumberEmpty;
                }
                else
                {
                    string formatString = resourceManager.LocalizationResource.CurrentLayoutNumberFormat;
                    currentLayoutNumber.Text = string.Format(CultureInfo.CurrentUICulture, formatString, positionInSequence + 1, currentSequence.NumberOfLayouts);
                }

                goToFirstToolStripButton.Enabled = (positionInSequence > 0);
                previousLayoutInSequence.Enabled = (positionInSequence > 0);
                nextLayoutInSequence.Enabled = ((positionInSequence + 1) < currentSequence.NumberOfLayouts);
                goToLastToolStripButton.Enabled = ((positionInSequence + 1) < currentSequence.NumberOfLayouts);
                revertCurrentLayoutToSavedButton.Enabled = (!IsPlayingSequence) && (currentSequence.NumberOfLayouts > 0) && (fieldControl.IsDirty);
                removeCurrentPositionFromSequenceButton.Enabled = (!IsPlayingSequence) && (currentSequence.NumberOfLayouts > 1);
                showMovementButton.Checked = fieldControl.ShowMovementLines;
                showMovementButton.Enabled = true;
                recordOverCurrentPositionButton.Enabled = !IsPlayingSequence;
                recordNewPositionButton.Enabled = !IsPlayingSequence;
                playToolStripButton.Enabled = currentSequence.NumberOfLayouts > 1;
            }
            else
            {
                previousLayoutInSequence.Enabled = false;
                nextLayoutInSequence.Enabled = false;
                revertCurrentLayoutToSavedButton.Enabled = false;
                removeCurrentPositionFromSequenceButton.Enabled = false;
                showMovementButton.Checked = false;
                showMovementButton.Enabled = false;
                recordOverCurrentPositionButton.Enabled = false;
                recordNewPositionButton.Enabled = false;
                goToFirstToolStripButton.Enabled = false;
                goToLastToolStripButton.Enabled = false;
                playToolStripButton.Enabled = false;
            }

            if (IsPlayingSequence)
            {
                playToolStripButton.Image = Properties.Resources.PauseHS;
            }
            else
            {
                playToolStripButton.Image = Properties.Resources.PlayHS;
            }
        }

        private void previousLayoutInSequence_Click(object sender, EventArgs e)
        {
            StopPlayingSequence();
            MoveToPreviousLayout();
        }

        private void MoveToPreviousLayout()
        {
            if (null == currentSequence)
            {
                return;
            }

            if (positionInSequence > 0)
            {
                if ((currentSequence.NumberOfLayouts > 0) && (fieldControl.IsDirty))
                {
                    ResourceManager resourceManager = ResourceManager.GetInstance();
                    DialogResult dr = RtlAwareMessageBox.Show(this, resourceManager.LocalizationResource.SaveSequenceEntryBeforeSwitchingEntries, this.Text,
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
                    switch (dr)
                    {
                        case DialogResult.Yes:
                            RecordPositionToSequence(true, positionInSequence, currentSequence);
                            break;
                        case DialogResult.Cancel:
                            return;
                    }
                }
                positionInSequence--;
                RestorePositionFromSequence(positionInSequence, currentSequence, GetNextLayout());
                UpdateSequenceControls();
            }
        }

        private void nextLayoutInSequence_Click(object sender, EventArgs e)
        {
            StopPlayingSequence();
            MoveToNextLayout();
        }

        private void MoveToNextLayout()
        {
            if (null == currentSequence)
            {
                return;
            }

            if (positionInSequence + 1 < currentSequence.NumberOfLayouts)
            {
                if ((currentSequence.NumberOfLayouts > 0) && (fieldControl.IsDirty))
                {
                    ResourceManager resourceManager = ResourceManager.GetInstance();
                    DialogResult dr = RtlAwareMessageBox.Show(this, resourceManager.LocalizationResource.SaveSequenceEntryBeforeSwitchingEntries, this.Text,
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
                    switch (dr)
                    {
                        case DialogResult.Yes:
                            RecordPositionToSequence(true, positionInSequence, currentSequence);
                            break;
                        case DialogResult.Cancel:
                            return;
                    }
                }
                positionInSequence++;
                RestorePositionFromSequence(positionInSequence, currentSequence, GetNextLayout());
                UpdateSequenceControls();
            }
        }

        private void recordOverCurrentPositionButton_Click(object sender, EventArgs e)
        {
            if (null != currentSequence)
            {
                RecordPositionToSequence(true, positionInSequence, currentSequence);
                UpdateSequenceControls();
            }
        }

        private void removeCurrentPositionFromSequenceButton_Click(object sender, EventArgs e)
        {
            if ((null != currentSequence) && (currentSequence.NumberOfLayouts > 0))
            {
                currentSequence.RemoveFromSequence(positionInSequence);
                if (positionInSequence >= currentSequence.NumberOfLayouts)
                {
                    positionInSequence--;
                }
                fieldControl.SetNextLayout(GetNextLayout());
                UpdateSequenceControls();
            }
        }

        private void revertCurrentLayoutToSavedButton_Click(object sender, EventArgs e)
        {
            if (null != currentSequence)
            {
                RestorePositionFromSequence(positionInSequence, currentSequence, GetNextLayout());
                UpdateSequenceControls();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopPlayingSequence();
            Application.Exit();
        }

        private void UpdateCaption()
        {
            string title = originalCaption;
            if (!string.IsNullOrEmpty(fileName))
            {
                ResourceManager resourceManager = ResourceManager.GetInstance();
                title = string.Format(CultureInfo.CurrentCulture, resourceManager.LocalizationResource.TitleFormatString, originalCaption, Path.GetFileName(fileName));
            }
            Text = title;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopPlayingSequence();

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.CheckFileExists = true;
                openFileDialog.Filter = fileFilter;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.SupportMultiDottedExtensions = true;
                openFileDialog.Multiselect = false;
                openFileDialog.InitialDirectory = GetDefaultSequenceSaveLocation(true);

                DialogResult res = openFileDialog.ShowDialog();
                if (res == DialogResult.OK)
                {
                    ResourceManager resourceManager = ResourceManager.GetInstance();
                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(LayoutSequence));
                        using (var fs = XmlReader.Create(new FileStream(openFileDialog.FileName, FileMode.Open)))
                        {
                            LayoutSequence seq = (LayoutSequence)serializer.Deserialize(fs);
                            if (seq != null)
                            {
                                // Locate the field type interface for the field specified by this saved file
                                IPlayingSurfaceType newFieldType = FindFieldType(seq.FieldTypeTag);
                                if (newFieldType == null)
                                {
                                    RtlAwareMessageBox.Show(this, String.Format(CultureInfo.CurrentUICulture, resourceManager.LocalizationResource.FailedToOpenFileFormatStr,
                                        openFileDialog.FileName), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
                                }
                                else
                                {
                                    fieldControl.FieldType = newFieldType;
                                    fieldControl.SetNextLayout(null);
                                    fieldControl.IsDirty = false;
                                    fieldControl.IsViewDirty = false;
                                    positionInSequence = 0;
                                    currentSequence = seq;
                                    sequenceDirty = false;
                                    RestorePositionFromSequence(positionInSequence, currentSequence, GetNextLayout());
                                    fileName = openFileDialog.FileName;
                                    UpdateCaption();
                                    UpdateFileMenuItems();
                                    UpdateLayoutMenuItems();
                                    UpdateCommonSavedLayoutMenuItems();
                                    UpdateUserSavedLayoutMenuItems();
                                    UpdateSequenceControls();
                                    UpdateViewMenuItems();
                                }
                            }
                            else
                            {
                                RtlAwareMessageBox.Show(this, String.Format(CultureInfo.CurrentUICulture, resourceManager.LocalizationResource.FailedToOpenFileFormatStr,
                                    openFileDialog.FileName), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
                            }
                        }
                    }
                    catch (IOException /*exception*/)
                    {
                        RtlAwareMessageBox.Show(this, String.Format(CultureInfo.CurrentUICulture, resourceManager.LocalizationResource.FailedToOpenFileFormatStr,
                            openFileDialog.FileName), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
                    }
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                FileSaveAs();
            }
            else
            {
                FileSave();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileSaveAs();
        }

        private void FileSave()
        {
            StopPlayingSequence();

            if (null != currentSequence)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(LayoutSequence));
                using (TextWriter writer = new StreamWriter(fileName))
                {
                    serializer.Serialize(writer, currentSequence);
                }
            }
            sequenceDirty = false;
            fieldControl.IsViewDirty = false;
            UpdateFileMenuItems();
        }

        private static string GetDefaultSequenceSaveLocation(bool attemptToCreate)
        {
            string fn1 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            fn1 = Path.Combine(fn1, "Sports Tactics Board");
            fn1 = Path.Combine(fn1, "Sequences");
            if (attemptToCreate)
            {
                // Ensure we have a folder to save files in
                if (!System.IO.Directory.Exists(fn1))
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(fn1);
                    }
                    catch (System.IO.IOException)
                    {
                    }
                }
            }
            return fn1;
        }

        private void FileSaveAs()
        {
            StopPlayingSequence();

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = fileFilter;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.SupportMultiDottedExtensions = true;
                saveFileDialog.InitialDirectory = GetDefaultSequenceSaveLocation(true);

                DialogResult res = saveFileDialog.ShowDialog();
                if (res == DialogResult.OK)
                {
                    fileName = saveFileDialog.FileName;
                    UpdateCaption();
                    FileSave();
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopPlayingSequence();

            FileNew(true, false);
        }

        static private List<IPlayingSurfaceType> FindPlayingSurfacesInAssembly(Assembly a)
        {
            List<IPlayingSurfaceType> surfaceTypes = new List<IPlayingSurfaceType>();

            Type[] types = a.GetTypes();
            for (int i = 0; i < types.Length; i++)
            {
                if ((types[i].IsClass) && (!types[i].IsAbstract))
                {
                    Type iface = types[i].GetInterface(typeof(IPlayingSurfaceType).Name);
                    if (iface != null)
                    {
                        IPlayingSurfaceType surfaceType = Activator.CreateInstance(types[i]) as IPlayingSurfaceType;
                        surfaceTypes.Add(surfaceType);
                    }
                }
            }

            return surfaceTypes;
        }

        internal static List<IPlayingSurfaceType> AvailableFieldTypes
        {
            get
            {
                return FindPlayingSurfacesInAssembly(Assembly.GetExecutingAssembly());
            }
        }

        private static IPlayingSurfaceType? FindFieldType(string tag)
        {
            List<IPlayingSurfaceType> fieldTypes = AvailableFieldTypes;
            foreach (IPlayingSurfaceType ft in fieldTypes)
            {
                if (ft.Tag == tag)
                {
                    return ft;
                }
            }
            return null;
        }

        private static IPlayingSurfaceType? LoadDefaultFieldType()
        {
            var defaultFieldType = Appsettings.Settings?.DefaultFieldType;
            if (defaultFieldType != null && defaultFieldType.Length == 0)
            {
                return null;
            };

            List<IPlayingSurfaceType> fieldTypes = AvailableFieldTypes;
            foreach (IPlayingSurfaceType ft in fieldTypes)
            {
                if (ft.Name == defaultFieldType)
                {
                    return ft;
                }
            }
            return null;
        }

        private void FileNew(bool saveAsDefaultChecked, bool alwaysAskForFieldType)
        {
            StopPlayingSequence();

            IPlayingSurfaceType? newFieldType = fieldControl.FieldType;
            if (newFieldType == null)
            {
                newFieldType = LoadDefaultFieldType();
            }
            if ((newFieldType == null) || (alwaysAskForFieldType))
            {
                newFieldType = SelectPlayingSurfaceType.AskUserForFieldType(saveAsDefaultChecked);
                if (null == newFieldType)
                {
                    return;
                }
            }

            fileName = "";
            fieldControl.FieldType = newFieldType;
            fieldControl.SetNextLayout(null);
            fieldControl.IsDirty = false;
            fieldControl.IsViewDirty = false;
            positionInSequence = 0;
            sequenceDirty = false;
            currentSequence = new LayoutSequence(fieldControl.FieldType.Tag);
            RecordPositionToSequence(false, positionInSequence, currentSequence);
            UpdateCaption();
            UpdateFileMenuItems();
            UpdateLayoutMenuItems();
            UpdateCommonSavedLayoutMenuItems();
            UpdateUserSavedLayoutMenuItems();
            UpdateViewMenuItems();
            UpdateSequenceControls();
        }

        private void showMovementButton_Click(object sender, EventArgs e)
        {
            ToolStripButton b = (ToolStripButton)sender;
            fieldControl.ShowMovementLines = !fieldControl.ShowMovementLines;
            b.Checked = fieldControl.ShowMovementLines;
        }

        private void fieldControl_KeyDown(object sender, KeyEventArgs e)
        {
            StopPlayingSequence();

            if (!e.Handled)
            {
                if ((e.KeyCode == Keys.Left) || (e.KeyCode == Keys.Up))
                {
                    MoveToPreviousLayout();
                }
                else if ((e.KeyCode == Keys.Right) || (e.KeyCode == Keys.Down))
                {
                    MoveToNextLayout();
                }
            }
        }

        private int RecordPositionToSequence(bool replace, int index, LayoutSequence sequence)
        {
            FieldLayout layout = fieldControl.FieldLayout;
            fieldControl.IsDirty = false;
            sequenceDirty = true;
            if (replace)
            {
                sequence.SetLayout(index, layout);
                return index;
            }
            else
            {
                return sequence.AddNewLayout(index, layout);
            }
        }

        private void RestorePositionFromSequence(int index, LayoutSequence sequence, FieldLayout _nextLayout)
        {
            fieldControl.SetLayouts(sequence.GetLayout(index), _nextLayout);
            fieldControl.IsDirty = false;
        }

        private void savedCurrentLayoutMenuItem_Click(object sender, EventArgs e)
        {
            StopPlayingSequence();
            SaveCurrentLayout();
        }

        private void NotImplementedYet()
        {
            ResourceManager resourceManager = ResourceManager.GetInstance();
            RtlAwareMessageBox.Show(this, resourceManager.LocalizationResource.NotImplementedYet, this.Text,
              MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
        }

        private void removeSavedLayoutMenuItem_Click(object sender, EventArgs e)
        {
            StopPlayingSequence();
            NotImplementedYet();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutBox aboutBox = new AboutBox())
            {
                aboutBox.ShowDialog();
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            UpdateFileMenuItems();
            UpdateLayoutMenuItems();
            UpdateCommonSavedLayoutMenuItems();
            UpdateUserSavedLayoutMenuItems();
            UpdateSequenceControls();

            if (null == fieldControl.FieldType)
            {
                FileNew(true, false);
            }
        }

        private void newPlayingSurfaceTypeMenuItem_Click(object sender, EventArgs e)
        {
            StopPlayingSequence();

            FileNew(false, true);
        }

        private void RenderToImage(Bitmap bitmap, FieldLayout layout, FieldLayout nextLayout)
        {
            FieldLayout nl = null;
            if (fieldControl.ShowMovementLines)
            {
                nl = nextLayout;
            }
            fieldControl.DrawIntoImage(bitmap, layout, nl);
        }

        private void copyMenuItem_Click(object sender, EventArgs e)
        {
            StopPlayingSequence();

            Clipboard.Clear();
            using (Bitmap bitmap = new Bitmap(fieldControl.Width, fieldControl.Height))
            {
                RenderToImage(bitmap, fieldControl.FieldLayout, GetNextLayout());
                Clipboard.SetImage(bitmap);
            }
        }

        private void SaveSequenceEntryToFile(string imageFileName, FieldLayout layout, FieldLayout nextLayout, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            using (Bitmap bitmap = new Bitmap(fieldControl.Width, fieldControl.Height))
            {
                RenderToImage(bitmap, layout, nextLayout);
                bitmap.Save(imageFileName, imageFormat);
            }
        }

        private void SaveImagesToFile(bool saveEntireSequence)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                ResourceManager resourceManager = ResourceManager.GetInstance();
                saveFileDialog.Filter = saveAsImageFileFilter;
                saveFileDialog.FilterIndex = 3; // set to PNG by default
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.OverwritePrompt = !saveEntireSequence;
                if (saveEntireSequence)
                {
                    saveFileDialog.Title = resourceManager.LocalizationResource.SaveImageSequenceDialogTitle;
                }
                else
                {
                    saveFileDialog.Title = resourceManager.LocalizationResource.SaveImageDialogTitle;
                }

                DialogResult res = saveFileDialog.ShowDialog();
                if (res != DialogResult.OK)
                {
                    return;
                }

                System.Drawing.Imaging.ImageFormat imageFormat = System.Drawing.Imaging.ImageFormat.Png;
                switch (saveFileDialog.FilterIndex)
                {
                    case 1:
                        imageFormat = System.Drawing.Imaging.ImageFormat.Bmp;
                        break;
                    case 2:
                        imageFormat = System.Drawing.Imaging.ImageFormat.Gif;
                        break;
                    case 3:
                    default:
                        imageFormat = System.Drawing.Imaging.ImageFormat.Png;
                        break;
                    case 4:
                        imageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
                        break;
                }

                if (saveEntireSequence)
                {
                    string fileNamePattern = saveFileDialog.FileName;
                    int idx = fileNamePattern.LastIndexOf(".", StringComparison.Ordinal);
                    if (idx < 0)
                    {
                        idx = fileNamePattern.Length;
                    }

                    fileNamePattern = fileNamePattern.Insert(idx, resourceManager.LocalizationResource.ImageFileNamePattern);
                    for (int sequenceIndex = 0; sequenceIndex < currentSequence.NumberOfLayouts; sequenceIndex++)
                    {
                        string entryFileName = string.Format(CultureInfo.CurrentUICulture, fileNamePattern, sequenceIndex + 1);
                        SaveSequenceEntryToFile(entryFileName, currentSequence.GetLayout(sequenceIndex), currentSequence.GetLayout(sequenceIndex + 1), imageFormat);
                    }
                }
                else
                {
                    SaveSequenceEntryToFile(saveFileDialog.FileName, fieldControl.FieldLayout, GetNextLayout(), imageFormat);
                }
            }
        }

        private void saveSequenceToImageFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopPlayingSequence();

            SaveImagesToFile(true);
        }

        private void saveTofileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopPlayingSequence();

            SaveImagesToFile(false);
        }

        private void printMenuItem_Click(object sender, EventArgs e)
        {
            StopPlayingSequence();

            NotImplementedYet();
        }

        private void ShowInstalledDocument(string[] possibleDocumentNames)
        {
            string exePath = Application.ExecutablePath;
            string binDir = Path.GetDirectoryName(exePath);
            string installDir = Path.GetDirectoryName(binDir);
#if DEBUG
            string rootProjectDir = Path.GetDirectoryName(installDir);
#endif
            string[] appFolders = new string[] {
                installDir, binDir
#if DEBUG
                ,rootProjectDir
#endif
            };

            string documentFileName = null;
            // Locate the document from the list of possible names
            foreach (string docName in possibleDocumentNames)
            {
                foreach (string appFolder in appFolders)
                {
                    documentFileName = Path.Combine(appFolder, docName);
                    if (!File.Exists(documentFileName))
                    {
                        documentFileName = null;
                    }
                    else
                    {
                        // exit the inner loop as we have found one that exists
                        break;
                    }
                }

                if (documentFileName != null)
                {
                    // exit the outer loop as we have found one that exists
                    break;
                }
            }

            // If we located a document, open it. If successful,
            // exit, otherwise catch the exceptions and display
            // a generic error message for all errors.
            if (!string.IsNullOrEmpty(documentFileName))
            {
                try
                {
                    System.Diagnostics.Process.Start(documentFileName);
                    return;
                }
                catch (ObjectDisposedException)
                {
                }
                catch (InvalidOperationException)
                {
                }
                catch (System.ComponentModel.Win32Exception)
                {
                }
            }

            ResourceManager resourceManager = ResourceManager.GetInstance();
            RtlAwareMessageBox.Show(this, resourceManager.LocalizationResource.UnableToOpenFileInstallationMayBeIncomplete, this.Text,
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
            return;
        }

        private void licenseMenuItem_Click(object sender, EventArgs e)
        {
            ShowInstalledDocument(new string[] { "LICENSE" });
        }

        private void readMeMenuItem_Click(object sender, EventArgs e)
        {
            ShowInstalledDocument(new string[] { "README.md" });
        }

        private void changeLogMenuItem_Click(object sender, EventArgs e)
        {
            ShowInstalledDocument(new string[] { "changelog.txt" });
        }

        private void StopPlayingSequence()
        {
            playSequenceTimer.Enabled = false;
            fieldControl.AllowInteraction = true;
            UpdateSequenceControls();
        }

        private void StartPlayingSequence()
        {
            playSequenceTimer.Interval = Appsettings.Settings?.AnimationFrameDurationInMilliseconds != null ? Appsettings.Settings.AnimationFrameDurationInMilliseconds : 0;
            playSequenceTimer.Enabled = true;
            fieldControl.AllowInteraction = false;
            UpdateSequenceControls();
        }

        private bool IsPlayingSequence
        {
            get { return playSequenceTimer.Enabled; }
        }

        private void goToFirstToolStripButton_Click(object sender, EventArgs e)
        {
            StopPlayingSequence();
            if (null == currentSequence)
            {
                return;
            }
            positionInSequence = 0;
            RestorePositionFromSequence(positionInSequence, currentSequence, GetNextLayout());
            UpdateSequenceControls();
        }


        private void playToolStripButton_Click(object sender, EventArgs e)
        {
            if (IsPlayingSequence)
            {
                StopPlayingSequence();
            }
            else
            {
                if (null == currentSequence)
                {
                    return;
                }

                if ((currentSequence.NumberOfLayouts > 0) && (fieldControl.IsDirty))
                {
                    ResourceManager resourceManager = ResourceManager.GetInstance();
                    DialogResult dr = RtlAwareMessageBox.Show(this, resourceManager.LocalizationResource.SaveSequenceEntryBeforeSwitchingEntries, this.Text,
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
                    switch (dr)
                    {
                        case DialogResult.Yes:
                            RecordPositionToSequence(true, positionInSequence, currentSequence);
                            break;
                        case DialogResult.Cancel:
                            return;
                    }
                }

                if (positionInSequence + 1 >= currentSequence.NumberOfLayouts)
                {
                    GoToStartOfSequence();
                }
                StartPlayingSequence();
            }
        }

        private void GoToStartOfSequence()
        {
            positionInSequence = 0;
            RestorePositionFromSequence(positionInSequence, currentSequence, GetNextLayout());
            UpdateSequenceControls();
        }

        private void playSequenceTimer_Tick(object sender, EventArgs e)
        {
            if (positionInSequence + 1 < currentSequence.NumberOfLayouts)
            {
                MoveToNextLayout();
                if (positionInSequence + 1 < currentSequence.NumberOfLayouts)
                {
                    // Continue playing the sequence if there are more left
                    return;
                }
                else if (repeatToolStripButton.Checked)
                {
                    // Repeat is enabled, so go back to the beginning and continue playback
                    GoToStartOfSequence();
                    return;
                }
            }
            StopPlayingSequence();
        }

        private void goToLastToolStripButton_Click(object sender, EventArgs e)
        {
            StopPlayingSequence();
            if (null == currentSequence)
            {
                return;
            }
            if (currentSequence.NumberOfLayouts > 0)
            {
                positionInSequence = currentSequence.NumberOfLayouts - 1;
                RestorePositionFromSequence(positionInSequence, currentSequence, GetNextLayout());
                UpdateSequenceControls();
            }
        }

        #region ICustomLabelProvider Members

        public string GetCustomLabel(string tag)
        {
            if (null != currentSequence)
            {
                return currentSequence.GetCustomLabel(tag);
            }
            return null;
        }

        public void UpdateCustomLabel(string tag, string label)
        {
            if (null != currentSequence)
            {
                currentSequence.AddOrUpdateCustomLabel(tag, label);
            }
        }

        public void RemoveCustomLabel(string tag)
        {
            if (null != currentSequence)
            {
                currentSequence.RemoveCustomLabel(tag);
            }
        }

        #endregion

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fieldControl.SetView(fieldControl.ZoomPoint, fieldControl.ZoomFactor, 0.0F);
            UpdateViewMenuItems();
        }

        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fieldControl.SetView(fieldControl.ZoomPoint, fieldControl.ZoomFactor, 90.0F);
            UpdateViewMenuItems();
        }

        private void resetViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fieldControl.ResetView();
            UpdateViewMenuItems();
        }

        private void UpdateViewMenuItems()
        {
            horizontalToolStripMenuItem.Checked = fieldControl.RotationAngle == 0.0F;
            verticalToolStripMenuItem.Checked = fieldControl.RotationAngle == 90.0F;
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopPlayingSequence();
            NotImplementedYet();
        }

        private void choseLanguageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;
            var str = item.Tag.ToString();
            //Properties.Settings.Default.DefaultLanguage = str;
            //Properties.Settings.Default.Save();
            ResourceManager resourceManager = ResourceManager.GetInstance();
            resourceManager.SetLocal(str);
            SetLanguage();
        }

        private void SetLanguage()
        {
            ResourceManager resourceManager = ResourceManager.GetInstance();
            //this.cur = new CultureInfo(resourceManager.GetLocal());

            recordNewPositionButton.Text = resourceManager.LocalizationResource.RecordNewPosition;
            previousLayoutInSequence.Text = resourceManager.LocalizationResource.PreviousLayout;
            nextLayoutInSequence.ToolTipText = resourceManager.LocalizationResource.NextLayout;
            recordOverCurrentPositionButton.Text = resourceManager.LocalizationResource.RecordOverCurrentPosition;
            removeCurrentPositionFromSequenceButton.Text = resourceManager.LocalizationResource.RemoveCurrent;
            revertCurrentLayoutToSavedButton.Text = resourceManager.LocalizationResource.RevertCurrent;
            sequenceMenuItem.Text = resourceManager.LocalizationResource.MenuSequence;
            openSequenceMenuItem.Text = resourceManager.LocalizationResource.MenuOpenSequence;
            saveSequenceMenuItem.Text = resourceManager.LocalizationResource.MenuSaveSequence;
            saveSequenceAsMenuItem.Text = resourceManager.LocalizationResource.MenuSaveSequenceAs;
            layoutsToolStripMenuItem.Text = resourceManager.LocalizationResource.MenuLayout;
            newSequenceMenuItem.Text = resourceManager.LocalizationResource.MenuNewSequence;
            showMovementButton.Text = resourceManager.LocalizationResource.ShowMovement;
            saveCurrentLayoutMenuItem.Text = resourceManager.LocalizationResource.MenuSaveCurrentLayout;
            commonSavedLayoutsMenuItem.Text = resourceManager.LocalizationResource.MenuCommonSavedLayouts;
            removeSavedLayoutMenuItem.Text = resourceManager.LocalizationResource.MenuRemoveSavedLayout;
            helpToolStripMenuItem.Text = resourceManager.LocalizationResource.MenuHelp;
            aboutToolStripMenuItem.Text = resourceManager.LocalizationResource.MenuAbout;
            playingSurfaceMenuItem.Text = resourceManager.LocalizationResource.MenuPlayingSurface;
            exportSequenceToImageFilesMenuItem.Text = resourceManager.LocalizationResource.MenuExport;
            exportSequenceToImageFilesMenuItem.ToolTipText = resourceManager.LocalizationResource.MenuExportTooltip;
            changePlayingSurfaceTypeMenuItem.Text = resourceManager.LocalizationResource.MenuChangePlayingSurfaceType;
            copyMenuItem.Text = resourceManager.LocalizationResource.MenuCopy;
            copyMenuItem.ToolTipText = resourceManager.LocalizationResource.MenuCopyTooltip;
            saveTofileToolStripMenuItem.Text = resourceManager.LocalizationResource.MenuSaveTofile;
            userSavedLayoutsMenuItem.Text = resourceManager.LocalizationResource.MenuUserSavedLayouts;
            printMenuItem.Text = resourceManager.LocalizationResource.MenuPrint;
            exitMenuItem.Text = resourceManager.LocalizationResource.MenuExit;
            licenseMenuItem.Text = resourceManager.LocalizationResource.MenuLicense;
            changeLogMenuItem.Text = resourceManager.LocalizationResource.MenuChangeLog;
            readMeMenuItem.Text = resourceManager.LocalizationResource.MenuReadMe;
            goToFirstToolStripButton.Text = resourceManager.LocalizationResource.GoToFirst;
            playToolStripButton.Text = resourceManager.LocalizationResource.PlayPause;
            goToLastToolStripButton.Text = resourceManager.LocalizationResource.GoToLast;
            orientationToolStripMenuItem.Text = resourceManager.LocalizationResource.MenuOrientation;
            horizontalToolStripMenuItem.Text = resourceManager.LocalizationResource.MenuHorizontal;
            verticalToolStripMenuItem.Text = resourceManager.LocalizationResource.MenuVertical;
            resetViewToolStripMenuItem.Text = resourceManager.LocalizationResource.MenuResetView;
            optionsToolStripMenuItem.Text = resourceManager.LocalizationResource.MenuOptions;
            repeatToolStripButton.Text = resourceManager.LocalizationResource.Repeat;
            choseLanguageToolStripMenuItem.Text = resourceManager.LocalizationResource.ChoseLanguageToolStripMenuItem;

            fileFilter = resourceManager.LocalizationResource.FileFilter;
            saveAsImageFileFilter = resourceManager.LocalizationResource.FileFilter;

            fieldControl.SetLanguage(resourceManager.LocalizationResource.ChangeLabel);

            var languages = Appsettings.Settings?.Languages?.Split(';');
            if (languages == null)
            {
                return;
            }

            choseLanguageToolStripMenuItem.DropDownItems.Clear();
            foreach (var language in languages)
            {
                var l = language.Split(':');
                var tmp = new ToolStripMenuItem();
                tmp.Text = l[0];
                tmp.Tag = l[1];
                tmp.Click += new EventHandler(choseLanguageToolStripMenuItem_Click);
                choseLanguageToolStripMenuItem.DropDownItems.Add(tmp);
            }
        }
    }
}