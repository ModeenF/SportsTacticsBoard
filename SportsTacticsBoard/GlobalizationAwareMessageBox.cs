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
using System;
using System.Globalization;
using System.Windows.Forms;

namespace SportsTacticsBoard
{
  // Code for this class is from Microsoft documentation on resolving FxCop error:
  //       Help         : http://www.gotdotnet.com/team/fxcop/docs/rules.aspx?version=1.35&url=/Globalization/SpecifyMessageBoxOptions.html  (String)
  //       Category     : Microsoft.Globalization  (String)
  //       CheckId      : CA1300  (String)
  //       RuleFile     : Globalization Rules  (String)
  //       Info         : "In order to run correctly on right-to-left systems, 
  //                      all calls to MessageBox.Show should use the overload 
  //                      that specifies MessageBoxOptions as an argument. Programs 
  //                      should detect whether they are running on a right-to-left 
  //                      system at run-time and pass the appropriate MessageBoxOptions 
  //                      value in order to display correctly."
  //
  // URL resolves to:
  //  http://msdnwiki.microsoft.com/en-us/mtpswiki/9357a724-026e-4a3d-a03a-f14635064ec6.aspx
  //
  public static class GlobalizationAwareMessageBox
  {

    public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
    {

      if (IsRightToLeft(owner)) {
        options |= MessageBoxOptions.RtlReading |
                   MessageBoxOptions.RightAlign;
      }

      return MessageBox.Show(owner, text, caption,
                             buttons, icon, defaultButton, options);
    }

    private static bool IsRightToLeft(IWin32Window owner)
    {

      Control control = owner as Control;
      if (control != null) {
        return control.RightToLeft == RightToLeft.Yes;
      }

      // If no parent control is available, ask the CurrentUICulture
      // if we are running under right-to-left.
      return CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft;
    }
  }
}