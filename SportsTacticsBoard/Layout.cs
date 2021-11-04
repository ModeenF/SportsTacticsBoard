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
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;

namespace SportsTacticsBoard
{
  [XmlType(TypeName = "Layout")]
  public class FieldLayout
  {
    [XmlElement(ElementName = "Entry")]
    public Collection<LayoutEntry> Entries
    {
      get
      {
        return entries;
      }
    }
    private Collection<LayoutEntry> entries;

    public FieldLayout()
    {
      entries = new Collection<LayoutEntry>();
    }

    public void AddEntry(string tag, PointF pos, bool hidden)
    {
      entries.Add(new LayoutEntry(tag, pos, hidden));
    }

    public void AddEntry(string tag, float posX, float posY, bool hidden)
    {
      entries.Add(new LayoutEntry(tag, posX, posY, hidden));
    }

    public void AddEntry(string tag, PointF pos)
    {
      entries.Add(new LayoutEntry(tag, pos, false));
    }

    public void AddEntry(string tag, float posX, float posY)
    {
      entries.Add(new LayoutEntry(tag, posX, posY, false));
    }

    private LayoutEntry FindEntry(string tag)
    {
      foreach (LayoutEntry entry in entries) {
        if (entry.Tag == tag) {
          return entry;
        }
      }
      return null;
    }

    public bool HasEntry(string tag)
    {
      return (FindEntry(tag) != null);
    }

    public PointF GetEntryPosition(string tag)
    {
      LayoutEntry e = FindEntry(tag);
      return new PointF(e.PositionX, e.PositionY);
    }

    public void RemoveEntry(string tag)
    {
      // Not particularily efficient, but works
      entries.Remove(FindEntry(tag));
    }

    [XmlIgnore]
    public ICollection<string> Tags
    {
      get {
        List<string> l = new List<string>();
        foreach (LayoutEntry e in entries) {
          l.Add(e.Tag);
        }
        return l;
      }
    }
  }
}
