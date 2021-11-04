// Sports Tactics Board
//
// http://sportstacticsbd.sourceforge.net/
// http://sourceforge.net/projects/sportstacticsbd/
// 
// Sports Tactics Board is a utility that allows coaches, trainers and 
// officials to describe sports tactics, strategies and positioning using 
// a magnetic or chalk-board style approach.
// 
// Copyright (C) 2007-2010 Robert Turner
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
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Xml.Serialization;
using System.IO;

namespace SportsTacticsBoard
{
  /// <summary>
  /// This class provides management of saved layout sets. Each 
  /// saved layout set is loaded from a chosen folder on disk, and
  /// subsequently added layouts are saved to that location.
  /// </summary>
  class SavedLayoutManager
  {
    /// <summary>
    /// Stores the saved layouts.
    /// </summary>
    private List<SavedLayout> savedLayouts;

    /// <summary>
    /// Default constructor for the saved layout manager.
    /// </summary>
    public SavedLayoutManager(string layoutFolderPath) {
      LayoutPath = layoutFolderPath;
    }

    /// <summary>
    /// Loads the layouts from disk into local data members.
    /// This can be used to refresh the current layouts from the
    /// data stored on disk. This is useful if files are being
    /// modified while the program is running.
    /// </summary>
    public void LoadLayouts()
    {
      savedLayouts = new List<SavedLayout>();
      if (!string.IsNullOrEmpty(LayoutPath)) {
        RecursivelyLoadLayoutsFromFolder(LayoutPath);
      }
    }

    /// <summary>
    /// Data member that stores the layout path.
    /// </summary>
    private string layoutPath;

    /// <summary>
    /// Retrieves the folder path name that this layout manager is
    /// saving/loading layouts to/from.
    /// </summary>
    public string LayoutPath
    {
      get { return layoutPath; }
      private set { 
        layoutPath = value;
        LoadLayouts();
      }
    }

    /// <summary>
    /// Provides the layout path for the layout library (shared/common saved layouts).
    /// This can be passed as the folder name to the constructor of this class when
    /// it is instantiated.
    /// This first loads a value from the configuration file, if that's set, it uses
    /// it, otherwise it attempts to construct the path from the location of the
    /// program binary (which only works for installed copies using the MSI installer).
    /// </summary>
    public static string CommonLayoutPath
    {
      get
      {
        string path = SportsTacticsBoard.Properties.Settings.Default.CommonLibraryFolder;
        if (string.IsNullOrEmpty(path)) {
          string exeDir = Path.GetDirectoryName(Application.ExecutablePath);
          string installDir = Path.GetDirectoryName(exeDir);
          path = Path.Combine(installDir, "Library");
          if (!Directory.Exists(path)) {
            string rootProjectDir = Path.GetDirectoryName(installDir);
            path = Path.Combine(rootProjectDir, "Library");
            if (!Directory.Exists(path)) {
              return null;
            }
          }
        }
        path = Path.Combine(path, "Layouts");
        return path;
      }
    }

    /// <summary>
    /// Provides the layout path for the user's saved layouts.
    /// This can be passed as the folder name to the constructor of this class when
    /// it is instantiated.
    /// </summary>
    public static string UserLayoutPath
    {
      get
      {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        path = Path.Combine(path, "Sports Tactics Board");
        path = Path.Combine(path, "Layouts");
        return path;
      }
    }

    /// <summary>
    /// The file extension for files used to store saved layouts in.
    /// </summary>
    private const string FileExtension = ".layout.xml";

    /// <summary>
    /// Contructs the filename, including sub-folders, for the saved layout.
    /// By default, layouts are saved in separate folders for each playing 
    /// surface type and each category.
    /// </summary>
    /// <param name="layout">The layout to generate a file name for.</param>
    /// <returns>The file name for the layout file.</returns>
    private string GetFileNameForLayout(SavedLayout layout)
    {
      if (string.IsNullOrEmpty(layout.FieldTypeTag)) {
        string msg = Properties.Resources.ResourceManager.GetString("ExceptionMessage_LayoutFieldTagInvalid");
        throw new ArgumentException(msg, "layout");
      }
      if (string.IsNullOrEmpty(layout.Name)) {
        string msg = Properties.Resources.ResourceManager.GetString("ExceptionMessage_LayoutNameInvalid");
        throw new ArgumentException(msg, "layout");
      }
      string fn = Path.Combine(LayoutPath, layout.FieldTypeTag);
      if (!string.IsNullOrEmpty(layout.Category)) {
        fn = Path.Combine(fn, layout.Category);
      }
      fn = Path.Combine(fn, layout.Name + FileExtension);
      return fn;
    }

