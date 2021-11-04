// Sports Tactics Board
//
// http://sportstacticsbd.sourceforge.net/
// http://sourceforge.net/projects/sportstacticsbd/
// 
// Sports Tactics Board is a utility that allows coaches, trainers and 
// officials to describe sports tactics, strategies and positioning using 
// a magnetic or chalk-board style approach.
// 
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SportsTacticsBoard
{
  public partial class SavedLayoutInformation : Form
  {
    internal class SavedLayoutEntryItem
    {
      public string Tag { get; set; }
      public string Name { get; set; }

      public override string ToString()
      {
        if (string.IsNullOrEmpty(Name)) {
          return Tag;
        }
        return Name;
      }
    }

    public SavedLayoutInformation()
    {
      InitializeComponent();
    }

    internal static SavedLayout AskUserForSavedLayoutDetails(ICollection<FieldObject> fieldObjects, string fieldTypeTag, string[] existingLayoutCategories)
    {
      using (SavedLayoutInformation dialog = new SavedLayoutInformation()) {
        foreach (var fo in fieldObjects) {
          SavedLayoutEntryItem e = new SavedLayoutEntryItem()
          {
            Tag = fo.Tag,
            Name = fo.Name
          };
          dialog.entriesListBox.Items.Add(e, true);
        }
        dialog.categoryComboBox.DataSource = existingLayoutCategories;
        dialog.categoryComboBox.SelectedIndex = -1; // make sure nothing is specified at first

        if (dialog.ShowDialog() == DialogResult.OK) {
          FieldLayout layout = FieldControl.ConvertFieldObjectsToLayout(fieldObjects);
          for (int index = 0; index < dialog.entriesListBox.Items.Count; index++) {
            if (!dialog.entriesListBox.GetItemChecked(index)) {
              layout.RemoveEntry(((SavedLayoutEntryItem)(dialog.entriesListBox.Items[index])).Tag);
            }
          }
          return new SavedLayout(dialog.nameTextBox.Text, dialog.categoryComboBox.Text, dialog.descriptionTextBox.Text, layout, fieldTypeTag);
        }
        return null;
      }
    }

    private void nameTextBox_Validating(object sender, CancelEventArgs e)
    {
      if (nameTextBox.Text.Trim().Length == 0)
      {
        errorProvider.SetError(nameTextBox, Properties.Resources.SavedLayoutInformation_ErrorMessage_NameMustNotBeBlank);
        e.Cancel = true;
        return;
      }
    }

    private void nameTextBox_Validated(object sender, EventArgs e)
    {
      errorProvider.SetError(nameTextBox, string.Empty);
    }

    private void entriesListBox_Validating(object sender, CancelEventArgs e)
    {
      if (entriesListBox.CheckedItems.Count == 0)
      {
        errorProvider.SetError(entriesListBox, Properties.Resources.SavedLayoutInformation_ErrorMessage_AtLeastOneItemMustBeChecked);
        e.Cancel = true;
        return;
      }
    }

    private void entriesListBox_Validated(object sender, EventArgs e)
    {
      errorProvider.SetError(entriesListBox, string.Empty);
    }
  }
}