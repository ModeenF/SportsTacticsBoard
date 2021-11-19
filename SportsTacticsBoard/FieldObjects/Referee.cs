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
using System.Drawing;

namespace SportsTacticsBoard.FieldObjects
{
    public class Referee : Person
    {
        public override string Label { get; }

        public override string Tag { get; }

        protected override int LabelFontSize
        {
            get { return LabelText.Length > 1 ? 6 : 9; }
        }

        public Referee(string label, string tag, float posX, float posY, float dispRadius,
            Color? outlinePenColor = null, Color? fillBrushColor = null, Color? movementPenColor = null, Color? labelBrushColor = null)
            : base(posX, posY, dispRadius)
        {
            Label = label;
            Tag = tag;

            if (outlinePenColor == null)
                OutlinePenColor = Color.Black;
            else
                OutlinePenColor = outlinePenColor.Value;

            if (fillBrushColor == null)
                FillBrushColor = Color.Black;
            else
                FillBrushColor = fillBrushColor.Value;

            if (movementPenColor == null)
                MovementPenColor = Color.Black;
            else
                MovementPenColor = movementPenColor.Value;

            if (labelBrushColor == null)
                LabelBrushColor = Color.White;
            else
                LabelBrushColor = labelBrushColor.Value;
        }
    }
}