    /// <summary>
    /// Reads a saved layout from a file. This method catches most exceptions and
    /// masks any errors that occur and returns a null layout if an error occurs.
    /// </summary>
    /// <param name="fileName">The file to read the layout from.</param>
    /// <returns>A saved layout or null if the file could not be loaded properly.</returns>
    private static SavedLayout ReadLayoutFromFile(string fileName)
    {
      try {
        XmlSerializer serializer = new XmlSerializer(typeof(SavedLayout));
        using (FileStream fs = new FileStream(fileName, FileMode.Open)) {
          SavedLayout layout = (SavedLayout)serializer.Deserialize(fs);
          if ((string.IsNullOrEmpty(layout.FieldTypeTag)) ||
              (string.IsNullOrEmpty(layout.Name))) {
            return null;
          }
          return layout;
        }
      }
      catch (System.InvalidOperationException) {
        return null;
      }
      catch (System.Xml.XmlException) {
        return null;
      }
      catch (System.IO.IOException) {
        return null;
      }
    }

    /// <summary>
    /// Saves the supplied layout to the specified file.
    /// </summary>
    /// <param name="layout">The saved layout object to write to the file.</param>
    /// <param name="fileName">The name of the file to write.</param>
    /// <returns>true if the file was written, false if an error occured.</returns>
    private static bool SaveLayoutToFile(SavedLayout layout, string fileName)
    {
      try {
        XmlSerializer serializer = new XmlSerializer(typeof(SavedLayout));
        using (TextWriter writer = new StreamWriter(fileName)) {
          serializer.Serialize(writer, layout);
        }
        return true;
      }
      catch (System.IO.IOException) {
        return false;
      }
    }

    private void AddLayout(SavedLayout layout)
    {
      // TODO: See if the layout already "exists" in our set of layouts
      savedLayouts.Add(layout);
    }

    /// <summary>
    /// Loads all layouts located in the supplied folder into the class data member. 
    /// This method recusively scans subfolders to locate the layout files.
    /// </summary>
    /// <param name="folderName">The folder to load layouts from.</param>
    private void RecursivelyLoadLayoutsFromFolder(string folderName)
    {
      if (string.IsNullOrEmpty(folderName)) {
        return;
      }
      try {
        // Load the layouts from disk.
        foreach (string fileName in Directory.GetFiles(folderName, "*" + FileExtension, SearchOption.AllDirectories)) {
          // Load each file
          SavedLayout layout = ReadLayoutFromFile(fileName);
          if (null != layout) {
            AddLayout(layout);
          } else {
            System.Diagnostics.Trace.TraceWarning("Unable to load layout file '{0}'", fileName);
          }
        }
      }
      catch (DirectoryNotFoundException) {
        // Ignore errors if the folder doesn't exist
        System.Diagnostics.Trace.TraceInformation("Layout folder '{0}' does not exist", folderName);
      }
    }

    /// <summary>
    /// Updates the specified menu by inserting menu items for each of 
    /// the saved layouts into the menu. The items are inserted into 
    /// sub-menus corresponding to the categories. Only saved layouts 
    /// for the specified field type will be inserted.
    /// </summary>
    /// <param name="menuToInsertInto">The menu item that will have a drop 
    /// down menu created beneath it with the layouts in.</param>
    /// <param name="fieldTypeTag">Only layouts for the this field type 
    /// will be inserted into the menu.</param>
    /// <param name="menuItemClickEventHandler">The event handler to invoke 
    /// when any of the inserted menu items are clicked.</param>
    /// <returns>true if items were inserted into the menu, false otherwise</returns>
    public bool UpdateMenu(ToolStripMenuItem menuToInsertInto, string fieldTypeTag, EventHandler menuItemClickEventHandler) {
      Dictionary<string, ToolStripMenuItem> categorySubMenus = new Dictionary<string, ToolStripMenuItem>();
      menuToInsertInto.DropDownItems.Clear();
      bool itemsInserted = false;
      if (savedLayouts.Count > 0) {
        foreach (SavedLayout savedLayout in savedLayouts) {
          if (MatchesFieldTypeTag(fieldTypeTag, savedLayout.FieldTypeTag)) {
            ToolStripMenuItem menuToInsertIn = menuToInsertInto;
            if (!string.IsNullOrEmpty(savedLayout.Category)) {
              if (categorySubMenus.ContainsKey(savedLayout.Category)) {
                menuToInsertIn = categorySubMenus[savedLayout.Category];
              } else {
                menuToInsertIn = new ToolStripMenuItem(savedLayout.Category);
                categorySubMenus.Add(savedLayout.Category, menuToInsertIn);
              }
            }
            ToolStripMenuItem mi = null;
            try {
              mi = new ToolStripMenuItem(savedLayout.Name, null, menuItemClickEventHandler);
              if (savedLayout.Description.Length > 0) {
                mi.ToolTipText = savedLayout.Description;
              }
              menuToInsertIn.DropDownItems.Add(mi);
              mi = null;
            } finally {
              if (null != mi) {
                mi.Dispose();
              }
            }
            itemsInserted = true;
          }
        }
      }
      if (categorySubMenus.Count > 0) {
        int index = 0;
        foreach (ToolStripMenuItem menuItem in categorySubMenus.Values) {
          menuToInsertInto.DropDownItems.Insert(index, menuItem);
          index++;
        }
      }
      if (!itemsInserted) {
        string menuItemStr = Properties.Resources.ResourceManager.GetString("NoSavedLayoutsMenuItemText");
        ToolStripMenuItem mi = null;
        try {
          mi = new ToolStripMenuItem(menuItemStr);
          mi.Enabled = false;
          menuToInsertInto.DropDownItems.Add(mi);
          mi = null;
        } finally {
          if (null != mi) {
            mi.Dispose();
          }
        }
      }
      return itemsInserted;
    }

