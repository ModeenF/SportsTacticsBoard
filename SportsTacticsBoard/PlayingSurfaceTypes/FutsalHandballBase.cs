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
  /// Implements a standard FIFA Futsal pitch, which seems to be more or less the same as Handball.
  /// 
  /// Playing surface units are in metres and the default pitch 
  /// dimensions are 40 metres by 22 metres. Pitch is drawn to
  /// correct dimensions without any specific compromises.
  /// 
  /// 2 referee field objects are supported for referee training.
  /// </summary>
  abstract class FutsalHandballBase : SportsTacticsBoard.IPlayingSurfaceType
  {
    private const float fieldLength = 40.0F;
    private const float fieldWidth = 22.0F;
    private const float margin = 3.25F;
    private const float linePenWidth = 0.08F;
    private const float penaltyMarkRadius = 0.075F;
    private const float penaltyMarkDiameter = penaltyMarkRadius * 2.0F;
    private const float playerSize = 0.5F;
    private const float ballSize = 0.2F;
    private const float goalWidth = 3.00F;
    private const float fieldObjectOutlinePenWidth = 0.05F;
    private const float fieldObjectMovementPenWidth = fieldObjectOutlinePenWidth * 3.0F;
    private const float coneSize = 0.25F;

    abstract protected int PlayersPerTeam { get; }
    abstract public string Tag { get; }

    public string Name
    {
      get { return Properties.Resources.ResourceManager.GetString("FieldType_" + Tag); }
    }

    public float Length
    {
      get { return fieldLength; }
    }

    public float Width
    {
      get { return fieldWidth; }
    }

    public float Margin
    {
      get { return margin; }
    }

    public Color SurfaceColor
    {
      get { return Color.Blue; }
    }

    public Collection<FieldObject> StandardObjects
    {
      get {
        List<FieldObject> fieldObjects = new List<FieldObject>();

        // Create the players
        for (int i = 1; i <= PlayersPerTeam; i++) {
          fieldObjects.Add(new FieldObjects.NumberedPlayer(i, FieldObjects.Player.TeamId.Attacking, playerSize));
          fieldObjects.Add(new FieldObjects.NumberedPlayer(i, FieldObjects.Player.TeamId.Defending, playerSize));
        }

        // Add the ball
        fieldObjects.Add(new FieldObjects.Ball(Length / 2, Width / 2, ballSize));

        // Add the referees
        fieldObjects.Add(new FieldObjects.Referee("R1", "Referee_" + Tag + "_1", Length * 3.0F / 4.0F, Width + (playerSize * 2.05F), playerSize));
        fieldObjects.Add(new FieldObjects.Referee("R2", "Referee_" + Tag + "_2", Length / 4F, -(playerSize * 2.05F), playerSize));

        // Add some cones
        const int NumberOfCones = 16;
        float xPosition = (fieldLength / 2) - ((NumberOfCones * coneSize * 3F) / 2F);
        const float yPosition = fieldWidth + 2.5F;
        for (int coneNumber = 1; (coneNumber <= NumberOfCones); coneNumber++, xPosition += (coneSize * 3)) {
          fieldObjects.Add(new FieldObjects.UnlabelledTriangularCone(coneNumber, xPosition, yPosition, coneSize));
        }

        // Adjust various parameters for all the field objects
        foreach (FieldObject fo in fieldObjects) {
          fo.OutlinePenWidth = fieldObjectOutlinePenWidth;
          fo.MovementPenWidth = fieldObjectMovementPenWidth;
        }

        return new Collection<FieldObject>(fieldObjects);
      }
    }

    private void AppendPlayerPositions(FieldLayout layout, FieldObjects.Player.TeamId teamId, bool putOnLeftSide)
    {
      const float spacing = playerSize * 2.0F * 1.5F;
      const float benchIndent = 5.0F;
      const float benchDistanceFromField = 2.5F;

      float benchY = 0.0F - benchDistanceFromField;
      float benchStartPos = benchIndent;
      if (!putOnLeftSide) {
        benchStartPos += Length / 2.0F;
      }

      for (int playerNumber = 1; playerNumber <= PlayersPerTeam; playerNumber++) {
        string playerTag = FieldObjects.NumberedPlayer.ComposeTag(teamId, playerNumber);
        layout.AddEntry(playerTag, benchStartPos + (spacing * playerNumber), benchY);
      }
    }

    public FieldLayout DefaultLayout
    {
      get {
        FieldLayout layout = new FieldLayout();
        AppendPlayerPositions(layout, SportsTacticsBoard.FieldObjects.Player.TeamId.Attacking, true);
        AppendPlayerPositions(layout, SportsTacticsBoard.FieldObjects.Player.TeamId.Defending, false);
        return layout;
      }
    }

    public ReadOnlyCollection<string> GetTeam(SportsTacticsBoard.FieldObjects.Player.TeamId team)
    {
      List<string> playersOnTeam = new List<string>();
      for (int i = 1; i <= PlayersPerTeam; i++) {
        playersOnTeam.Add(FieldObjects.NumberedPlayer.ComposeTag(team, i));
      }
      return new ReadOnlyCollection<string>(playersOnTeam);
    }

    public void DrawMarkings(Graphics graphics)
    {
      // Create the pens for drawing the field lines with
      using (Pen linePen = new Pen(Color.White, linePenWidth)) {
        using (Pen additionalMarkPen = new Pen(Color.White, 0.06F)) {
          using (Brush lineBrush = new SolidBrush(Color.White)) {

            const float fieldCentreX = fieldLength / 2;
            const float fieldCentreY = fieldWidth / 2;

            const float keepBackDistance = 5.0F;

            // Draw the lines on the field

            // ... The goal and touch lines
            graphics.DrawRectangle(linePen, 0.0F, 0.0F, Length, Width);

            #region Draw the centre line
            // ... The centre line
            graphics.DrawLine(linePen, new PointF(fieldCentreX, 0.0F), new PointF(fieldCentreX, fieldWidth));
            #endregion

            #region Draw the centre circle
            // ... The centre circle
            float centreCircleRadius = 3.0F;
            RectangleF centreCircleRect = new RectangleF(fieldCentreX - (centreCircleRadius), fieldCentreY - (centreCircleRadius), centreCircleRadius * 2, centreCircleRadius * 2);
            graphics.DrawEllipse(linePen, centreCircleRect);
            RectangleF centreMarkRect = new RectangleF(fieldCentreX - penaltyMarkRadius, fieldCentreY - penaltyMarkRadius, penaltyMarkDiameter, penaltyMarkDiameter);
            graphics.FillEllipse(lineBrush, centreMarkRect);
            #endregion

            #region Draw the goals
            // ... The goals
            const float goalWidth = 3.0F;
            const float goalDepth = 1.0F;
            const float goalWidthAtCentreOfPosts = goalWidth + linePenWidth;
            const float goalTop = (fieldWidth / 2) - (goalWidthAtCentreOfPosts / 2);
            const float goalBottom = goalTop + goalWidthAtCentreOfPosts;
            graphics.DrawLines(linePen, new PointF[] { 
              new PointF(0.0F, goalTop),
              new PointF(0.0F - goalDepth, goalTop),
              new PointF(0.0F - goalDepth, goalBottom),
              new PointF(0.0F, goalBottom)
            });
            graphics.DrawLines(linePen, new PointF[] { 
              new PointF(Length, goalTop),
              new PointF(Length + goalDepth, goalTop),
              new PointF(Length + goalDepth, goalBottom),
              new PointF(Length, goalBottom)
            });
            #endregion

            #region Draw the penalty areas
            // Draw the penalty areas
            const float penaltyAreaArcDistance = 6.0F;
            RectangleF leftAreaTop = new RectangleF(0.0F - penaltyAreaArcDistance, fieldCentreY - ((goalWidth / 2) + linePenWidth + penaltyAreaArcDistance), penaltyAreaArcDistance * 2, penaltyAreaArcDistance * 2);
            RectangleF leftAreaBottom = new RectangleF(0.0F - penaltyAreaArcDistance, fieldCentreY + ((goalWidth / 2) + linePenWidth) - penaltyAreaArcDistance, penaltyAreaArcDistance * 2, penaltyAreaArcDistance * 2);
            RectangleF rightAreaTop = new RectangleF(fieldLength - penaltyAreaArcDistance, fieldCentreY - ((goalWidth / 2) + linePenWidth + penaltyAreaArcDistance), penaltyAreaArcDistance * 2, penaltyAreaArcDistance * 2);
            RectangleF rightAreaBottom = new RectangleF(fieldLength - penaltyAreaArcDistance, fieldCentreY + ((goalWidth / 2) + linePenWidth) - penaltyAreaArcDistance, penaltyAreaArcDistance * 2, penaltyAreaArcDistance * 2);
            graphics.DrawArc(linePen, leftAreaTop, 270.0F, 90.0F);
            graphics.DrawArc(linePen, leftAreaBottom, 0.0F, 90.0F);
            graphics.DrawLine(linePen, 0.0F + penaltyAreaArcDistance, fieldCentreY - ((goalWidth / 2) + linePenWidth), 0.0F + penaltyAreaArcDistance, fieldCentreY + ((goalWidth / 2) + linePenWidth));
            graphics.DrawArc(linePen, rightAreaTop, 180.0F, 90.0F);
            graphics.DrawArc(linePen, rightAreaBottom, 90.0F, 90.0F);
            graphics.DrawLine(linePen, fieldLength - penaltyAreaArcDistance, fieldCentreY - ((goalWidth / 2) + linePenWidth), fieldLength - penaltyAreaArcDistance, fieldCentreY + ((goalWidth / 2) + linePenWidth));
            #endregion

            #region Draw the substitution zones
            graphics.DrawLine(linePen, fieldCentreX + 5.0F, -0.40F, fieldCentreX + 5.0F, +0.40F);
            graphics.DrawLine(linePen, fieldCentreX + 10.0F, -0.40F, fieldCentreX + 10.0F, +0.40F);
            graphics.DrawLine(linePen, fieldCentreX - 5.0F, -0.40F, fieldCentreX - 5.0F, +0.40F);
            graphics.DrawLine(linePen, fieldCentreX - 10.0F, -0.40F, fieldCentreX - 10.0F, +0.40F);
            #endregion

            #region Draw penalty marks
            // ... The penalty marks
            RectangleF leftFirstPenaltyMark = new RectangleF(0.0F + 6.0F - penaltyMarkRadius, fieldCentreY - penaltyMarkRadius, penaltyMarkDiameter, penaltyMarkDiameter);
            RectangleF leftSecondPenaltyMark = new RectangleF(0.0F + 10.0F - penaltyMarkRadius, fieldCentreY - penaltyMarkRadius, penaltyMarkDiameter, penaltyMarkDiameter);
            RectangleF rightFirstPenaltyMark = new RectangleF(Length - 6.0F - penaltyMarkRadius, fieldCentreY - penaltyMarkRadius, penaltyMarkDiameter, penaltyMarkDiameter);
            RectangleF rightSecondPenaltyMark = new RectangleF(Length - 10.0F - penaltyMarkRadius, fieldCentreY - penaltyMarkRadius, penaltyMarkDiameter, penaltyMarkDiameter);
            graphics.FillEllipse(lineBrush, leftFirstPenaltyMark);
            graphics.FillEllipse(lineBrush, leftSecondPenaltyMark);
            graphics.FillEllipse(lineBrush, rightFirstPenaltyMark);
            graphics.FillEllipse(lineBrush, rightSecondPenaltyMark);
            #endregion

            #region Draw the second penalty mark distance markers
            // Not sure these are right, the FIFA Laws are not clear on their position, nor do they show these in any of the diagrams.
            //      const float extraMarkLength = 0.40F;
            //      graphics.DrawLine(additionalMarkPen, 0.0F + 10.0F - (extraMarkLength / 2), fieldCentreY - keepBackDistance, 0.0F + 10.0F + (extraMarkLength / 2), fieldCentreY - keepBackDistance);
            //      graphics.DrawLine(additionalMarkPen, 0.0F + 10.0F - (extraMarkLength / 2), fieldCentreY + keepBackDistance, 0.0F + 10.0F + (extraMarkLength / 2), fieldCentreY + keepBackDistance);
            //      graphics.DrawLine(additionalMarkPen, fieldLength - 10.0F - (extraMarkLength / 2), fieldCentreY - keepBackDistance, fieldLength - 10.0F + (extraMarkLength / 2), fieldCentreY - keepBackDistance);
            //      graphics.DrawLine(additionalMarkPen, fieldLength - 10.0F - (extraMarkLength / 2), fieldCentreY + keepBackDistance, fieldLength - 10.0F + (extraMarkLength / 2), fieldCentreY + keepBackDistance);
            #endregion

            #region Draw the corner arcs
            // ... The corner arcs
            const float cornerArcRadius = 0.25F;
            const float cornerArcDiameter = cornerArcRadius * 2;
            RectangleF topLeftCornerArc = new RectangleF(0.0F - cornerArcRadius,
              0.0F - cornerArcRadius, cornerArcDiameter, cornerArcDiameter);
            RectangleF topRightCornerArc = new RectangleF(Length - cornerArcRadius,
              0.0F - cornerArcRadius, cornerArcDiameter, cornerArcDiameter);
            RectangleF bottomLeftCornerArc = new RectangleF(0.0F - cornerArcRadius,
              Width - cornerArcRadius, cornerArcDiameter, cornerArcDiameter);
            RectangleF bottomRightCornerArc = new RectangleF(Length - cornerArcRadius,
              Width - cornerArcRadius, cornerArcDiameter, cornerArcDiameter);
            graphics.DrawArc(linePen, topLeftCornerArc, 0.0F, 90.0F);
            graphics.DrawArc(linePen, topRightCornerArc, 90.0F, 90.0F);
            graphics.DrawArc(linePen, bottomLeftCornerArc, 270.0F, 90.0F);
            graphics.DrawArc(linePen, bottomRightCornerArc, 180.0F, 90.0F);
            #endregion

            #region Draw corner kick distance markers
            const float cornerKickMarkLength = 0.40F;
            graphics.DrawLine(linePen, 0.0F - cornerKickMarkLength, 0.0F + (cornerArcRadius + keepBackDistance), 0.0F, 0.0F + (cornerArcRadius + keepBackDistance));
            graphics.DrawLine(linePen, 0.0F - cornerKickMarkLength, fieldWidth - (cornerArcRadius + keepBackDistance), 0.0F, fieldWidth - (cornerArcRadius + keepBackDistance));
            graphics.DrawLine(linePen, fieldLength, 0.0F + (cornerArcRadius + keepBackDistance), fieldLength + cornerKickMarkLength, 0.0F + (cornerArcRadius + keepBackDistance));
            graphics.DrawLine(linePen, fieldLength, fieldWidth - (cornerArcRadius + keepBackDistance), fieldLength + cornerKickMarkLength, fieldWidth - (cornerArcRadius + keepBackDistance));
            #endregion

          }
        }
      }
    }
  }
}
