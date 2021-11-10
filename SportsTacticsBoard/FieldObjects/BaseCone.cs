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
// Copyright (C) 2010 Robert Turner
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
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

namespace SportsTacticsBoard.FieldObjects
{
    public abstract class BaseCone : FieldObject
    {
        public int ConeNumber { get; }

        public override string Label { get; }

        public override string Tag
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Cone_{0}", ConeNumber); }
        }

        public override string Name { get; }

        protected override int LabelFontSize
        {
            get { return 8; }
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

        protected BaseCone(int coneNumber, float posX, float posY, float dispRadius)
                : base(posX, posY, dispRadius)
        {
            ConeNumber = coneNumber;
            OutlinePenColor = Color.Black;
            FillBrushColor = Color.Orange;
            MovementPenWidth = 1.5F;
            MovementPenColor = Color.Orange;

            Label = string.Format(CultureInfo.CurrentUICulture,
                ResourceManager.LocalizationResource.FieldObjectBaseConeLabelFormat, coneNumber);
            Name = string.Format(CultureInfo.CurrentUICulture,
                ResourceManager.LocalizationResource.FieldObjectBaseConeNameFormat, coneNumber);
        }
    }
}