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
using System.Globalization;

namespace SportsTacticsBoard.FieldObjects
{
    public abstract class Player : Person
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

        public static string ComposeName(TeamId team, string playerLabel)
        {
            var nameFormat = ResourceManager.LocalizationResource.FieldObjectPlayerNameFormat;
            var teamName = team == TeamId.Attacking ?
                ResourceManager.LocalizationResource.TeamNameAttacking :
                ResourceManager.LocalizationResource.TeamNameDefending;
            return string.Format(CultureInfo.CurrentUICulture, nameFormat, teamName, playerLabel);
        }

        public static string ComposeTag(TeamId team, string playerLabel)
        {
            return "Player_" + team + "_" + playerLabel;
        }

        private static Color GetTeamColor(TeamId team)
        {
            switch (team)
            {
                case TeamId.Defending:
                    return Color.LightBlue;
                case TeamId.Attacking:
                default:
                    return Color.Yellow;
            }
        }

        public Player(TeamId team, float dispRadius) :
          base(0.0F, 0.0F, dispRadius)
        {
            Team = team;
            OutlinePenColor = Color.White;
            MovementPenColor = GetTeamColor(team);
            FillBrushColor = GetTeamColor(team);
        }
    }
}