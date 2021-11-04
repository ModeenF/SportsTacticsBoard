// Sports Tactics Board
//
// http://sportstacticsbd.sourceforge.net/
// http://sourceforge.net/projects/sportstacticsbd/
// 
// Sports Tactics Board is a utility that allows coaches, trainers and 
// officials to describe sports tactics, strategies and positioning using 
// a magnetic or chalk-board style approach.
// 
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
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SportsTacticsBoard
{
  [XmlType(TypeName = "SavedLayout")]
  public class SavedLayout
  {
    private string fieldTypeTag;
    private string name;
    private string category;
    private string description;
    private FieldLayout layout;

    [XmlElement(ElementName = "FieldTypeTag")]
    public string FieldTypeTag
    {
      get { return fieldTypeTag; }
      set { fieldTypeTag = value; }
    }

    [XmlElement(ElementName = "Name")]
    public string Name
    {
      get { return name; }
      set { name = value; }
    }

    [XmlElement(ElementName = "Category")]
    public string Category
    {
      get { return category; }
      set { category = value; }
    }

    [XmlElement(ElementName = "Description")]
    public string Description
    {
      get { return description; }
      set { description = value; }
    }

    [XmlElement(ElementName = "Layout")]
    public FieldLayout Layout
    {
      get { return layout; }
      set { layout = value; }
    }

    public SavedLayout()
    {
      name = "";
      category = "";
      description = "";
      layout = new FieldLayout();
      fieldTypeTag = "";
    }

    public SavedLayout(string name, string category, string description, FieldLayout layout, string fieldTypeTag)
    {
      this.name = name;
      this.category = category;
      this.description = description;
      this.layout = layout;
      this.fieldTypeTag = fieldTypeTag;
    }
  }
}
