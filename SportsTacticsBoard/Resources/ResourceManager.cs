// Sports Tactics Board
//
// http://github.com/manio143/SportsTacticsBoard
// 
// Sports Tactics Board is a utility that allows coaches, trainers and 
// officials to describe sports tactics, strategies and positioning using 
// a magnetic or chalk-board style approach.
// 
// Copyright (C) 2021- Fredrik Modéen
// Copyright (C) 2016 Marian Dziubiak
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
using SportsTacticsBoard.Classes;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using YamlDotNet.Serialization;

namespace SportsTacticsBoard.Resources
{
    public sealed class ResourceManager
    {
        private const string LocalizationResourceFileName = "localization.";
        private const string ResourceExtension = ".res";
        private const string ImageResourcePath = "SportsTacticsBoard.Images.";
        private string PathToResources = AppDomain.CurrentDomain.BaseDirectory + @"/res";

        private string? FilePath { get; set; }

        private static ResourceManager instance;
        private static readonly object padlock = new object();

        public string DefaultCulture { get; set; }
        public LocalizationResource LocalizationResource { get; set; }
        public ImagesResource ImagesResource { get; set; }

        public static ResourceManager GetInstance(string? culture = null)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ResourceManager(culture ?? Appsettings.Settings?.DefaultLanguage);
                }

                return instance;
            }
        }

        public ResourceManager(string? culture)
        {
            SetLocal(culture ?? "en-US");
        }

        public void SetLocal(string? culture)
        {
            string cultureLocal = culture == null ? DefaultCulture : culture;
            if (!cultureLocal.Equals(DefaultCulture))
            {
                DefaultCulture = cultureLocal;
            }

            FilePath = PathToResources + "/" + LocalizationResourceFileName + cultureLocal + ResourceExtension;
            LocalizationResource = DeserializeLocalizationResource(File.Exists(FilePath) ? cultureLocal : DefaultCulture);
            ImagesResource = GetImagesFromAssembly();
        }

        private LocalizationResource? DeserializeLocalizationResource(string culture)
        {
            if (File.Exists(FilePath))
            {
                var localResource = YamlDeserialize<LocalizationResource>(FilePath);

                if (culture != DefaultCulture)
                {
                    string pathToDefaultResource = PathToResources + "/" + LocalizationResourceFileName + DefaultCulture + ResourceExtension;
                    if (File.Exists(pathToDefaultResource))
                    {
                        var defaultLocalizationResource = YamlDeserialize<LocalizationResource>(pathToDefaultResource);

                        //Add default values to empty fields
                        foreach (var propertyInfo in typeof(LocalizationResource).GetProperties())
                        {
                            if (propertyInfo.GetGetMethod()?.Invoke(localResource, new object[0]) == null)
                                propertyInfo.GetSetMethod()?.Invoke(localResource, new[]
                                {
                                    propertyInfo.GetGetMethod()?.Invoke(defaultLocalizationResource, new object[0])
                                });
                        }
                    }
                }
                return localResource;
            }
            return null;
        }

        private static ImagesResource GetImagesFromAssembly()
        {
            return new ImagesResource()
            {
                DataContainerMoveFirstHs = new Bitmap(GetStreamForImage("DataContainer_MoveFirstHS.png")),
                DataContainerMoveLastHs = new Bitmap(GetStreamForImage("DataContainer_MoveLastHS.png")),
                DataContainerMoveNextHs = new Bitmap(GetStreamForImage("DataContainer_MoveNextHS.png")),
                DataContainerMovePreviousHs = new Bitmap(GetStreamForImage("DataContainer_MovePreviousHS.png")),
                DeleteHs = new Bitmap(GetStreamForImage("DeleteHS.png")),
                EditUndoHs = new Bitmap(GetStreamForImage("Edit_UndoHS.png")),
                GoToNextHs = new Bitmap(GetStreamForImage("GoToNextHS.png")),
                GoToPrevious = new Bitmap(GetStreamForImage("GoToPrevious.png")),
                Help = new Bitmap(GetStreamForImage("Help.png")),
                NavBack = new Bitmap(GetStreamForImage("NavBack.png")),
                NavForward = new Bitmap(GetStreamForImage("NavForward.png")),
                NewDocumentHs = new Bitmap(GetStreamForImage("NewDocumentHS.png")),
                OpenHs = new Bitmap(GetStreamForImage("openHS.png")),
                PauseHs = new Bitmap(GetStreamForImage("PauseHS.png")),
                PlayHs = new Bitmap(GetStreamForImage("PlayHS.png")),
                RecordHs = new Bitmap(GetStreamForImage("RecordHS.png")),
                RepeatHs = new Bitmap(GetStreamForImage("RepeatHS.png")),
                RestartHs = new Bitmap(GetStreamForImage("RestartHS.png")),
                SaveHs = new Bitmap(GetStreamForImage("saveHS.png"))
                //Zoom = new Bitmap(GetStreamForImage("Zoom.cur")) //Can't load a .cur file
            };
        }

        private static Stream GetStreamForImage(string name)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(ImageResourcePath + name);
        }

        private static T YamlDeserialize<T>(string path)
        {
            using (var stream = File.OpenRead(path))
            using (var streamReader = new StreamReader(stream))
                return new Deserializer().Deserialize<T>(streamReader);
        }
    }
}
