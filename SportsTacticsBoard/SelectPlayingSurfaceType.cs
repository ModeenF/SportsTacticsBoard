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
  public partial class SelectPlayingSurfaceType : Form
  {
    public SelectPlayingSurfaceType()
    {
      InitializeComponent();
    }

    internal static IPlayingSurfaceType AskUserForFieldType(bool saveAsDefaultChecked)
    {
      using (SelectPlayingSurfaceType sftDialog = new SelectPlayingSurfaceType()) {
        sftDialog.saveAsDefaultCheckBox.Checked = saveAsDefaultChecked;
        sftDialog.fieldTypeComboBox.DataSource = MainForm.AvailableFieldTypes;
        sftDialog.fieldTypeComboBox.DisplayMember = "Name";
        if (sftDialog.fieldTypeComboBox.Items.Count > 0) {
          string defaultFieldType = global::SportsTacticsBoard.Properties.Settings.Default.DefaultFieldType;
          int selectedIndex = 0;
          if (defaultFieldType.Length > 0) {
            int index = sftDialog.fieldTypeComboBox.FindStringExact(defaultFieldType);
            if (index >= 0) {
              selectedIndex = index;
            }
          }
          sftDialog.fieldTypeComboBox.SelectedIndex = selectedIndex;
        }
        if (sftDialog.ShowDialog() != DialogResult.OK) {
          return null;
        }
        IPlayingSurfaceType selectedFieldType = (IPlayingSurfaceType)sftDialog.fieldTypeComboBox.SelectedItem;
        if (sftDialog.saveAsDefaultCheckBox.Checked) {
          global::SportsTacticsBoard.Properties.Settings.Default.DefaultFieldType =
            selectedFieldType.Name;
          global::SportsTacticsBoard.Properties.Settings.Default.Save();
        }
        return selectedFieldType;
      }
    }

  }
}