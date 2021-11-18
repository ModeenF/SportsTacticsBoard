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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SportsTacticsBoard.PlayingSurfaceTypes
{
    /// <summary>
    /// Implements a standard FIFA soccer field.
    /// 
    /// Playing surface units are in yards and the default field 
    /// dimensions are 100 yards by 60 yards. Field is drawn to
    /// correct dimensions without any specific compromises.
    /// 
    /// 4 referee field objects are supported for referee training.
    /// </summary>
    public class SoccerField : IPlayingSurfaceType
    {
        public SoccerField()
        {
            Length = 100.0F;
            Width = 60.0F;
            Margin = 6.5F;
            PlayerSize = 1.15F;
            BallSize = 0.75F;
            ConeSize = 0.50F;
            FieldObjectOutlinePenWidth = 3.0F / 36.0F;
            FieldObjectMovementPenWidth = FieldObjectOutlinePenWidth * 3.0F;
            NetDepth = 2.0F;
            LinePenWidth = 5.0F / 36.0F;
            GoalPenWidth = LinePenWidth / 2.0F;
            PenaltyMarkRadius = 5.0F / 36.0F;
            PenaltyMarkDiameter = PenaltyMarkRadius * 2.0F;
            CentreTickLength = PenaltyMarkRadius;
            SurfaceColor = Color.Green;
            PlayersPerTeam = 11;
            Tag = "Soccer";

            UseCenterCircel = true;
            UseAllRef = true;
            UsePenaltyMarks = true;
            Use18yard = true;
            UseRetreatLine = false;
        }

        public float NetDepth { get; set; }
        public float LinePenWidth { get; set; }
        public float GoalPenWidth { get; set; }
        public float PenaltyMarkRadius { get; set; }
        public float PenaltyMarkDiameter { get; set; }
        public float CentreTickLength { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Margin { get; set; }
        public Color SurfaceColor { get; set; }
        public string Tag { get; set; }
        public int PlayersPerTeam { get; set; }
        public float PlayerSize { get; set; }
        public float BallSize { get; set; }
        public float ConeSize { get; set; }
        public float FieldObjectOutlinePenWidth { get; set; }
        public float FieldObjectMovementPenWidth { get; set; }

        public bool UseCenterCircel { get; set; }
        public bool UseAllRef { get; set; }
        public bool UsePenaltyMarks { get; set; }
        public bool Use18yard { get; set; }

        public bool UseRetreatLine { get; set; }

        public string Name
        {
            get
            {
                return Properties.Resources.ResourceManager.GetString("FieldType_" + Tag);
            }
        }

        public Collection<FieldObject> StandardObjects
        {
            get
            {
                List<FieldObject> fieldObjects = new List<FieldObject>();

                // Create the players
                for (int i = 1; i <= PlayersPerTeam; i++)
                {
                    fieldObjects.Add(new FieldObjects.NumberedPlayer(i, FieldObjects.Player.TeamId.Attacking, PlayerSize));
                    fieldObjects.Add(new FieldObjects.NumberedPlayer(i, FieldObjects.Player.TeamId.Defending, PlayerSize));
                }

                // Add the ball
                fieldObjects.Add(new FieldObjects.Ball(Length / 2, Width / 2, BallSize));

                // Add the referees
                fieldObjects.Add(new FieldObjects.Referee("CR", "Referee_Soccer_CR", (Length / 2F) + 5.0F, (Width / 2F) + 5.0F, PlayerSize));
                if (UseAllRef)
                {
                    fieldObjects.Add(new FieldObjects.Referee("AR", "Referee_Soccer_AR1", Length / 4F, -2.0F, PlayerSize));
                    fieldObjects.Add(new FieldObjects.Referee("AR", "Referee_Soccer_AR2", Length * 3.0F / 4.0F, Width + 2.0F, PlayerSize));
                    fieldObjects.Add(new FieldObjects.Referee("4", "Referee_Soccer_4th", Length / 2.0F, Width + 3.0F, PlayerSize));
                }

                // Add some cones
                const int NumberOfCones = 20;
                var xPosition = (Length / 2) - ((NumberOfCones * ConeSize * 3F) / 2F);
                var yPosition = -5.0F;
                for (int coneNumber = 1; (coneNumber <= NumberOfCones); coneNumber++, xPosition += (ConeSize * 3))
                {
                    fieldObjects.Add(new FieldObjects.UnlabelledTriangularCone(coneNumber, xPosition, yPosition, ConeSize));
                }

                // Adjust various parameters for all the field objects
                foreach (FieldObject fo in fieldObjects)
                {
                    fo.OutlinePenWidth = FieldObjectOutlinePenWidth;
                    fo.MovementPenWidth = FieldObjectMovementPenWidth;
                }

                return new Collection<FieldObject>(fieldObjects);
            }
        }

        private void AppendPlayerPositions(FieldLayout layout, FieldObjects.Player.TeamId teamId, bool putOnLeftSide)
        {
            const float spacing = 3.25F;
            const float benchIndent = 5.0F;
            const float benchDistanceFromField = 5.25F;

            float benchY = Width + benchDistanceFromField;
            float benchStartPos = benchIndent;
            if (!putOnLeftSide)
            {
                benchStartPos += Length / 2.0F;
            }
           
            for (int playerNumber = 1; playerNumber <= PlayersPerTeam; playerNumber++)
            {
                string playerTag = FieldObjects.NumberedPlayer.ComposeTag(teamId, playerNumber);
                layout.AddEntry(playerTag, benchStartPos + (spacing * playerNumber), benchY);
            }
        }

        public FieldLayout DefaultLayout
        {
            get
            {
                FieldLayout layout = new FieldLayout();
                AppendPlayerPositions(layout, FieldObjects.Player.TeamId.Attacking, true);
                AppendPlayerPositions(layout, FieldObjects.Player.TeamId.Defending, false);
                return layout;
            }
        }

        public ReadOnlyCollection<string> GetTeam(FieldObjects.Player.TeamId team)
        {
            List<string> playersOnTeam = new List<string>();
            for (int i = 1; i <= PlayersPerTeam; i++)
            {
                playersOnTeam.Add(FieldObjects.NumberedPlayer.ComposeTag(team, i));
            }
            return new ReadOnlyCollection<string>(playersOnTeam);
        }

        public virtual void DrawMarkings(Graphics graphics)
        {
            // Create the pens for drawing the field lines with
            using (Pen linePen = new Pen(Color.White, LinePenWidth))
            {
                using (Pen goalPen = new Pen(Color.White, GoalPenWidth))
                {

                    // Draw the lines on the field

                    // ... The goal and touch lines
                    graphics.DrawRectangle(linePen, 0.0F, 0.0F, Length, Width);

                    #region Draw the centre line
                    // ... The centre line
                    PointF fieldCentre = new PointF(Length / 2, Width / 2);
                    PointF centreLineTop = new PointF(fieldCentre.X, 0.0F);
                    PointF centreLineBottom = new PointF(fieldCentre.X, Width);
                    graphics.DrawLine(linePen, centreLineTop, centreLineBottom);
                    // ......The 'tick' across the centre line for the kick-off spot
                    graphics.DrawLine(linePen, fieldCentre.X - CentreTickLength, fieldCentre.Y, fieldCentre.X + CentreTickLength, fieldCentre.Y);
                    #endregion


                    //Retreat line in swedish 5vs5 and 7 vs 7 oponents need to move there players behaind this line
                    // when the goalceeper has the bol
                    if (UseRetreatLine)
                    {
                        #region Retreat line left
                        // ... Retreat line left
                        PointF retreatLineLeftCentre = new PointF(Length / 4, Width / 2);
                        PointF retreatLineLeftLineTop = new PointF(retreatLineLeftCentre.X, 0.0F);
                        PointF retreatLineLeftLineBottom = new PointF(retreatLineLeftCentre.X, Width);
                        graphics.DrawLine(linePen, retreatLineLeftLineTop, retreatLineLeftLineBottom);
                        #endregion

                        #region Retreat line left
                        // ... Retreat line left
                        PointF retreatLineRightCentre = new PointF(Length - (Length / 4), Width / 2);
                        PointF retreatLineRightLineTop = new PointF(retreatLineRightCentre.X, 0.0F);
                        PointF retreatLineRightLineBottom = new PointF(retreatLineRightCentre.X, Width);
                        graphics.DrawLine(linePen, retreatLineRightLineTop, retreatLineRightLineBottom);
                        #endregion

                    }
                    #region Draw the centre circle
                    if (UseCenterCircel)
                    {
                        // ... The centre circle
                        float centreCircleDiameter = 20.0F;
                        RectangleF centreCircleRect = new RectangleF(fieldCentre.X - (centreCircleDiameter / 2), fieldCentre.Y - (centreCircleDiameter / 2), centreCircleDiameter, centreCircleDiameter);
                        graphics.DrawEllipse(linePen, centreCircleRect);
                    }
                    #endregion

                    #region Draw the goals
                    // ... The goals
                    float goalWidthInPixels = NetDepth;
                    float goalHeightInPixels = 8.0F;
                    RectangleF leftGoal = new RectangleF(0.0F - goalWidthInPixels, fieldCentre.Y - (goalHeightInPixels / 2),
                      goalWidthInPixels, goalHeightInPixels);
                    RectangleF rightGoal = new RectangleF(Length, fieldCentre.Y - (goalHeightInPixels / 2),
                      goalWidthInPixels, goalHeightInPixels);
                    graphics.DrawRectangle(goalPen, leftGoal.X, leftGoal.Y, leftGoal.Width, leftGoal.Height);
                    graphics.DrawRectangle(goalPen, rightGoal.X, rightGoal.Y, rightGoal.Width, rightGoal.Height);
                    #endregion

                    #region Draw the goal areas (6-yard boxes)
                    // ... The 6 yard boxes
                    float widthOf6YardBoxInPixels = 6.0F;
                    float heightOf6YardBoxInPixels = 6.0F * 2.0F + 8.0F;
                    float halfHeightOf6YardBoxInPixels = 6.0F + 4.0F;
                    RectangleF leftSide6YardBox = new RectangleF(0.0F, fieldCentre.Y - halfHeightOf6YardBoxInPixels,
                      widthOf6YardBoxInPixels, heightOf6YardBoxInPixels);
                    RectangleF rightSide6YardBox = new RectangleF(Length - widthOf6YardBoxInPixels, fieldCentre.Y - halfHeightOf6YardBoxInPixels,
                      widthOf6YardBoxInPixels, heightOf6YardBoxInPixels);
                    graphics.DrawRectangle(linePen, leftSide6YardBox.X, leftSide6YardBox.Y, leftSide6YardBox.Width, leftSide6YardBox.Height);
                    graphics.DrawRectangle(linePen, rightSide6YardBox.X, rightSide6YardBox.Y, rightSide6YardBox.Width, rightSide6YardBox.Height);
                    #endregion

                    #region Draw the penalty areas (18-yard boxes)
                    //// ... The 18 yard boxes
                    float widthOf18YardBoxInPixels = 18.0F;
                    float heightOf18YardBoxInPixels = 18.0F * 2.0F + 8.0F;
                    float halfHeightOf18YardBoxInPixels = 18.0F + 4.0F;
                    RectangleF leftSide18YardBox = new RectangleF(0.0F, fieldCentre.Y - halfHeightOf18YardBoxInPixels,
                      widthOf18YardBoxInPixels, heightOf18YardBoxInPixels);
                    RectangleF rightSide18YardBox = new RectangleF(Length - widthOf18YardBoxInPixels, fieldCentre.Y - halfHeightOf18YardBoxInPixels,
                      widthOf18YardBoxInPixels, heightOf18YardBoxInPixels);

                    if (Use18yard)
                    {
                        graphics.DrawRectangle(linePen, leftSide18YardBox.X, leftSide18YardBox.Y, leftSide18YardBox.Width, leftSide18YardBox.Height);
                        graphics.DrawRectangle(linePen, rightSide18YardBox.X, rightSide18YardBox.Y, rightSide18YardBox.Width, rightSide18YardBox.Height);
                    }
                    #endregion

                    #region Draw penalty marks
                    // ... The penalty marks

                    if (UsePenaltyMarks)
                    {
                        RectangleF leftPenaltyMark = new RectangleF(0.0F + 12.0F - PenaltyMarkRadius, fieldCentre.Y - PenaltyMarkRadius, PenaltyMarkDiameter, PenaltyMarkDiameter);
                        RectangleF rightPenaltyMark = new RectangleF(Length - 12.0F - PenaltyMarkRadius, fieldCentre.Y - PenaltyMarkRadius, PenaltyMarkDiameter, PenaltyMarkDiameter);
                        graphics.DrawEllipse(linePen, leftPenaltyMark);
                        graphics.DrawEllipse(linePen, rightPenaltyMark);

                        #endregion

                        #region Draw the "Ds" at the top of the penalty boxes
                        // ... The Ds
                        Region oldClip = graphics.Clip;
                        using (Region penaltyBoxExcludeRegion = new Region(leftSide18YardBox))
                        {
                            penaltyBoxExcludeRegion.Union(rightSide18YardBox);
                            graphics.ExcludeClip(penaltyBoxExcludeRegion);
                            float dRadiusInPixels = 10.0F;
                            float dDiameterInPixels = 20.0F;
                            RectangleF leftDRectangle = new RectangleF(leftPenaltyMark.Left - dRadiusInPixels,
                              leftPenaltyMark.Top - dRadiusInPixels, dDiameterInPixels, dDiameterInPixels);
                            RectangleF rightDRectangle = new RectangleF(rightPenaltyMark.Left - dRadiusInPixels,
                              rightPenaltyMark.Top - dRadiusInPixels, dDiameterInPixels, dDiameterInPixels);
                            graphics.DrawEllipse(linePen, leftDRectangle);
                            graphics.DrawEllipse(linePen, rightDRectangle);
                            graphics.Clip = oldClip;
                        }
                    }
                    #endregion

                    #region Draw the corner arcs
                    // ... The corner arcs
                    float cornerArcDiameterInPixels = 2.0F;
                    float cornerArcRadiusInPixels = 1.0F;
                    RectangleF topLeftCornerArc = new RectangleF(0.0F - cornerArcRadiusInPixels,
                      0.0F - cornerArcRadiusInPixels, cornerArcDiameterInPixels, cornerArcDiameterInPixels);
                    RectangleF topRightCornerArc = new RectangleF(Length - cornerArcRadiusInPixels,
                      0.0F - cornerArcRadiusInPixels, cornerArcDiameterInPixels, cornerArcDiameterInPixels);
                    RectangleF bottomLeftCornerArc = new RectangleF(0.0F - cornerArcRadiusInPixels,
                      Width - cornerArcRadiusInPixels, cornerArcDiameterInPixels, cornerArcDiameterInPixels);
                    RectangleF bottomRightCornerArc = new RectangleF(Length - cornerArcRadiusInPixels,
                      Width - cornerArcRadiusInPixels, cornerArcDiameterInPixels, cornerArcDiameterInPixels);
                    graphics.DrawArc(linePen, topLeftCornerArc, 0.0F, 90.0F);
                    graphics.DrawArc(linePen, topRightCornerArc, 90.0F, 90.0F);
                    graphics.DrawArc(linePen, bottomLeftCornerArc, 270.0F, 90.0F);
                    graphics.DrawArc(linePen, bottomRightCornerArc, 180.0F, 90.0F);
                    #endregion
                }
            }
        }
    }
}
