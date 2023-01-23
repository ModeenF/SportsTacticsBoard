// Sports Tactics Board
//
// http://sportstacticsbd.sourceforge.net/
// http://sourceforge.net/projects/sportstacticsbd/
// 
// Sports Tactics Board is a utility that allows coaches, trainers and 
// officials to describe sports tactics, strategies and positioning using 
// a magnetic or chalk-board style approach.
// 
// Copyright (C) 2023 - Fredrik Modéen
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

// NuGet packages:
// Microsoft.Extensions.Configuration.Binder
// Microsoft.Extensions.Configuration.Json

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;

namespace SportsTacticsBoard.Classes
{
    public class Appsettings
    {
        private static string file = "appsettings.json";
        private static bool reRead = false;
        private static SportsTacticsBoardSettings? settings;

        public static SportsTacticsBoardSettings? Settings
        {
            get
            {
                return ReadSettings();
            }
        }

        public static SportsTacticsBoardSettings? ReadSettings()
        {
            if (settings == null || reRead)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(file, optional: false);

                IConfiguration config = builder.Build();
                var tmp = config.GetSection("appsettings").Get<SportsTacticsBoardSettings>();

                if (tmp != null)
                    settings = tmp;
            }

            return settings;
        }

        public static void AddOrUpdateAppSetting<T>(string key, T? value)
        {
            if (value == null)
            {
                return;
            }

            try
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, file);
                string json = File.ReadAllText(filePath);
                dynamic? jsonObj = JsonConvert.DeserializeObject(json);

                if (jsonObj == null)
                    return;

                var sectionPath = key.Split(":")[0];
                if (!string.IsNullOrEmpty(sectionPath))
                {
                    var keyPath = key.Split(":")[1];
                    jsonObj[sectionPath][keyPath] = value;
                }
                else
                {
                    jsonObj[sectionPath] = value; // if no sectionpath just set the value
                }

                string output = JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, output);
                reRead = true;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
    }
}
