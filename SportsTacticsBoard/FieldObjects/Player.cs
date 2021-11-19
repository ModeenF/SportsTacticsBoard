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
using System.Globalization;

namespace SportsTacticsBoard.FieldObjects
{
    public class Player : Person
    {
        public enum TeamId
        {
            Attacking,
            Defending
        }

        public TeamId Team { get; }

        public override string Tag
        {
            get { return ComposeTag(Team, Label); }
        }

        public override string Name
        {
            get { return ComposeName(Team, Label); }
        }

        public string ComposeName(TeamId team, string playerLabel)
        {
            var nameFormat = resourceManager.LocalizationResource.FieldObjectPlayerNameFormat;
            var teamName = team == TeamId.Attacking ?
                resourceManager.LocalizationResource.TeamNameAttacking :
                resourceManager.LocalizationResource.TeamNameDefending;
            return string.Format(CultureInfo.CurrentUICulture, nameFormat, teamName, playerLabel);
        }

        public static string ComposeTag(TeamId team, string playerLabel)
        {
            return "Player_" + team + "_" + playerLabel;
        }

        public Player(TeamId team, float dispRadius, Color? outlinePenColor = null,
            Color? fillBrushColorDefending = null, Color? fillBrushColorAttacking = null,
            Color? movementPenColorDefending = null, Color? movementPenColorAttacking = null) :
          base(0.0F, 0.0F, dispRadius)
        {
            Team = team;

            if (outlinePenColor == null)
                OutlinePenColor = Color.White;
            else
                OutlinePenColor = outlinePenColor.Value;

            Color defendingColor = Color.LightBlue;
            if (fillBrushColorDefending != null)
                defendingColor = fillBrushColorDefending.Value;

            Color attackingColor = Color.Yellow;
            if (fillBrushColorAttacking != null)
                defendingColor = fillBrushColorAttacking.Value;

            FillBrushColor = team == TeamId.Defending ? defendingColor : attackingColor;

            defendingColor = Color.LightBlue;
            if (movementPenColorDefending != null)
                defendingColor = movementPenColorDefending.Value;

            attackingColor = Color.Yellow;
            if (movementPenColorAttacking != null)
                defendingColor = movementPenColorAttacking.Value;
            MovementPenColor = team == TeamId.Defending ? defendingColor : attackingColor;
        }
    }
}