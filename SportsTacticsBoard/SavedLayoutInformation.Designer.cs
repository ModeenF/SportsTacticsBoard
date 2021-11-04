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
      this.okButton.AccessibleDescription = null;
      this.okButton.AccessibleName = null;
      resources.ApplyResources(this.okButton, "okButton");
      this.okButton.BackgroundImage = null;
      this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.errorProvider.SetError(this.okButton, resources.GetString("okButton.Error"));
      this.okButton.Font = null;
      this.errorProvider.SetIconAlignment(this.okButton, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("okButton.IconAlignment"))));
      this.errorProvider.SetIconPadding(this.okButton, ((int)(resources.GetObject("okButton.IconPadding"))));
      this.okButton.Name = "okButton";
      this.okButton.UseVisualStyleBackColor = true;
      // 
      // cancelButton
      // 
      this.cancelButton.AccessibleDescription = null;
      this.cancelButton.AccessibleName = null;
      resources.ApplyResources(this.cancelButton, "cancelButton");
      this.cancelButton.BackgroundImage = null;
      this.cancelButton.CausesValidation = false;
      this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.errorProvider.SetError(this.cancelButton, resources.GetString("cancelButton.Error"));
      this.cancelButton.Font = null;
      this.errorProvider.SetIconAlignment(this.cancelButton, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cancelButton.IconAlignment"))));
      this.errorProvider.SetIconPadding(this.cancelButton, ((int)(resources.GetObject("cancelButton.IconPadding"))));
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.UseVisualStyleBackColor = true;
      // 
      // nameLabel
      // 
      this.nameLabel.AccessibleDescription = null;
      this.nameLabel.AccessibleName = null;
      resources.ApplyResources(this.nameLabel, "nameLabel");
      this.errorProvider.SetError(this.nameLabel, resources.GetString("nameLabel.Error"));
      this.nameLabel.Font = null;
      this.errorProvider.SetIconAlignment(this.nameLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("nameLabel.IconAlignment"))));
      this.errorProvider.SetIconPadding(this.nameLabel, ((int)(resources.GetObject("nameLabel.IconPadding"))));
      this.nameLabel.Name = "nameLabel";
      // 
      // descriptionLabel
      // 
      this.descriptionLabel.AccessibleDescription = null;
      this.descriptionLabel.AccessibleName = null;
      resources.ApplyResources(this.descriptionLabel, "descriptionLabel");
      this.errorProvider.SetError(this.descriptionLabel, resources.GetString("descriptionLabel.Error"));
      this.descriptionLabel.Font = null;
      this.errorProvider.SetIconAlignment(this.descriptionLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("descriptionLabel.IconAlignment"))));
      this.errorProvider.SetIconPadding(this.descriptionLabel, ((int)(resources.GetObject("descriptionLabel.IconPadding"))));
      this.descriptionLabel.Name = "descriptionLabel";
      // 
      // nameTextBox
      // 
      this.nameTextBox.AccessibleDescription = null;
      this.nameTextBox.AccessibleName = null;
      resources.ApplyResources(this.nameTextBox, "nameTextBox");
      this.nameTextBox.BackgroundImage = null;
      this.errorProvider.SetError(this.nameTextBox, resources.GetString("nameTextBox.Error"));
      this.nameTextBox.Font = null;
      this.errorProvider.SetIconAlignment(this.nameTextBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("nameTextBox.IconAlignment"))));
      this.errorProvider.SetIconPadding(this.nameTextBox, ((int)(resources.GetObject("nameTextBox.IconPadding"))));
      this.nameTextBox.Name = "nameTextBox";
      this.nameTextBox.Validated += new System.EventHandler(this.nameTextBox_Validated);
      this.nameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.nameTextBox_Validating);
      // 
      // descriptionTextBox
      // 
      this.descriptionTextBox.AccessibleDescription = null;
      this.descriptionTextBox.AccessibleName = null;
      resources.ApplyResources(this.descriptionTextBox, "descriptionTextBox");
      this.descriptionTextBox.BackgroundImage = null;
      this.errorProvider.SetError(this.descriptionTextBox, resources.GetString("descriptionTextBox.Error"));
      this.descriptionTextBox.Font = null;
      this.errorProvider.SetIconAlignment(this.descriptionTextBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("descriptionTextBox.IconAlignment"))));
      this.errorProvider.SetIconPadding(this.descriptionTextBox, ((int)(resources.GetObject("descriptionTextBox.IconPadding"))));
      this.descriptionTextBox.Name = "descriptionTextBox";
      // 
      // entriesLabel
      // 
      this.entriesLabel.AccessibleDescription = null;
      this.entriesLabel.AccessibleName = null;
      resources.ApplyResources(this.entriesLabel, "entriesLabel");
      this.errorProvider.SetError(this.entriesLabel, resources.GetString("entriesLabel.Error"));
      this.entriesLabel.Font = null;
      this.errorProvider.SetIconAlignment(this.entriesLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("entriesLabel.IconAlignment"))));
      this.errorProvider.SetIconPadding(this.entriesLabel, ((int)(resources.GetObject("entriesLabel.IconPadding"))));
      this.entriesLabel.Name = "entriesLabel";
      // 
      // entriesListBox
      // 
      this.entriesListBox.AccessibleDescription = null;
      this.entriesListBox.AccessibleName = null;
      resources.ApplyResources(this.entriesListBox, "entriesListBox");
      this.entriesListBox.BackgroundImage = null;
      this.entriesListBox.CheckOnClick = true;
      this.errorProvider.SetError(this.entriesListBox, resources.GetString("entriesListBox.Error"));
      this.entriesListBox.Font = null;
      this.errorProvider.SetIconAlignment(this.entriesListBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("entriesListBox.IconAlignment"))));
      this.errorProvider.SetIconPadding(this.entriesListBox, ((int)(resources.GetObject("entriesListBox.IconPadding"))));
      this.entriesListBox.Name = "entriesListBox";
      this.entriesListBox.Sorted = true;
      this.entriesListBox.Validating += new System.ComponentModel.CancelEventHandler(this.entriesListBox_Validating);
      this.entriesListBox.Validated += new System.EventHandler(this.entriesListBox_Validated);
      // 
      // errorProvider
      // 
      this.errorProvider.ContainerControl = this;
      resources.ApplyResources(this.errorProvider, "errorProvider");
      // 
      // categoryLabel
      // 
      this.categoryLabel.AccessibleDescription = null;
      this.categoryLabel.AccessibleName = null;
      resources.ApplyResources(this.categoryLabel, "categoryLabel");
      this.errorProvider.SetError(this.categoryLabel, resources.GetString("categoryLabel.Error"));
      this.categoryLabel.Font = null;
      this.errorProvider.SetIconAlignment(this.categoryLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("categoryLabel.IconAlignment"))));
      this.errorProvider.SetIconPadding(this.categoryLabel, ((int)(resources.GetObject("categoryLabel.IconPadding"))));
      this.categoryLabel.Name = "categoryLabel";
      // 
      // categoryComboBox
      // 
      this.categoryComboBox.AccessibleDescription = null;
      this.categoryComboBox.AccessibleName = null;
      resources.ApplyResources(this.categoryComboBox, "categoryComboBox");
      this.categoryComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.categoryComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.categoryComboBox.BackgroundImage = null;
      this.errorProvider.SetError(this.categoryComboBox, resources.GetString("categoryComboBox.Error"));
      this.categoryComboBox.Font = null;
      this.categoryComboBox.FormattingEnabled = true;
      this.errorProvider.SetIconAlignment(this.categoryComboBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("categoryComboBox.IconAlignment"))));
      this.errorProvider.SetIconPadding(this.categoryComboBox, ((int)(resources.GetObject("categoryComboBox.IconPadding"))));
      this.categoryComboBox.Name = "categoryComboBox";
      this.categoryComboBox.Sorted = true;
      // 
      // SavedLayoutInformation
      // 
      this.AcceptButton = this.okButton;
      this.AccessibleDescription = null;
      this.AccessibleName = null;
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackgroundImage = null;
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
      this.Font = null;
      this.Icon = null;
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