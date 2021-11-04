// Sports Tactics Board
//
// http://sportstacticsbd.sourceforge.net/
// http://sourceforge.net/projects/sportstacticsbd/
// 
// Sports Tactics Board is a utility that allows coaches, trainers and 
// officials to describe sports tactics, strategies and positioning using 
// a magnetic or chalk-board style approach.
// 
// Copyright (C) 2010-2012 Robert Turner
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
using System.Collections.ObjectModel;
using System.Drawing;

namespace SportsTacticsBoard.PlayingSurfaceTypes
{
  /// <summary>
  /// Implements a Floorball field
  /// Author: Ulrich Jenzer
  /// </summary>
  class FloorballField : SportsTacticsBoard.IPlayingSurfaceType
  {
    // Field units are in metres
    private const float fieldLength = 24.0F;
    private const float fieldWidth = 14.0F;
    private const float goalWidth = 0.5F;
    private const float goalHeight = 1.6F;
    private const float goalHalfHeight = goalHeight / 2.0F;
    private const float goalSavedAreaHeight = 4.5F;
    private const float goalSavedAreaHalfHeight = goalSavedAreaHeight / 2.0F;
    private const float goalSavedAreaWidth = 3.0F;
    private const float margin = 0.8F;
    private const float playerSize = 0.35F;
    private const float ballSize = 0.19F;
    private const float linePenWidth = 0.05F;
    private const float fieldObjectOutlinePenWidth = 0.05F;
    private const float fieldObjectMovementPenWidth = fieldObjectOutlinePenWidth * 3.0F;
    private const float coneSize = 0.25F;

    private const int playersPerTeam = 6;


    public string Tag
    {
      get { return "Floorball"; }
    }

    public string Name
    {
      get
      {
        return Properties.Resources.ResourceManager.GetString("FieldType_" + Tag);
      }
    }

    public float Length
    {
      get { return fieldLength; }
    }

    public float HalfLength
    {
      get { return Length / 2.0F; }
    }

    public float Width
    {
      get { return fieldWidth; }
    }

    public float HalfWidth
    {
      get { return Width / 2.0F; }
    }

    public float Margin
    {
      get { return margin; }
    }

    public Color SurfaceColor
    {
      get { return Color.LightSkyBlue; }
    }

    public Collection<FieldObject> StandardObjects
    {
      get
      {
        List<FieldObject> fieldObjects = new List<FieldObject>();

        // Create the players
        for (int i = 1; i <= playersPerTeam; i++) {
          fieldObjects.Add(new FieldObjects.NumberedPlayer(i, FieldObjects.Player.TeamId.Attacking, playerSize));
          fieldObjects.Add(new FieldObjects.NumberedPlayer(i, FieldObjects.Player.TeamId.Defending, playerSize));
        }

        // Add the ball
        fieldObjects.Add(new FieldObjects.Ball("1", "Ball_1", HalfLength, HalfWidth, ballSize));
        fieldObjects.Add(new FieldObjects.Ball("2", "Ball_2", HalfLength + 1.0F, -0.5F, ballSize));
        fieldObjects.Add(new FieldObjects.Ball("3", "Ball_3", HalfLength + 1.5F, -0.5F, ballSize));

        // Add the referees
        fieldObjects.Add(new FieldObjects.Referee("S", "Referee_Floorball_S", Length - 0.3F, 0.3F, playerSize));
        //fieldObjects.Add(new FieldObjects.Referee("N", "Referee_Floorball_N", Length / 2.0F, Width + (Width / 16.0F), playerSize));

        // Add the Cones
        var xPosition = HalfLength - 1.0F;
        var yPosition = -0.5F;
        for (int coneNumber = 1; (coneNumber <= 6); coneNumber++, xPosition -= (coneSize * 2.5F)) {
          fieldObjects.Add(new FieldObjects.CircularConeWithBorder(coneNumber, xPosition, yPosition, coneSize));
        }

        // Adjust various parameters for all the field objects
        foreach (FieldObject fo in fieldObjects) {
          fo.OutlinePenWidth = fieldObjectOutlinePenWidth;
          fo.MovementPenWidth = fieldObjectMovementPenWidth;
          fo.OutlinePenColor = Color.Black;
        }

        return new Collection<FieldObject>(fieldObjects);
      }
    }

