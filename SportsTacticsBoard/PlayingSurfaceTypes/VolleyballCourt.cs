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

using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SportsTacticsBoard.PlayingSurfaceTypes
{
  /// <summary>
  /// Implements a standard Volleyball court
  /// Volleyball court dimensions and detals taken from:
  /// http://en.wikipedia.org/wiki/Volleyball   
  /// Author: Lukasz Kawalec - kadeon
  /// </summary>
  class VolleyballCourt : SportsTacticsBoard.IPlayingSurfaceType
  {
    // Field units are in metres
    private const float fieldLength = 18.0F;
    private const float fieldWidth = 9.0F;
    private const float margin = 5.0F;
    private const float playerSize = 0.35F;
    private const float ballSize = 0.21F;
    private const float linePenWidth = 0.05F;
    private const float fieldObjectOutlinePenWidth = 0.05F;
    private const float fieldObjectMovementPenWidth = fieldObjectOutlinePenWidth * 3.0F;

    private const int playersPerTeam = 6;


    public string Tag
    {
      get { return "Volleyball"; }
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
        fieldObjects.Add(new FieldObjects.Ball(Length + Length / 10, Width / 5, ballSize));

        // Add the referees
        fieldObjects.Add(new FieldObjects.Referee("N", "Referee_Volleyball_N", Length / 2.0F, -(Width / 16.0F), playerSize));
        fieldObjects.Add(new FieldObjects.Referee("S", "Referee_Volleyball_S", Length / 2.0F, Width + (Width / 16.0F), playerSize));
        fieldObjects.Add(new FieldObjects.Referee("NW", "Referee_Volleyball_NW", -Length / 16.0F, -Width / 16.0F, playerSize));
        fieldObjects.Add(new FieldObjects.Referee("NE", "Referee_Volleyball_NE", Length + Length / 16.0F, -Width / 16.0F, playerSize));
        fieldObjects.Add(new FieldObjects.Referee("SW", "Referee_Volleyball_SW", -Length / 16.0F, Width + Width / 16.0F, playerSize));
        fieldObjects.Add(new FieldObjects.Referee("SE", "Referee_Volleyball_SE", Length + Length / 16.0F, Width + Width / 16.0F, playerSize));

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
      layout.AddEntry(playerTag, Length / 4, Width - Width / 16);
      playerTag = FieldObjects.NumberedPlayer.ComposeTag(teamId, 2);
      layout.AddEntry(playerTag, Length / 2 - Length / 32, Width / 2);
      playerTag = FieldObjects.NumberedPlayer.ComposeTag(teamId, 3);
      layout.AddEntry(playerTag, Length / 4, Width / 16);
      playerTag = FieldObjects.NumberedPlayer.ComposeTag(teamId, 4);
      layout.AddEntry(playerTag, Length / 6, Width / 5);
      playerTag = FieldObjects.NumberedPlayer.ComposeTag(teamId, 5);
      layout.AddEntry(playerTag, Length / 5, Width / 2);
      playerTag = FieldObjects.NumberedPlayer.ComposeTag(teamId, 6);
      layout.AddEntry(playerTag, Length / 6, Width - Width / 5);

      // Add defending players
      teamId = SportsTacticsBoard.FieldObjects.Player.TeamId.Defending;
      playerTag = FieldObjects.NumberedPlayer.ComposeTag(teamId, 1);
      layout.AddEntry(playerTag, Length / 2 + Length / 32, Width / 2 - Width / 12);
      playerTag = FieldObjects.NumberedPlayer.ComposeTag(teamId, 2);
      layout.AddEntry(playerTag, Length / 2 + Length / 32, Width / 2);
      playerTag = FieldObjects.NumberedPlayer.ComposeTag(teamId, 3);
      layout.AddEntry(playerTag, Length / 2 + Length / 32, Width / 2 + Width / 12);
      playerTag = FieldObjects.NumberedPlayer.ComposeTag(teamId, 4);
      layout.AddEntry(playerTag, Length - Length / 6, Width - Width / 5);
      playerTag = FieldObjects.NumberedPlayer.ComposeTag(teamId, 5);
      layout.AddEntry(playerTag, Length - Length / 4, Width / 4);
      playerTag = FieldObjects.NumberedPlayer.ComposeTag(teamId, 6);
      layout.AddEntry(playerTag, Length + Length / 8, Width / 5);

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

    public ReadOnlyCollection<string> GetTeam(SportsTacticsBoard.FieldObjects.Player.TeamId team)
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
      using (Pen linePen = new Pen(Color.White, linePenWidth)) {
        using (Brush courtBrush = new SolidBrush(Color.Orange)) {

          // Draw the court surface
          graphics.FillRectangle(courtBrush, 0.0F, 0.0F, Length, Width);

          #region Draw main lines
          // Draw main lines
          graphics.DrawRectangle(linePen, 0.0F, 0.0F, Length, Width);

          // Draw small horizontal main lines extension
          linePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
          graphics.DrawLine(linePen, -Length / 32, 0.0F, 0.0F, 0.0F);
          graphics.DrawLine(linePen, -Length / 32, Width, 0.0F, Width);
          graphics.DrawLine(linePen, Length, 0.0F, Length + Length / 32, 0.0F);
          graphics.DrawLine(linePen, Length, Width, Length + Length / 32, Width);
          linePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
          #endregion

          #region Draw central line
          PointF fieldCenter = new PointF(Length / 2, Width / 2);
          PointF centerLineTop = new PointF(fieldCenter.X, 0.0F);
          PointF centerLineBottom = new PointF(fieldCenter.X, Width);

          linePen.Width *= 2.0F;
          graphics.DrawLine(linePen, centerLineTop, centerLineBottom);
          linePen.Width /= 2.0F;
          #endregion

          #region Draw attack line
          const float distanceFromCentralLine = 3.0F;
          const float distanceOutsideField = 3.0F;

          // Draw left attack line 
          PointF attackLineTop = new PointF(centerLineTop.X - distanceFromCentralLine, centerLineTop.Y);
          PointF attackLineBottom = new PointF(centerLineBottom.X - distanceFromCentralLine, centerLineBottom.Y);
          graphics.DrawLine(linePen, attackLineTop, attackLineBottom);

          // Draw left attack line extension
          linePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
          graphics.DrawLine(linePen, new PointF(attackLineTop.X, attackLineTop.Y - distanceOutsideField), attackLineTop);
          graphics.DrawLine(linePen, new PointF(attackLineBottom.X, attackLineBottom.Y + distanceOutsideField), attackLineBottom);

          // Draw right attack line extension
          linePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
          attackLineTop.X = centerLineTop.X + distanceFromCentralLine;
          attackLineBottom.X = centerLineBottom.X + distanceFromCentralLine;
          graphics.DrawLine(linePen, attackLineTop, attackLineBottom);

          // Draw right attack line extension
          linePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
          graphics.DrawLine(linePen, new PointF(attackLineTop.X, attackLineTop.Y - distanceOutsideField), attackLineTop);
          graphics.DrawLine(linePen, new PointF(attackLineBottom.X, attackLineBottom.Y + distanceOutsideField), attackLineBottom);

          linePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
          #endregion
        }
      }
    }

  }
}
