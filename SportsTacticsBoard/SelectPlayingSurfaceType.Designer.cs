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
  partial class SelectPlayingSurfaceType
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
      if (disposing && (components != null)) {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectPlayingSurfaceType));
      this.okButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.fieldTypeComboBox = new System.Windows.Forms.ComboBox();
      this.saveAsDefaultCheckBox = new System.Windows.Forms.CheckBox();
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
      // label1
      // 
      resources.ApplyResources(this.label1, "label1");
      this.label1.Name = "label1";
      // 
      // fieldTypeComboBox
      // 
      resources.ApplyResources(this.fieldTypeComboBox, "fieldTypeComboBox");
      this.fieldTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.fieldTypeComboBox.FormattingEnabled = true;
      this.fieldTypeComboBox.Name = "fieldTypeComboBox";
      this.fieldTypeComboBox.Sorted = true;
      // 
      // saveAsDefaultCheckBox
      // 
      resources.ApplyResources(this.saveAsDefaultCheckBox, "saveAsDefaultCheckBox");
      this.saveAsDefaultCheckBox.Name = "saveAsDefaultCheckBox";
      this.saveAsDefaultCheckBox.UseVisualStyleBackColor = true;
      // 
      // SelectPlayingSurfaceType
      // 
      this.AcceptButton = this.okButton;
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cancelButton;
      this.Controls.Add(this.saveAsDefaultCheckBox);
      this.Controls.Add(this.fieldTypeComboBox);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.okButton);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SelectPlayingSurfaceType";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox fieldTypeComboBox;
    private System.Windows.Forms.CheckBox saveAsDefaultCheckBox;
  }
}