    /// <summary>
    /// Append players to layout on standart positions
    /// </summary>
    /// <param name="layout">Layout</param>
    private void AppendPlayerPositions(FieldLayout layout)
    {
      // Add attacking players
      SportsTacticsBoard.FieldObjects.Player.TeamId teamId = SportsTacticsBoard.FieldObjects.Player.TeamId.Attacking;
      string playerTag = FieldObjects.NumberedPlayer.ComposeTag(teamId, 1);
      layout.AddEntry(playerTag, 2.6F, HalfWidth);
      playerTag = FieldObjects.NumberedPlayer.ComposeTag(teamId, 2);
      layout.AddEntry(playerTag, HalfLength / 2.0F, HalfWidth + 2.0F);
      playerTag = FieldObjects.NumberedPlayer.ComposeTag(teamId, 3);
      layout.AddEntry(playerTag, HalfLength / 2.0F, HalfWidth - 2.0F);
      playerTag = FieldObjects.NumberedPlayer.ComposeTag(teamId, 4);
      layout.AddEntry(playerTag, HalfLength - 0.6F, HalfWidth);
      playerTag = FieldObjects.NumberedPlayer.ComposeTag(teamId, 5);
      layout.AddEntry(playerTag, HalfLength - 0.6F, HalfWidth - 3.0F);
      playerTag = FieldObjects.NumberedPlayer.ComposeTag(teamId, 6);
      layout.AddEntry(playerTag, HalfLength - 0.6F, HalfWidth + 3.0F);

      // Add defending players
      teamId = SportsTacticsBoard.FieldObjects.Player.TeamId.Defending;
      playerTag = FieldObjects.NumberedPlayer.ComposeTag(teamId, 1);
      layout.AddEntry(playerTag, Length - 2.6F, Width / 2.0F);
      playerTag = FieldObjects.NumberedPlayer.ComposeTag(teamId, 2);
      layout.AddEntry(playerTag, HalfLength / 2.0F * 3.0F, HalfWidth + 2.0F);
      playerTag = FieldObjects.NumberedPlayer.ComposeTag(teamId, 3);
      layout.AddEntry(playerTag, HalfLength / 2.0F * 3.0F, HalfWidth - 2.0F);
      playerTag = FieldObjects.NumberedPlayer.ComposeTag(teamId, 4);
      layout.AddEntry(playerTag, HalfLength + 0.6F, HalfWidth);
      playerTag = FieldObjects.NumberedPlayer.ComposeTag(teamId, 5);
      layout.AddEntry(playerTag, HalfLength + 0.6F, HalfWidth - 3.0F);
      playerTag = FieldObjects.NumberedPlayer.ComposeTag(teamId, 6);
      layout.AddEntry(playerTag, HalfLength + 0.6F, HalfWidth + 3.0F);
    }

    public FieldLayout DefaultLayout
    {
      get
      {
        FieldLayout layout = new FieldLayout();
        AppendPlayerPositions(layout);
        return layout;
      }
    }

    public ReadOnlyCollection<string> GetTeam(FieldObjects.Player.TeamId team)
    {
      List<string> playersOnTeam = new List<string>();
      for (int i = 1; i <= playersPerTeam; i++) {
        playersOnTeam.Add(FieldObjects.NumberedPlayer.ComposeTag(team, i));
      }
      return new ReadOnlyCollection<string>(playersOnTeam);
    }

    public void DrawMarkings(Graphics graphics)
    {
      // Create the pen for drawing the field lines with
      using (Pen linePen = new Pen(Color.Brown, linePenWidth)) {
        using (Brush courtBrush = new SolidBrush(Color.LightGray)) {

          #region Draw the court surface
          using (var path = new System.Drawing.Drawing2D.GraphicsPath()) {
            float pos_frame_rad = 2.0F;
            path.AddArc(new RectangleF(0.0F, 0.0F, (pos_frame_rad * 2), (pos_frame_rad * 2)), -180.0F, 90.0F);
            path.AddArc(new RectangleF(Length - (pos_frame_rad * 2), 0.0F, (pos_frame_rad * 2), (pos_frame_rad * 2)), 270.0F, 90.0F);
            path.AddArc(new RectangleF(Length - (pos_frame_rad * 2), Width - (pos_frame_rad * 2), (pos_frame_rad * 2), (pos_frame_rad * 2)), 0.0F, 90.0F);
            path.AddArc(new RectangleF(0.0F, Width - (pos_frame_rad * 2), (pos_frame_rad * 2), (pos_frame_rad * 2)), 90.0F, 90.0F);
            path.CloseFigure();
            linePen.Width *= 2.0F;
            graphics.DrawPath(linePen, path);
            linePen.Width /= 2.0F;
          }
          #endregion

          #region Draw central line
          PointF fieldCenter = new PointF(Length / 2, Width / 2);
          PointF centerLineTop = new PointF(fieldCenter.X, 0.0F);
          PointF centerLineBottom = new PointF(fieldCenter.X, Width);
          PointF leftLineTop = new PointF(2.5F, 0.0F);
          PointF leftLineBottom = new PointF(2.5F, Width);
          PointF rightLineTop = new PointF(Length - 2.5F, 0.0F);
          PointF rightLineBottom = new PointF(Length - 2.5F, Width);

          graphics.DrawLine(linePen, centerLineTop, centerLineBottom);
          graphics.DrawLine(linePen, leftLineTop, leftLineBottom);
          graphics.DrawLine(linePen, rightLineTop, rightLineBottom);
          #endregion

          #region Draw goal lines
          DrawGoal(graphics, linePen, 3.0F - 0.5F, Width / 2.0F, true);
          DrawGoal(graphics, linePen, Length - 3.0F + 0.5F, Width / 2.0F, !true);
          #endregion Draw goal lines

          #region Draw bully line
          using (Pen bullyPen = new Pen(Color.Black, 0.09F)) {
            DrawingCross(graphics, bullyPen, Length / 2.0F, 1.0F);
            DrawingCross(graphics, bullyPen, HalfLength, HalfWidth);
            DrawingCross(graphics, bullyPen, Length / 2.0F, Width - 1.0F);

            DrawingCross(graphics, bullyPen, 2.5F, 1.0F);
            DrawingCross(graphics, bullyPen, 2.5F, Width - 1.0F);

            DrawingCross(graphics, bullyPen, Length - 2.5F, 1.0F);
            DrawingCross(graphics, bullyPen, Length - 2.5F, Width - 1.0F);
          }
          #endregion Draw bully line

        }
      }
    }

