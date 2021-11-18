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
using SportsTacticsBoard.Resources;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;

namespace SportsTacticsBoard
{
    public interface ICustomLabelProvider
    {
        string GetCustomLabel(string tag);
        void UpdateCustomLabel(string tag, string label);
        void RemoveCustomLabel(string tag);
    }

    public abstract class FieldObject
    {
        public ResourceManager resourceManager;

        public abstract string Tag { get; }
        public abstract string Label { get; }

        public ICustomLabelProvider CustomLabelProvider { get; set; }

        public PointF Position { get; set; }
        private float DisplayRadius { get; }
        public Color FillBrushColor { get; set; } = Color.White;
        public Color LabelBrushColor { get; set; } = Color.Black;
        public Color OutlinePenColor { get; set; } = Color.White;
        public Color MovementPenColor { get; set; } = Color.White;
        public float OutlinePenWidth { get; set; } = 1.0F;
        public float MovementPenWidth { get; set; } = 3.0F;

        protected FieldObject(float posX, float posY, float dispRadius)
        {
            Position = new PointF(posX, posY);
            DisplayRadius = dispRadius;
            resourceManager = ResourceManager.GetInstance();
        }

        protected virtual Collection<float> MovementPenDashPattern
        {
            get { return null; }
        }

        private SolidBrush LabelBrush
        {
            get { return new SolidBrush(LabelBrushColor); }
        }

        protected virtual int LabelFontSize
        {
            get { return 9; }
        }

        public virtual bool ShowsLabel
        {
            get { return true; }
        }

        private bool HasLabel
        {
            get { return !string.IsNullOrEmpty(LabelText); }
        }

        public virtual string Name
        {
            get
            {
                var propertyName = "FieldObject" + Tag;
                return (string)
                    typeof(LocalizationResource).GetProperties()
                        .First(p => p.Name == propertyName)
                        .GetGetMethod()
                        .Invoke(resourceManager.LocalizationResource, Array.Empty<object>());
            }
        }

        public bool ContainsPoint(PointF pt)
        {
            RectangleF rect = GetRectangle();
            return rect.Contains(pt);
        }

        public RectangleF GetRectangle()
        {
            return GetRectangleAt(Position);
        }

        public RectangleF GetRectangleAt(PointF pos)
        {
            return new RectangleF(pos.X - DisplayRadius,
              pos.Y - DisplayRadius, DisplayRadius * 2, DisplayRadius * 2);
        }

        public void Draw(Graphics graphics)
        {
            DrawAt(graphics, Position);
        }

        protected string LabelText
        {
            get
            {
                if (null == CustomLabelProvider)
                    return Label;
                
                var customLabel = CustomLabelProvider.GetCustomLabel(Tag);
                return customLabel ?? Label;
            }
        }

        public virtual void DrawAt(Graphics graphics, PointF pos)
        {
            if (null == graphics)
                throw new ArgumentNullException(nameof(graphics));

            var rectangle = GetRectangleAt(pos);
            using (var fillBrush = new SolidBrush(FillBrushColor))
                graphics.FillEllipse(fillBrush, rectangle);

            if (OutlinePenWidth > 0.0)
                using (var outlinePen = new Pen(OutlinePenColor, OutlinePenWidth))
                    graphics.DrawEllipse(outlinePen, rectangle);

            if (!HasLabel)
                return;

            var fontSize = LabelFontSize * (float)rectangle.Height / 18.0F;
            using (var labelFont = new Font("Arial", fontSize, FontStyle.Bold))
            {
                using (StringFormat strFormat = new StringFormat())
                {
                    strFormat.Alignment = StringAlignment.Center;
                    strFormat.LineAlignment = StringAlignment.Center;
                    graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    try
                    {
                        graphics.DrawString(LabelText, labelFont, LabelBrush, rectangle, strFormat);
                    }
                    catch (System.Runtime.InteropServices.ExternalException)
                    {
                        // Sometimes we get a "generic error" from the GDI+ subsystem
                        // when resizing the window really small and then slowly larger 
                        // again. All the parameters seem correct, so we'll just catch and
                        // ignore this exception here.
                    }
                }
            }
        }

        public void DrawMovementLine(Graphics graphics, PointF endPoint)
        {
            DrawMovementLineFrom(graphics, Position, endPoint);
        }

        public void DrawMovementLineFrom(Graphics graphics, PointF startPoint, PointF endPoint)
        {
            if (null == graphics)
                throw new ArgumentNullException(nameof(graphics));

            using (Pen movementPen = GetMovementPen())
                graphics.DrawLine(movementPen, startPoint, endPoint);
        }

        private Pen GetMovementPen()
        {
            Pen p = new Pen(MovementPenColor, MovementPenWidth)
            {
                DashStyle = System.Drawing.Drawing2D.DashStyle.Dash,
                EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor                
            };

            if (MovementPenDashPattern != null)
            {
                p.DashPattern = MovementPenDashPattern.ToArray();
            }

            return p;
        }
    }
}