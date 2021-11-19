// Sports Tactics Board
//
// http://sportstacticsbd.sourceforge.net/
// http://sourceforge.net/projects/sportstacticsbd/
// 
// Sports Tactics Board is a utility that allows coaches, trainers and 
// officials to describe sports tactics, strategies and positioning using 
// a magnetic or chalk-board style approach.
// 
// Copyright (C) 2021- Fredrik Modéen
// Copyright (C) 2006-2007 Robert Turner
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
using System.Xml.Serialization;

namespace SportsTacticsBoard
{
    [XmlType(TypeName = "SavedLayout")]
    public class SavedLayout : IEquatable<SavedLayout>
    {
        [XmlElement(ElementName = "FieldTypeTag")]
        public string FieldTypeTag { get; set; }

        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "Category")]
        public string Category { get; set; }

        [XmlElement(ElementName = "Description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "Layout")]
        public FieldLayout Layout { get; set; }

        public SavedLayout()
        {
            Name = "";
            Category = "";
            Description = "";
            Layout = new FieldLayout();
            FieldTypeTag = "";
        }

        public SavedLayout(string name, string category, string description, FieldLayout layout, string fieldTypeTag)
        {
            Name = name;
            Category = category;
            Description = description;
            Layout = layout;
            FieldTypeTag = fieldTypeTag;
        }

        public bool Equals(SavedLayout other)
        {
            // Would still want to check for null etc. first.
            return this.Category == other.Category &&
                   this.Name == other.Name &&
                   this.Description == other.Description &&
                   this.FieldTypeTag == other.FieldTypeTag;
        }
    }
}
