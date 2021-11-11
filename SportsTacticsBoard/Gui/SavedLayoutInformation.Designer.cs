// Sports Tactics Board
//
// http://sportstacticsbd.sourceforge.net/
// http://sourceforge.net/projects/sportstacticsbd/
// 
// Sports Tactics Board is a utility that allows coaches, trainers and 
// officials to describe sports tactics, strategies and positioning using 
// a magnetic or chalk-board style approach.
// 
// Copyright (C) 2006 Robert Turner
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
namespace SportsTacticsBoard
{
  partial class SavedLayoutInformation
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SavedLayoutInformation));
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.nameLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.entriesLabel = new System.Windows.Forms.Label();
            this.entriesListBox = new System.Windows.Forms.CheckedListBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.categoryLabel = new System.Windows.Forms.Label();
            this.categoryComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.CausesValidation = false;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // nameLabel
            // 
            resources.ApplyResources(this.nameLabel, "nameLabel");
            this.nameLabel.Name = "nameLabel";
            // 
            // descriptionLabel
            // 
            resources.ApplyResources(this.descriptionLabel, "descriptionLabel");
            this.descriptionLabel.Name = "descriptionLabel";
            // 
            // nameTextBox
            // 
            resources.ApplyResources(this.nameTextBox, "nameTextBox");
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.nameTextBox_Validating);
            this.nameTextBox.Validated += new System.EventHandler(this.nameTextBox_Validated);
            // 
            // descriptionTextBox
            // 
            resources.ApplyResources(this.descriptionTextBox, "descriptionTextBox");
            this.descriptionTextBox.Name = "descriptionTextBox";
            // 
            // entriesLabel
            // 
            resources.ApplyResources(this.entriesLabel, "entriesLabel");
            this.entriesLabel.Name = "entriesLabel";
            // 
            // entriesListBox
            // 
            resources.ApplyResources(this.entriesListBox, "entriesListBox");
            this.entriesListBox.CheckOnClick = true;
            this.entriesListBox.Name = "entriesListBox";
            this.entriesListBox.Sorted = true;
            this.entriesListBox.Validating += new System.ComponentModel.CancelEventHandler(this.entriesListBox_Validating);
            this.entriesListBox.Validated += new System.EventHandler(this.entriesListBox_Validated);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // categoryLabel
            // 
            resources.ApplyResources(this.categoryLabel, "categoryLabel");
            this.categoryLabel.Name = "categoryLabel";
            // 
            // categoryComboBox
            // 
            resources.ApplyResources(this.categoryComboBox, "categoryComboBox");
            this.categoryComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.categoryComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.categoryComboBox.FormattingEnabled = true;
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Sorted = true;
            // 
            // SavedLayoutInformation
            // 
            this.AcceptButton = this.okButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.Controls.Add(this.categoryComboBox);
            this.Controls.Add(this.categoryLabel);
            this.Controls.Add(this.entriesListBox);
            this.Controls.Add(this.entriesLabel);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SavedLayoutInformation";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Label nameLabel;
    private System.Windows.Forms.Label descriptionLabel;
    private System.Windows.Forms.TextBox nameTextBox;
    private System.Windows.Forms.TextBox descriptionTextBox;
    private System.Windows.Forms.Label entriesLabel;
    private System.Windows.Forms.CheckedListBox entriesListBox;
    private System.Windows.Forms.ErrorProvider errorProvider;
    private System.Windows.Forms.ComboBox categoryComboBox;
    private System.Windows.Forms.Label categoryLabel;
    }
}