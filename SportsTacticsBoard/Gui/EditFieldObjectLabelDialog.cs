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
// Copyright (C) 2010 Robert Turner
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
using SportsTacticsBoard.Resources;
using System.Windows.Forms;

namespace SportsTacticsBoard
{
    internal partial class EditFieldObjectLabelDialog : Form
    {
        public string FieldObjectName
        {
            get
            {
                return fieldObjectNameTextBox.Text;
            }
            set
            {
                fieldObjectNameTextBox.Text = value;
            }
        }

        public string CustomLabel
        {
            get
            {
                return customLabelTextBox.Text;
            }
            set
            {
                customLabelTextBox.Text = value;
            }
        }

        public EditFieldObjectLabelDialog()
        {
            InitializeComponent();
            SetLanguage();
        }

        private void OkButton_Click(object sender, System.EventArgs e)
        {            
            if (customLabelTextBox.Text.Length > 2)
            {
                ResourceManager resourceManager = ResourceManager.GetInstance();
                DialogResult dr = RtlAwareMessageBox.Show(this, resourceManager.LocalizationResource.CustomLabelLongerThanRecommended,
                    this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, 0);
                if (dr != DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.None;
                }
            }
        }

        private void SetLanguage()
        {
            ResourceManager resourceManager = ResourceManager.GetInstance();
            customLabelLabel.Text = resourceManager.LocalizationResource.CustomLabel;
            revertToDefaultButton.Text = resourceManager.LocalizationResource.RevertToDefault;
            fieldObjectLabel.Text = resourceManager.LocalizationResource.FieldObjectLabel;
            okButton.Text = resourceManager.LocalizationResource.Ok;
            cancelButton.Text = resourceManager.LocalizationResource.Cancel;
            this.Text = resourceManager.LocalizationResource.EditFieldObjectLabelDialog;
        }
    }
}
