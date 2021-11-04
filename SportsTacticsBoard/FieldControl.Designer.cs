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
namespace SportsTacticsBoard
{
  partial class FieldControl
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
      if (disposing) {
        if (components != null) {
          components.Dispose();
        }
        if (fieldToDisplayTransform != null) {
          fieldToDisplayTransform.Dispose();
        }
        if (displayToFieldTransfom != null) {
          displayToFieldTransfom.Dispose();
        }
        if (null != zoomCursor) {
          zoomCursor.Dispose();
        }
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FieldControl));
      this.fieldObjectContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.changeLabelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolTip = new System.Windows.Forms.ToolTip(this.components);
      this.fieldObjectContextMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // fieldObjectContextMenu
      // 
      this.fieldObjectContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeLabelMenuItem});
      this.fieldObjectContextMenu.Name = "fieldObjectContextMenu";
      resources.ApplyResources(this.fieldObjectContextMenu, "fieldObjectContextMenu");
      // 
      // changeLabelMenuItem
      // 
      this.changeLabelMenuItem.Name = "changeLabelMenuItem";
      resources.ApplyResources(this.changeLabelMenuItem, "changeLabelMenuItem");
      this.changeLabelMenuItem.Click += new System.EventHandler(this.changeLabelMenuItem_Click);
      // 
      // FieldControl
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.DoubleBuffered = true;
      this.Name = "FieldControl";
      this.fieldObjectContextMenu.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ContextMenuStrip fieldObjectContextMenu;
    private System.Windows.Forms.ToolStripMenuItem changeLabelMenuItem;
    private System.Windows.Forms.ToolTip toolTip;
  }
}
