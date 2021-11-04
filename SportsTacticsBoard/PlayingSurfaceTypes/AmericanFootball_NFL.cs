// Sports Tactics Board
//
// http://sportstacticsbd.sourceforge.net/
// http://sourceforge.net/projects/sportstacticsbd/
// 
// Sports Tactics Board is a utility that allows coaches, trainers and 
// officials to describe sports tactics, strategies and positioning using 
// a magnetic or chalk-board style approach.
// 
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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SportsTacticsBoard.PlayingSurfaceTypes
{
  /// <summary>
  /// Implements an American football field, as per NFL markings and dimensions.
  /// 
  /// Playing surface units are in feet.
  /// </summary>
  class AmericanFootball_NFL : SportsTacticsBoard.IPlayingSurfaceType
  {
    private const float fieldLength = 360.0F;
    private const float fieldWidth = 160.0F;
    private const float margin = 18.0F;
    private const float endZoneDepth = 30.0F;
    private const float outOfBoundsWidth = 6.0F;
    private const float playerSize = 2.5F;
    private const float ballSize = 1.5F;
    private const float fieldObjectOutlinePenWidth = 3.0F / 36.0F;
    private const float fieldObjectMovementPenWidth = fieldObjectOutlinePenWidth * 3.0F;
    private const float coneSize = 1.50F;

    public string Tag
    {
      get { return "NFLFootball"; }
    }

    public string Name
    {
      get {
        return Properties.Resources.ResourceManager.GetString("FieldType_" + Tag);
      }
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
      get { return Color.Green; }
    }

    private static string MakeLabelFromTag(string s)
    {
      if (char.IsDigit(s[s.Length - 1])) {
        return s.Substring(0, s.Length - 1);
      } else {
        return s;
      }
    }

    private static string[] attackingPlayers = new string[] { "QB", "C", "OG1", "OG2", "OG3", "OT1", "OT2", "OT3", "WR1", "WR2", "WR3", "WR4", "TE1", "TE2", "TE3", "TE4", "FB", "TB", "P", "K", "LS", };
    private static string[] defendingPlayers = new string[] { "DL1", "DL2", "DL3", "DL4", "DE1", "DE2", "DE3", "LB1", "LB2", "LB3", "LB4", "LB5", "LB6", "CB1", "CB2", "DB1", "DB2", "DB3", "DB4", "FS1", "FS2", "SS1", "SS2", "R1", "R2", "R3" };

    public Collection<FieldObject> StandardObjects
    {
      get {
        List<FieldObject> fieldObjects = new List<FieldObject>();

        // Add some players
        foreach (var s in attackingPlayers) {
          fieldObjects.Add(new FieldObjects.LabelledPlayer(MakeLabelFromTag(s), FieldObjects.Player.TeamId.Attacking, playerSize, s));
        }
        foreach (var s in defendingPlayers) {
          fieldObjects.Add(new FieldObjects.LabelledPlayer(MakeLabelFromTag(s), FieldObjects.Player.TeamId.Defending, playerSize, s));
        }

        // Add some cones
        const int NumberOfCones = 20;
        var xPosition = (Length / 2) - ((NumberOfCones * coneSize * 3F) / 2F);
        var yPosition = fieldWidth + 12.0F;
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

    public FieldLayout DefaultLayout
    {
      get {
        FieldLayout layout = new FieldLayout();
        const float offsetFromCentre = 12.0F;
        const float positionY = -12.0F;
        const float positionIncrement = playerSize * 2.25F;
        float positionX = (fieldLength / 2.0F) + offsetFromCentre;
        foreach (var s in attackingPlayers) {
          string playerTag = FieldObjects.LabelledPlayer.ComposeTag(FieldObjects.Player.TeamId.Attacking, s);
          layout.AddEntry(playerTag, positionX, positionY);
          positionX += positionIncrement;
        }
        positionX = (fieldLength / 2.0F) - offsetFromCentre;
        foreach (var s in defendingPlayers) {
          string playerTag = FieldObjects.LabelledPlayer.ComposeTag(FieldObjects.Player.TeamId.Defending, s);
          layout.AddEntry(playerTag, positionX, positionY);
          positionX -= positionIncrement;
        }
        return layout;
      }
    }

    public ReadOnlyCollection<string> GetTeam(SportsTacticsBoard.FieldObjects.Player.TeamId team)
    {
      throw new System.NotImplementedException();
    }

    public void DrawMarkings(Graphics graphics)
    {
      const float lineInsetDistance = 1.0F;
      const float hashMarkLength = 2.0F;
      const float innerHashDistance = 70.0F + (9.0F / 12.0F);
      const float yardNumbersHeight = 6.0F;

      // Create the graphics resources required for drawing the field
      using (Brush outOfBoundsBrush = new SolidBrush(Color.White)) {
        using (Pen linePen = new Pen(Color.White, 0.5F)) {
          using (Pen goalPen = new Pen(Color.Gold, 1.0F)) {
            using (Font yardNumbers = new Font(FontFamily.GenericSansSerif, yardNumbersHeight, GraphicsUnit.World)) {
              // Draw the out-of-bounds area outside the field
              Region oldClip = graphics.Clip;
              RectangleF inBoundsRectangle = new RectangleF(0.0F, 0.0F, fieldLength, fieldWidth);
              using (Region inBoundsRegion = new Region(inBoundsRectangle)) {
                graphics.ExcludeClip(inBoundsRegion);
                RectangleF outOfBoundsRectangle = new RectangleF(inBoundsRectangle.Location, inBoundsRectangle.Size);
                outOfBoundsRectangle.Inflate(outOfBoundsWidth, outOfBoundsWidth);
                graphics.FillRectangle(outOfBoundsBrush, outOfBoundsRectangle);
                graphics.Clip = oldClip;
              }

              // Draw the end-lines, the 10-yard lines, and the hash marks
              for (float positionX = endZoneDepth; positionX <= fieldLength - endZoneDepth; positionX += 3.0F) {
                if (positionX % (5.0F * 3) == 0.0F) {
                  // Draw a full line
                  float inset = (positionX == endZoneDepth || positionX == fieldLength - endZoneDepth) ? 0.0F : lineInsetDistance;
                  graphics.DrawLine(linePen, positionX, 0.0F + inset, positionX, fieldWidth - inset);
                } else {
                  // Draw hash marks
                  // ... outer
                  graphics.DrawLine(linePen, positionX, 0.0F + lineInsetDistance, positionX, 0.0F + lineInsetDistance + hashMarkLength);
                  graphics.DrawLine(linePen, positionX, fieldWidth - lineInsetDistance - hashMarkLength, positionX, fieldWidth - lineInsetDistance);
                  // ... inner
                  graphics.DrawLine(linePen, positionX, 0.0F + innerHashDistance - hashMarkLength, positionX, 0.0F + innerHashDistance);
                  graphics.DrawLine(linePen, positionX, fieldWidth - innerHashDistance, positionX, fieldWidth - innerHashDistance + hashMarkLength);
                }
              }

              // Draw the yard numbers
              using (var stringFormat = new StringFormat()) {
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                stringFormat.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
                const float TextBoxWidth = 10.0F;
                const float TextBoxHeight = yardNumbersHeight;
                const float TextInsetDepth = 27.0F;
                const float arrowOffset = 2.0F;
                const float arrowWidth = 2.0F;
                for (int yardMarker = 10; yardMarker <= 90; yardMarker += 10) {
                  int yardLabel = (yardMarker > 50) ? 100 - yardMarker : yardMarker;
                  var textRect1 = new RectangleF(endZoneDepth + (3.0F * yardMarker) - (TextBoxWidth / 2), fieldWidth - TextInsetDepth, TextBoxWidth, TextBoxHeight);
                  var textRect2 = new RectangleF(endZoneDepth + (3.0F * yardMarker) - (TextBoxWidth / 2), TextInsetDepth - TextBoxHeight, TextBoxWidth, TextBoxHeight);
                  graphics.DrawString(yardLabel.ToString(System.Globalization.CultureInfo.CurrentCulture), yardNumbers, outOfBoundsBrush, textRect1, stringFormat);
                  graphics.DrawString(yardLabel.ToString(System.Globalization.CultureInfo.CurrentCulture), yardNumbers, outOfBoundsBrush, textRect2, stringFormat);
                  // Draw triangles pointing towards the end-zones
                  if (yardMarker <= 40) {
                    float arrow1MidPoint = (textRect1.Top + textRect1.Bottom) / 2;
                    float arrow2MidPoint = (textRect2.Top + textRect2.Bottom) / 2;
                    graphics.FillPolygon(outOfBoundsBrush, new PointF[] {
                      new PointF(textRect1.Left - arrowOffset - arrowWidth, arrow1MidPoint),
                      new PointF(textRect1.Left - arrowOffset, arrow1MidPoint - (arrowWidth / 2)),
                      new PointF(textRect1.Left - arrowOffset, arrow1MidPoint + (arrowWidth / 2)),
                      new PointF(textRect1.Left - arrowOffset - arrowWidth, arrow1MidPoint)
                    }, System.Drawing.Drawing2D.FillMode.Alternate);
                    graphics.FillPolygon(outOfBoundsBrush, new PointF[] {
                      new PointF(textRect2.Left - arrowOffset - arrowWidth, arrow2MidPoint),
                      new PointF(textRect2.Left - arrowOffset, arrow2MidPoint - (arrowWidth / 2)),
                      new PointF(textRect2.Left - arrowOffset, arrow2MidPoint + (arrowWidth / 2)),
                      new PointF(textRect2.Left - arrowOffset - arrowWidth, arrow2MidPoint)
                    }, System.Drawing.Drawing2D.FillMode.Alternate);
                  } else if (yardMarker >= 60) {
                    float arrow1MidPoint = (textRect1.Top + textRect1.Bottom) / 2;
                    float arrow2MidPoint = (textRect2.Top + textRect2.Bottom) / 2;
                    graphics.FillPolygon(outOfBoundsBrush, new PointF[] {
                      new PointF(textRect1.Right + arrowOffset + arrowWidth, arrow1MidPoint),
                      new PointF(textRect1.Right + arrowOffset, arrow1MidPoint - (arrowWidth / 2)),
                      new PointF(textRect1.Right + arrowOffset, arrow1MidPoint + (arrowWidth / 2)),
                      new PointF(textRect1.Right + arrowOffset + arrowWidth, arrow1MidPoint)
                    }, System.Drawing.Drawing2D.FillMode.Alternate);
                    graphics.FillPolygon(outOfBoundsBrush, new PointF[] {
                      new PointF(textRect2.Right + arrowOffset + arrowWidth, arrow2MidPoint),
                      new PointF(textRect2.Right + arrowOffset, arrow2MidPoint - (arrowWidth / 2)),
                      new PointF(textRect2.Right + arrowOffset, arrow2MidPoint + (arrowWidth / 2)),
                      new PointF(textRect2.Right + arrowOffset + arrowWidth, arrow2MidPoint)
                    }, System.Drawing.Drawing2D.FillMode.Alternate);
                  }
                }
              }

              // Draw markings to represent the goals
              const float goalInset = 2.0F;
              const float goalWidth = 18.5F;
              graphics.DrawLine(goalPen, 0.0F - goalInset, fieldWidth / 2, 0.0F, fieldWidth / 2);
              graphics.DrawLine(goalPen, 0.0F, (fieldWidth / 2) - (goalWidth / 2), 0.0F, (fieldWidth / 2) + (goalWidth / 2));
              graphics.DrawLine(goalPen, fieldLength, fieldWidth / 2, fieldLength + goalInset, fieldWidth / 2);
              graphics.DrawLine(goalPen, fieldLength, (fieldWidth / 2) - (goalWidth / 2), fieldLength, (fieldWidth / 2) + (goalWidth / 2));
            }
          }
        }
      }
    }
  }
}
