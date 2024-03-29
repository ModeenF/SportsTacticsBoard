﻿// Sports Tactics Board
//
// http://sportstacticsbd.sourceforge.net/
// http://sourceforge.net/projects/sportstacticsbd/
// 
// Sports Tactics Board is a utility that allows coaches, trainers and 
// officials to describe sports tactics, strategies and positioning using 
// a magnetic or chalk-board style approach.
// 
// Copyright (C) 2021- Fredrik Modéen
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
using SportsTacticsBoard.Resources;
using System;
using System.Windows.Forms;

namespace SportsTacticsBoard
{
    static class Program
    {
        const string CultureOption = "-culture";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            System.Globalization.CultureInfo? culture = null;
            ResourceManager resourceManager = ResourceManager.GetInstance();
            for (int index = 0; (index < args.Length); index++)
            {
                if (string.Equals(args[index], CultureOption, StringComparison.OrdinalIgnoreCase))
                {
                    index++;
                    if (index >= args.Length)
                    {
                        // Missing parameter
                        var msg = string.Format(System.Globalization.CultureInfo.CurrentCulture, resourceManager.LocalizationResource.MissingCultureOptionValueFormat, CultureOption);
                        RtlAwareMessageBox.Show(null, msg, resourceManager.LocalizationResource.InvalidParametersTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0);
                        return;
                    }

                    try
                    {
                        culture = new System.Globalization.CultureInfo(args[index]);
                    }
                    catch (ArgumentException)
                    {
                        var msg = string.Format(System.Globalization.CultureInfo.CurrentCulture, resourceManager.LocalizationResource.InvalidCultureOptionFormat, CultureOption, args[index]);
                        RtlAwareMessageBox.Show(null, msg, resourceManager.LocalizationResource.InvalidParametersTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0);
                        return;
                    }
                }
            }

            if (null != culture)
            {
                try
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = culture;
                    System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
                }
                catch (NotSupportedException)
                {
                    var msg = string.Format(System.Globalization.CultureInfo.CurrentCulture, resourceManager.LocalizationResource.InvalidCultureOptionFormat, CultureOption, culture.Name);
                    RtlAwareMessageBox.Show(null, msg, resourceManager.LocalizationResource.InvalidParametersTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0);
                    return;
                }
            }

            System.Diagnostics.Trace.TraceInformation("System.Threading.Thread.CurrentThread.CurrentCulture.Name={0}", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            System.Diagnostics.Trace.TraceInformation("System.Threading.Thread.CurrentThread.CurrentUICulture.Name={0}", System.Threading.Thread.CurrentThread.CurrentUICulture.Name);
            Application.EnableVisualStyles();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.Run(new MainForm());
        }
    }
}