    /// <summary>
    /// Retrieves the field object layout for the specified menu item. This 
    /// menu item must have been inserted by UpdateMenu().
    /// </summary>
    /// <param name="menuItem">The menu item to obtain the layout for.</param>
    /// <param name="fieldTypeTag">The field type that the layout must apply to.</param>
    /// <returns>The field object layout for the menu item. Returns null if no layout is 
    /// found or does not apply for the supplied field type.</returns>
    public FieldLayout GetLayoutForMenuItem(ToolStripMenuItem menuItem, string fieldTypeTag) {
      string name = menuItem.Text;
      foreach (SavedLayout savedLayout in savedLayouts) {
        if ((MatchesFieldTypeTag(fieldTypeTag, savedLayout.FieldTypeTag)) &&
            (savedLayout.Name == name)) {
          return savedLayout.Layout;
        }
      }
      return null;
    }

    /// <summary>
    /// Prompts the user to save the current layout, along with asking for 
    /// details under which to save the layout. If the user saves the layout, it
    /// is added to the list of saved layouts.
    /// </summary>
    /// <param name="layoutToSave">The layout to be saved which describes the 
    /// current positions of the field objects.</param>
    /// <param name="fieldTypeTag">Identifies the field type of the layout.</param>
    public void SaveCurrentLayout(ICollection<FieldObject> layoutToSave, string fieldTypeTag) {
      if (string.IsNullOrEmpty(layoutPath)) {
        // TODO: Display an error message to the user
        return;
      }
      SavedLayout sl =
        SavedLayoutInformation.AskUserForSavedLayoutDetails(layoutToSave,
                                                            fieldTypeTag,
                                                            GetCurrentSavedLayoutCategories(fieldTypeTag));
      if (null != sl) {
        // Save the layout to disk
        string fileName = GetFileNameForLayout(sl);
        {
          string folderName = Path.GetDirectoryName(fileName);
          if (!Directory.Exists(folderName)) {
            Directory.CreateDirectory(folderName);
          }
        }
        bool saveResult = SaveLayoutToFile(sl, fileName);
        if (saveResult) {
          // Add the layout to our internal list
          AddLayout(sl);
        } else {
          // TODO: Display an error message to the user
        }
      }
    }

    /// <summary>
    /// Helper method to compare two field type tags and account for 
    /// null or empty string objects.
    /// </summary>
    /// <param name="fieldTypeTag">The first field type tag to compare.</param>
    /// <param name="layoutFieldTypeTag">The second field type tag to compare.</param>
    /// <returns>true if the field type tags are the same, false otherwise</returns>
    private static bool MatchesFieldTypeTag(string fieldTypeTag, string layoutFieldTypeTag)
    {
      if ((string.IsNullOrEmpty(fieldTypeTag)) || (string.IsNullOrEmpty(layoutFieldTypeTag))) {
        return false;
      }
      return fieldTypeTag == layoutFieldTypeTag;
    }

    /// <summary>
    /// Helper method that returns an array of strings corresponding to 
    /// each of the categories associated with all saved layouts for the 
    /// specified field type.
    /// </summary>
    /// <param name="fieldTypeTag">The field type tag for which to 
    /// consider layouts.</param>
    /// <returns>An array of strings with the category names - suitable 
    /// for display to the user.</returns>
    private string[] GetCurrentSavedLayoutCategories(string fieldTypeTag)
    {
      List<string> result = new List<string>();
      foreach (SavedLayout savedLayout in savedLayouts) {
        if ((MatchesFieldTypeTag(fieldTypeTag, savedLayout.FieldTypeTag)) &&
            (!string.IsNullOrEmpty(savedLayout.Category))) {
          result.Add(savedLayout.Category);
        }
      }
      return result.ToArray();
    }

  }
}
