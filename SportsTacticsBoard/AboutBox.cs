// Sports Tactics Board
//
// http://sportstacticsbd.sourceforge.net/
// http://sourceforge.net/projects/sportstacticsbd/
// 
// Sports Tactics Board is a utility that allows coaches, trainers and 
// officials to describe sports tactics, strategies and positioning using 
// a magnetic or chalk-board style approach.
// 
// Copyright (C) 2006-2009 Robert Turner
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
using System.Reflection;
using System.Windows.Forms;

namespace SportsTacticsBoard
{
  public partial class AboutBox : Form
  {
    public AboutBox()
    {
      InitializeComponent();
      webSiteLinkLabel.Links[0].LinkData = 
        webSiteLinkLabel.Text.Substring(webSiteLinkLabel.Links[0].Start, webSiteLinkLabel.Links[0].Length);
    }

    private void AboutBox_Load(object sender, EventArgs e)
    {
      versionLabel.Text = typeof(Program).Assembly.GetName().Version.ToString();
      object[] copyrightAttributes = typeof(Program).Assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
      if (copyrightAttributes.Length > 0) {
        copyrightLabel.Text = ((AssemblyCopyrightAttribute)copyrightAttributes[0]).Copyright;
      }
      object[] titleAttributes = typeof(Program).Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
      if (titleAttributes.Length > 0) {
        titleLabel.Text = ((AssemblyTitleAttribute)titleAttributes[0]).Title;
      }
    }

    private void webSiteLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
      string url = e.Link.LinkData as string;
      System.Diagnostics.Process.Start(url);
      this.webSiteLinkLabel.Links[webSiteLinkLabel.Links.IndexOf(e.Link)].Visited = true;
    }
  }
}