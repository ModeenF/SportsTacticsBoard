// Sports Tactics Board
//
// http://sportstacticsbd.sourceforge.net/
// http://sourceforge.net/projects/sportstacticsbd/
// 
// Sports Tactics Board is a utility that allows coaches, trainers and 
// officials to describe sports tactics, strategies and positioning using 
// a magnetic or chalk-board style approach.
// 
// Copyright (C) 2016 Marian Dziubiak
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

using System.Collections.Generic;
using System.Drawing;

namespace SportsTacticsBoard.FieldObjects
{
    public class Ball : FieldObject
    {
        public override string Label { get; }
        public override string Tag { get; }

        public override bool ShowsLabel
        {
            get { return false; }
        }

        protected override List<float> MovementPenDashPattern
        {
            get
            {
                var list = new List<float>();
                list.Add(3.0F);
                list.Add(2.0F);
                return list;
            }
        }

        public Ball(float posX, float posY, float dispRadius)
            : this(string.Empty, "Ball", posX, posY, dispRadius)
        {
            OutlinePenColor = Color.White;
        }

        public Ball(string label, string tag, float posX, float posY, float dispRadius)
            : base(posX, posY, dispRadius)
        {
            OutlinePenColor = Color.Black;
            FillBrushColor = Color.White;
            Label = label;
            Tag = tag;
        }
    }
}