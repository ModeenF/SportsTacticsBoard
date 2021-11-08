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
using System;
using System.Drawing;
using System.Xml.Serialization;

namespace SportsTacticsBoard
{
    [XmlType(TypeName = "LayoutEntry")]
    public class LayoutEntry : IEquatable<LayoutEntry>
    {
        public LayoutEntry()
        {
            Tag = string.Empty;
        }

        public LayoutEntry(string tag, float posX, float posY, bool hidden)
        {
            Tag = tag;
            PositionX = posX;
            PositionY = posY;
            Hidden = hidden;
        }

        public LayoutEntry(string tag, PointF pt, bool hidden)
        {
            Tag = tag;
            PositionX = pt.X;
            PositionY = pt.Y;
            Hidden = hidden;
        }

        [XmlAttribute(AttributeName = "tag")]
        public string Tag { get; set; }

        [XmlAttribute(AttributeName = "x")]
        public float PositionX { get; set; }

        [XmlAttribute(AttributeName = "y")]
        public float PositionY { get; set; }

        [XmlAttribute(AttributeName = "hidden")]
        public bool Hidden { get; set; }

        public bool Equals(LayoutEntry other)
        {
            // Would still want to check for null etc. first.
            return this.Tag == other.Tag &&
                   this.PositionX == other.PositionX &&
                   this.PositionY == other.PositionY &&
                   this.Hidden == other.Hidden;
        }
    }
}