    private static void DrawGoal(Graphics g, Pen pen, float px, float py, bool leftSide)
    {
      if (leftSide == true) { // links
        // Goal
        g.DrawLine(pen, px - goalWidth, py - goalHalfHeight, px, py - goalHalfHeight);
        g.DrawLine(pen, px, py - goalHalfHeight, px, py + goalHalfHeight);
        g.DrawLine(pen, px, py + goalHalfHeight, px - goalWidth, py + goalHalfHeight);
        g.DrawLine(pen, px - goalWidth, py + goalHalfHeight, px - goalWidth, py - goalHalfHeight);

        // Goal Raum
        pen.Width /= 2.0F;
        g.DrawLine(pen, px - goalWidth, py - goalSavedAreaHalfHeight, px - goalWidth, py + goalSavedAreaHalfHeight);
        g.DrawLine(pen, px - goalWidth, py - goalSavedAreaHalfHeight, px - goalWidth + goalSavedAreaWidth, py - goalSavedAreaHalfHeight);
        g.DrawLine(pen, px - goalWidth + goalSavedAreaWidth, py - goalSavedAreaHalfHeight, px - goalWidth + goalSavedAreaWidth, py + goalSavedAreaHalfHeight);
        g.DrawLine(pen, px - goalWidth + goalSavedAreaWidth, py + goalSavedAreaHalfHeight, px - goalWidth, py + goalSavedAreaHalfHeight);
        pen.Width *= 2.0F;
        // Goal Schutzraum
      } else { // rechts
        // Goal
        g.DrawLine(pen, px + goalWidth, py - goalHalfHeight, px + goalWidth, py + goalHalfHeight);
        g.DrawLine(pen, px, py - goalHalfHeight, px + goalWidth, py - goalHalfHeight);
        g.DrawLine(pen, px, py - goalHalfHeight, px, py + goalHalfHeight);
        g.DrawLine(pen, px, py + goalHalfHeight, px + goalWidth, py + goalHalfHeight);

        // Goal Raum
        pen.Width /= 2.0F;
        g.DrawLine(pen, px + goalWidth, py - goalSavedAreaHalfHeight, px + goalWidth, py + goalSavedAreaHalfHeight);
        g.DrawLine(pen, px + goalWidth, py - goalSavedAreaHalfHeight, px + goalWidth - goalSavedAreaWidth, py - goalSavedAreaHalfHeight);
        g.DrawLine(pen, px + goalWidth - goalSavedAreaWidth, py - goalSavedAreaHalfHeight, px + goalWidth - goalSavedAreaWidth, py + goalSavedAreaHalfHeight);
        g.DrawLine(pen, px + goalWidth, py + goalSavedAreaHalfHeight, px + goalWidth - goalSavedAreaWidth, py + goalSavedAreaHalfHeight);
        pen.Width *= 2.0F;
        // Goal Schutzraum
      }
    }

    private static void DrawingCross(Graphics g, Pen pen, float pxm, float pym)
    {
      float crossLength = 0.15F;
      g.DrawLine(pen, pxm - crossLength, pym, pxm + crossLength, pym);
      g.DrawLine(pen, pxm, pym - crossLength, pxm, pym + crossLength);
    }
  }
}
