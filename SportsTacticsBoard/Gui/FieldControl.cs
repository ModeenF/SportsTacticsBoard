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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SportsTacticsBoard
{
    internal enum Tool
    {
        MoveObjects,
        Move,
        Zoom,
    }

    internal enum ToolMode
    {
        MultiUse,
        RevertAfterSingleUse
    }

    partial class FieldControl : UserControl
    {
        private Matrix fieldToDisplayTransform;
        private Matrix displayToFieldTransfom;

        private PointF zoomPoint;

        private float zoomFactor = 1.0F;

        private float rotationAngle;

        private bool viewDirty;
        private bool dirty;
        private IPlayingSurfaceType fieldType;
        private bool allowInteraction = true;
        private Tool previousTool = Tool.MoveObjects;
        private Tool tool = Tool.MoveObjects;
        private ToolMode toolMode = ToolMode.MultiUse;

        private Cursor zoomCursor;

        public bool AllowInteraction
        {
            get { return allowInteraction; }
            set
            {
                if (value != allowInteraction)
                {
                    allowInteraction = value;
                    this.Cursor = (allowInteraction) ? ToolCursor : Cursors.No;
                    if (!allowInteraction)
                    {
                        ClearToolTip();
                    }
                }
            }
        }

        public Tool Tool
        {
            get { return tool; }
            set
            {
                if (value != tool)
                {
                    previousTool = tool;
                    CancelDrag();
                    tool = value;
                    this.Cursor = ToolCursor;
                }
            }
        }

        public ToolMode ToolMode
        {
            get { return toolMode; }
            set { toolMode = value; }
        }

        private void RevertTool()
        {
            ToolMode = ToolMode.MultiUse;
            Tool = previousTool;
        }

        private Cursor ToolCursor
        {
            get
            {
                switch (Tool)
                {
                    case Tool.MoveObjects:
                    default:
                        return Cursors.Arrow;
                    case Tool.Move:
                        return Cursors.Hand;
                    case Tool.Zoom:
                        return zoomCursor;
                }
            }
        }

        public bool IsDirty
        {
            get { return dirty; }
            set
            {
                dirty = value;
                if (null != IsDirtyChanged)
                {
                    IsDirtyChanged.Invoke(this, null);
                }
            }
        }

        public bool IsViewDirty
        {
            get { return viewDirty; }
            set
            {
                viewDirty = value;
                if (null != IsViewDirtyChanged)
                {
                    IsViewDirtyChanged.Invoke(this, null);
                }
            }
        }

        public PointF ZoomPoint
        {
            get { return zoomPoint; }
        }

        public float ZoomFactor
        {
            get { return zoomFactor; }
        }

        public float RotationAngle
        {
            get { return rotationAngle; }
        }

        public void SetView(PointF newZoomPoint, float newZoomFactor, float newRotationAngle)
        {
            zoomPoint = newZoomPoint;
            zoomFactor = newZoomFactor;
            rotationAngle = newRotationAngle;
            CalculateFieldGeometry(Size);
            IsViewDirty = true;
            Invalidate();
        }

        public void ResetView()
        {
            ResetZoom();
            CalculateFieldGeometry(Size);
            IsViewDirty = true;
            Invalidate();
        }

        public ICustomLabelProvider CustomLabelProvider { get; set; }

        public event EventHandler IsDirtyChanged;
        public event EventHandler IsViewDirtyChanged;

        private bool showMovementLines = true;

        public bool ShowMovementLines
        {
            get
            {
                return showMovementLines;
            }
            set
            {
                showMovementLines = value;
                Invalidate();
            }
        }

        public FieldControl()
        {
            InitializeComponent();
            using (var s = new System.IO.MemoryStream(Properties.Resources.zoom, false))
            {
                zoomCursor = new Cursor(s);
            }
            this.Cursor = ToolCursor;
        }

        public IPlayingSurfaceType FieldType
        {
            set
            {
                nextLayout = null;
                fieldObjects = null;

                fieldType = value;
                if (null != fieldType)
                {
                    BackColor = fieldType.SurfaceColor;
                    ResetZoomAndRotation();
                    CalculateFieldGeometry(Size);
                    fieldObjects = fieldType.StandardObjects;
                    foreach (var fo in fieldObjects)
                    {
                        fo.CustomLabelProvider = CustomLabelProvider;
                    }
                    SetLayout(fieldType.DefaultLayout);
                }
                else
                {
                    ResetZoomAndRotation();
                }

                Invalidate();
            }
            get { return fieldType; }
        }

        private void ResetZoomAndRotation()
        {
            ResetZoom();
            rotationAngle = 0.0F;
        }

        private void ResetZoom()
        {
            var ft = FieldType;
            if (null != ft)
            {
                zoomPoint = new PointF(ft.Length / 2.0F, ft.Width / 2.0F);
            }
            else
            {
                zoomPoint = new PointF(0.0F, 0.0F);
            }
            zoomFactor = 1.0F;
        }

        private Collection<FieldObject>? fieldObjects;

        static public FieldLayout ConvertFieldObjectsToLayout(ICollection<FieldObject>? fieldObjects)
        {
            FieldLayout layout = new FieldLayout();
            foreach (FieldObject fo in fieldObjects)
            {
                layout.AddEntry(fo.Tag, fo.Position);
            }
            return layout;
        }

        public FieldLayout FieldLayout
        {
            get
            {
                return ConvertFieldObjectsToLayout(fieldObjects);
            }
        }

        public ICollection<FieldObject>? FieldLayoutAsFieldObjects
        {
            get
            {
                return fieldObjects;
            }
        }

        public void SetLayout(FieldLayout layout)
        {
            if (null != layout)
            {
                foreach (FieldObject fo in fieldObjects)
                {
                    if (layout.HasEntry(fo.Tag))
                    {
                        fo.Position = layout.GetEntryPosition(fo.Tag);
                        IsDirty = true;
                    }
                }
                Invalidate();
            }
        }

        private FieldLayout? nextLayout;

        public void SetNextLayout(FieldLayout? layout)
        {
            nextLayout = layout;
            Invalidate();
        }

        public void SetLayouts(FieldLayout layout, FieldLayout newNextLayout)
        {
            nextLayout = newNextLayout;
            SetLayout(layout);
        }

        /// <summary>
        /// Converts the supplied point to the field coordinates.
        /// </summary>
        /// <param name="pt">The point on the user interface.</param>
        /// <returns>A point converted to field coordinates.</returns>
        private PointF ToFieldPoint(Point pt)
        {
            PointF[] points = new PointF[] {
        new PointF(pt.X, pt.Y)
      };
            displayToFieldTransfom.TransformPoints(points);
            return points[0];
        }

        private Rectangle ToEnclosingDisplayRect(RectangleF fieldRect)
        {
            PointF[] points = new PointF[] {
        new PointF(fieldRect.Left, fieldRect.Top), // Top Left
        new PointF(fieldRect.Right, fieldRect.Top), // Top Right
        new PointF(fieldRect.Left, fieldRect.Bottom), // Bottom Left
        new PointF(fieldRect.Right, fieldRect.Bottom) // Bottom Right
      };
            fieldToDisplayTransform.TransformPoints(points);
            int left = (int)Math.Round(points[0].X);
            int right = left;
            int top = (int)Math.Round(points[0].Y);
            int bottom = top;
            for (int idx = 1; idx < points.Length; idx++)
            {
                int x = (int)Math.Round(points[idx].X);
                int y = (int)Math.Round(points[idx].Y);
                left = Math.Min(x, left);
                right = Math.Max(x, right);
                top = Math.Min(y, top);
                bottom = Math.Max(y, bottom);
            }
            return new Rectangle(left, top, right - left, bottom - top);
        }

        private FieldObject ObjectAtPoint(Point pt)
        {
            PointF fieldPoint = ToFieldPoint(pt);
            foreach (FieldObject fo in fieldObjects)
            {
                if (fo.ContainsPoint(fieldPoint))
                {
                    return fo;
                }
            }
            return null;
        }

#if false
    public void ZoomAndCentreOn(RectangleF fieldRectangle)
    {
      // TODO: Write and call this method
      throw new NotImplementedException();
    }
#endif

        private void CalculateFieldGeometry(Size windowSize)
        {
            IPlayingSurfaceType ft = FieldType;
            if (null != ft)
            {
                // TESTING: Values to determine where the "viewport" is (centre-point of the zoom/window, zoom ratio (1.0 is unity))

                // Calculate the ratio used to scale field units into pixels
                float ratioX = windowSize.Width / (ft.Length + (ft.Margin * 2.0F));
                float ratioY = windowSize.Height / (ft.Width + (ft.Margin * 2.0F));
                float ratio = (ratioX < ratioY) ? ratioX : ratioY;
                ratio *= zoomFactor;

                Point fieldOriginPositionInDisplayUnits =
                  new Point(windowSize.Width / 2 - (int)Math.Round(ratio * zoomPoint.X),
                            windowSize.Height / 2 - (int)Math.Round(ratio * zoomPoint.Y));

                // NOTE: The order of these transforms is important.
                fieldToDisplayTransform = new Matrix();
                fieldToDisplayTransform.Translate(fieldOriginPositionInDisplayUnits.X, fieldOriginPositionInDisplayUnits.Y);
                fieldToDisplayTransform.Scale(ratio, ratio);
                fieldToDisplayTransform.RotateAt(rotationAngle, zoomPoint);

            }
            else
            {
                fieldToDisplayTransform = new Matrix();
            }
            // Create the reverse transform matrix
            displayToFieldTransfom = fieldToDisplayTransform.Clone();
            if (displayToFieldTransfom.IsInvertible)
            {
                displayToFieldTransfom.Invert();
            }
        }

        public void DrawIntoImage(Image image, FieldLayout layoutToDraw, FieldLayout nextLayoutData)
        {
            using (Graphics graphics = Graphics.FromImage(image))
            {
                if (image.GetType() == typeof(Bitmap))
                {
                    Brush brush = null;
                    try
                    {
                        if (null == FieldType)
                        {
                            brush = new SolidBrush(Color.White);
                        }
                        else
                        {
                            brush = new SolidBrush(this.FieldType.SurfaceColor);
                        }
                        graphics.FillRectangle(brush, 0, 0, image.Width, image.Height);
                    }
                    finally
                    {
                        if (brush != null)
                        {
                            brush.Dispose();
                        }
                    }
                }
                DrawLayoutIntoGraphics(graphics, layoutToDraw, nextLayoutData);
            }
        }

        private void DrawLayoutIntoGraphics(Graphics g, FieldLayout layoutToDraw, FieldLayout nextLayoutData)
        {
            if (null == FieldType)
            {
                return;
            }
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Transform = fieldToDisplayTransform;

            FieldType.DrawMarkings(g);

            // Draw the movement lines that show the movement between the current position and
            // the next position in the sequence (these are drawn first so that they appear under
            // the players)
            if (nextLayoutData != null)
            {
                foreach (FieldObject fieldObject in fieldObjects)
                {
                    if (nextLayoutData.HasEntry(fieldObject.Tag))
                    {
                        if (null != layoutToDraw)
                        {
                            fieldObject.DrawMovementLineFrom(g, layoutToDraw.GetEntryPosition(fieldObject.Tag), nextLayoutData.GetEntryPosition(fieldObject.Tag));
                        }
                        else
                        {
                            fieldObject.DrawMovementLine(g, nextLayoutData.GetEntryPosition(fieldObject.Tag));
                        }
                    } // endif
                }
            }

            // Draw each of the field objects
            foreach (FieldObject fieldObject in fieldObjects)
            {
                if (null != layoutToDraw)
                {
                    fieldObject.DrawAt(g, layoutToDraw.GetEntryPosition(fieldObject.Tag));
                }
                else
                {
                    fieldObject.Draw(g);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (null == e)
            {
                return;
            }
            FieldLayout nextLayoutData = null;
            if (showMovementLines)
            {
                nextLayoutData = nextLayout;
            }
            DrawLayoutIntoGraphics(e.Graphics, null, nextLayoutData);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (null != FieldType)
            {
                CalculateFieldGeometry(Size);
                Invalidate();
            }
        }

        private void InvalidateFieldArea(RectangleF rect)
        {
            Rectangle r = ToEnclosingDisplayRect(rect);
            // Make the rectangle slightly bigger to ensure that we invalidate the 
            // entire area of the object, including possible anti-aliasing area.
            r.Inflate(3, 3);
            Invalidate(r);
        }

        private void MoveObjectTo(FieldObject fo, PointF pos)
        {
            if (fo.Position != pos)
            {
                if (ShowMovementLines && (null != nextLayout))
                {
                    RectangleF r = RectangleF.Union(fo.GetRectangle(), fo.GetRectangleAt(nextLayout.GetEntryPosition(fo.Tag)));
                    InvalidateFieldArea(r);
                }
                else
                {
                    InvalidateFieldArea(fo.GetRectangle());
                }
                fo.Position = pos;
                IsDirty = true;
                if (ShowMovementLines && (null != nextLayout))
                {
                    RectangleF r = RectangleF.Union(fo.GetRectangle(), fo.GetRectangleAt(nextLayout.GetEntryPosition(fo.Tag)));
                    InvalidateFieldArea(r);
                }
                else
                {
                    InvalidateFieldArea(fo.GetRectangle());
                }
                Update();
            }
        }

        private void ClearToolTip()
        {
            if (toolTip.Active)
            {
                toolTip.Hide(this);
                toolTip.Tag = null;
            }
        }

        private FieldObject dragObject;
        private PointF dragObjectOriginalPosition;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if ((null != e) && (AllowInteraction))
            {
                ClearToolTip();

                switch (Tool)
                {
                    case Tool.MoveObjects:
                        if ((e.Button == MouseButtons.Left) && (ModifierKeys == Keys.None))
                        {
                            FieldObject fo = ObjectAtPoint(e.Location);
                            if (fo != null)
                            {
                                dragObject = fo;
                                dragObjectOriginalPosition = fo.Position;
                                Capture = true;
                            }
                        }
                        else if ((e.Button == MouseButtons.Right) && (ModifierKeys == Keys.None))
                        {
                            fieldObjectContextMenu.Tag = null;
                            FieldObject fo = ObjectAtPoint(e.Location);
                            if (fo != null)
                            {
                                fieldObjectContextMenu.Tag = fo;
                                changeLabelMenuItem.Enabled = (null != CustomLabelProvider) && (fo.ShowsLabel);
                                fieldObjectContextMenu.Show(this, e.Location);
                            }
                        }
                        break;
                    case Tool.Move:
                        // TODO: Support capturing the mouse for moving the field around
                        break;
                    case Tool.Zoom:
                        if (e.Button == MouseButtons.Left)
                        {
                            ZoomAtPoint(e.Location, true);
                        }
                        else if (e.Button == MouseButtons.Right)
                        {
                            ZoomAtPoint(e.Location, false);
                        }
                        else if (e.Button == MouseButtons.Middle)
                        {
                            CentreAt(ToFieldPoint(e.Location));
                        }
                        if (ToolMode == ToolMode.RevertAfterSingleUse)
                        {
                            RevertTool();
                        }
                        break;
                }
            }
            base.OnMouseDown(e);
        }

        private void ZoomAtPoint(Point displayPoint, bool zoomIn)
        {
            const float Top = 4.0F;
            const float Bottom = 3.0F;
            float zoom = (zoomIn) ? (Top / Bottom) : (Bottom / Top);
            zoom = Math.Max(-2.0F, Math.Min(2.0F, zoom));
            ZoomAt(ToFieldPoint(displayPoint), zoom);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (null != e)
            {
                if ((Capture) && (dragObject != null))
                {
                    PointF pt = ToFieldPoint(new Point(e.X, e.Y));
                    MoveObjectTo(dragObject, pt);
                }
                else if ((AllowInteraction) && (!Capture))
                {
                    // Show a tool tip if the cursor is over an item
                    FieldObject fo = ObjectAtPoint(e.Location);
                    if ((toolTip.Tag != fo) || (!toolTip.Active))
                    {
                        if (null != fo)
                        {
                            toolTip.Tag = fo;
                            toolTip.ToolTipTitle = fo.Label;
                            var objectRect = this.ToEnclosingDisplayRect(fo.GetRectangle());
                            var tipPosition = new Point(objectRect.Right, objectRect.Bottom);
                            tipPosition.Offset(10, 10);
                            toolTip.Show(fo.Name, this, tipPosition);
                        }
                        else
                        {
                            ClearToolTip();
                        }
                    }
                }
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (null != e)
            {
                if ((Capture) && (dragObject != null))
                {
                    PointF pt = ToFieldPoint(new Point(e.X, e.Y));
                    MoveObjectTo(dragObject, pt);
                    dragObject = null;
                    Capture = false;
                }
            }
            base.OnMouseUp(e);
        }

        private bool CancelDrag()
        {
            if ((Capture) && (dragObject != null))
            {
                MoveObjectTo(dragObject, dragObjectOriginalPosition);
                dragObject = null;
                Capture = false;
                return true;
            }
            return false;
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                case Keys.Cancel:
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    return true;
                default:
                    return base.IsInputKey(keyData);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (null != e)
            {
                if ((e.KeyCode == Keys.Escape) || (e.KeyCode == Keys.Cancel))
                {
                    e.Handled = CancelDrag();
                }
                else if ((AllowInteraction) && ((e.KeyCode == Keys.Control) || (e.KeyCode == Keys.ControlKey)))
                {
                    Tool = Tool.Zoom;
                    ToolMode = ToolMode.MultiUse;
                }
            }
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (null != e)
            {
                if ((Tool == Tool.Zoom) && ((e.KeyCode == Keys.Control) || (e.KeyCode == Keys.ControlKey)))
                {
                    RevertTool();
                }
            }
            base.OnKeyUp(e);
        }

        private void changeLabelMenuItem_Click(object sender, EventArgs e)
        {
            FieldObject fo = fieldObjectContextMenu.Tag as FieldObject;
            if ((null != fo) && (null != CustomLabelProvider))
            {
                using (var d = new EditFieldObjectLabelDialog())
                {
                    d.FieldObjectName = fo.Name;
                    d.CustomLabel = CustomLabelProvider.GetCustomLabel(fo.Tag);
                    DialogResult r = d.ShowDialog(this);
                    switch (r)
                    {
                        case DialogResult.No:
                            CustomLabelProvider.RemoveCustomLabel(fo.Tag);
                            InvalidateFieldArea(fo.GetRectangle());
                            break;
                        case DialogResult.OK:
                            CustomLabelProvider.UpdateCustomLabel(fo.Tag, d.CustomLabel);
                            InvalidateFieldArea(fo.GetRectangle());
                            break;
                    }
                }
            }
        }

        private void ZoomAt(PointF fieldPoint, float zoomRatio)
        {
            if (zoomRatio == 0.0F)
            {
                throw new ArgumentOutOfRangeException(nameof(zoomRatio));
            }
            var ft = FieldType;
            if (null != ft)
            {
                PointF constrainedFieldPoint = new PointF(Math.Max(0.0F, Math.Min(ft.Length, fieldPoint.X)),
                                                          Math.Max(0.0F, Math.Min(ft.Width, fieldPoint.Y)));
                float newZoomFactor = Math.Max(0.5F, Math.Min(4.0F, zoomFactor * zoomRatio));
                float offsetX = constrainedFieldPoint.X - zoomPoint.X;
                float offsetY = constrainedFieldPoint.Y - zoomPoint.Y;
                float zoomRatioChange = zoomFactor / newZoomFactor;
                float newZoomPointX = constrainedFieldPoint.X - (offsetX * zoomRatioChange);
                float newZoomPointY = constrainedFieldPoint.Y - (offsetY * zoomRatioChange);
                if ((newZoomPointX != zoomPoint.X) || (newZoomPointY != zoomPoint.Y) || (newZoomFactor != zoomFactor))
                {
                    zoomPoint.X = newZoomPointX;
                    zoomPoint.Y = newZoomPointY;
                    zoomFactor = newZoomFactor;
                    CalculateFieldGeometry(Size);
                    IsViewDirty = true;
                    Invalidate();
                }
            }
        }

        private void Zoom(float zoomRatio)
        {
            if (zoomRatio == 0.0F)
            {
                throw new ArgumentOutOfRangeException("zoomRatio");
            }
            float newZoomFactor = zoomFactor * zoomRatio;
            newZoomFactor = Math.Max(0.5F, Math.Min(4.0F, newZoomFactor));
            if (newZoomFactor != zoomFactor)
            {
                zoomFactor = newZoomFactor;
                CalculateFieldGeometry(Size);
                IsViewDirty = true;
                Invalidate();
            }
        }

        private void CentreAt(PointF fieldPoint)
        {
            var ft = FieldType;
            if (null != ft)
            {
                float newZoomPointX = Math.Max(0.0F, Math.Min(ft.Length, fieldPoint.X));
                float newZoomPointY = Math.Max(0.0F, Math.Min(ft.Width, fieldPoint.Y));
                if ((newZoomPointX != zoomPoint.X) || (newZoomPointY != zoomPoint.Y))
                {
                    zoomPoint.X = newZoomPointX;
                    zoomPoint.Y = newZoomPointY;
                    CalculateFieldGeometry(Size);
                    IsViewDirty = true;
                    Invalidate();
                }
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (null != e)
            {
                if (e.Delta != 0)
                {
                    // Mouse wheel changed, so zoom on current location
                    float zoom = 1.0F;
                    if (e.Delta > 0)
                    {
                        zoom = 1.1F * ((float)e.Delta / 120.0F);
                    }
                    else
                    {
                        zoom = 0.9F * ((float)(-e.Delta) / 120.0F);
                    }
                    zoom = Math.Max(-2.0F, Math.Min(2.0F, zoom));
                    if ((ModifierKeys == Keys.None) || (ModifierKeys == Keys.Control))
                    {
                        Zoom(zoom);
                    }
                    else if (ModifierKeys == Keys.Alt)
                    {
                        ZoomAt(ToFieldPoint(e.Location), zoom);
                    }
                }
            }
            base.OnMouseWheel(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (null != e)
            {
                if ((e.Button == System.Windows.Forms.MouseButtons.Left) && (ModifierKeys == Keys.Alt))
                {
                    // Re-centre the display at the point clicked
                    CentreAt(ToFieldPoint(e.Location));
                }
            }
            base.OnMouseClick(e);
        }

        public void SetLanguage(string text)
        {
            changeLabelMenuItem.Text = text;
        }
    }
}
