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
  partial class AboutBox
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
      this.titleLabel = new System.Windows.Forms.Label();
      this.copyrightLabel = new System.Windows.Forms.Label();
      this.versionLabel = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.licenseTextBox = new System.Windows.Forms.TextBox();
      this.webSiteLinkLabel = new System.Windows.Forms.LinkLabel();
      this.SuspendLayout();
      // 
      // titleLabel
      // 
      this.titleLabel.AccessibleDescription = null;
      this.titleLabel.AccessibleName = null;
      resources.ApplyResources(this.titleLabel, "titleLabel");
      this.titleLabel.Name = "titleLabel";
      // 
      // copyrightLabel
      // 
      this.copyrightLabel.AccessibleDescription = null;
      this.copyrightLabel.AccessibleName = null;
      resources.ApplyResources(this.copyrightLabel, "copyrightLabel");
      this.copyrightLabel.Font = null;
      this.copyrightLabel.Name = "copyrightLabel";
      // 
      // versionLabel
      // 
      this.versionLabel.AccessibleDescription = null;
      this.versionLabel.AccessibleName = null;
      resources.ApplyResources(this.versionLabel, "versionLabel");
      this.versionLabel.Font = null;
      this.versionLabel.Name = "versionLabel";
      // 
      // label3
      // 
      this.label3.AccessibleDescription = null;
      this.label3.AccessibleName = null;
      resources.ApplyResources(this.label3, "label3");
      this.label3.Font = null;
      this.label3.Name = "label3";
      // 
      // licenseTextBox
      // 
      this.licenseTextBox.AccessibleDescription = null;
      this.licenseTextBox.AccessibleName = null;
      resources.ApplyResources(this.licenseTextBox, "licenseTextBox");
      this.licenseTextBox.BackgroundImage = null;
      this.licenseTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.licenseTextBox.CausesValidation = false;
      this.licenseTextBox.Font = null;
      this.licenseTextBox.Name = "licenseTextBox";
      this.licenseTextBox.ReadOnly = true;
      this.licenseTextBox.TabStop = false;
      // 
      // webSiteLinkLabel
      // 
      this.webSiteLinkLabel.AccessibleDescription = null;
      this.webSiteLinkLabel.AccessibleName = null;
      resources.ApplyResources(this.webSiteLinkLabel, "webSiteLinkLabel");
      this.webSiteLinkLabel.Font = null;
      this.webSiteLinkLabel.Name = "webSiteLinkLabel";
      this.webSiteLinkLabel.TabStop = true;
      this.webSiteLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.webSiteLinkLabel_LinkClicked);
      // 
      // AboutBox
      // 
      this.AccessibleDescription = null;
      this.AccessibleName = null;
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackgroundImage = null;
      this.Controls.Add(this.webSiteLinkLabel);
      this.Controls.Add(this.licenseTextBox);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.versionLabel);
      this.Controls.Add(this.copyrightLabel);
      this.Controls.Add(this.titleLabel);
      this.Font = null;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = null;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "AboutBox";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.Load += new System.EventHandler(this.AboutBox_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label titleLabel;
    private System.Windows.Forms.Label copyrightLabel;
    private System.Windows.Forms.Label versionLabel;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox licenseTextBox;
    private System.Windows.Forms.LinkLabel webSiteLinkLabel;
  }
}