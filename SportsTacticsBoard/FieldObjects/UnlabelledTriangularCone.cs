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
using System.Drawing;

namespace SportsTacticsBoard.FieldObjects
{
    public class UnlabelledTriangularCone : BaseCone
    {
        public UnlabelledTriangularCone(int coneNumber, float posX, float posY, float dispRadius)
          : base(coneNumber, posX, posY, dispRadius)
        {
        }

        public override bool ShowsLabel
        {
            get { return false; }
        }

        public override void DrawAt(Graphics graphics, PointF pos)
        {
            var rect = GetRectangleAt(pos);
            PointF[] trianglePoints =
            {
                new PointF(pos.X, rect.Top),
                new PointF(rect.Left, rect.Bottom),
                new PointF(rect.Right, rect.Bottom),
                new PointF(pos.X, rect.Top)
            };

            using (var fillBrush = new SolidBrush(FillBrushColor))
                graphics.FillPolygon(fillBrush, trianglePoints);

            if (!(OutlinePenWidth > 0.0)) return;

            using (var outlinePen = new Pen(OutlinePenColor, OutlinePenWidth))
                graphics.DrawPolygon(outlinePen, trianglePoints);
        }
    }
}