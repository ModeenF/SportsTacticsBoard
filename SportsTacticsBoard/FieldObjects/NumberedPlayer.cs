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

using System.Globalization;

namespace SportsTacticsBoard.FieldObjects
{
    public class NumberedPlayer : Player
    {
        public int Number { get; }

        public override string Label
        {
            get { return Number.ToString(CultureInfo.CurrentCulture); }
        }

        public static string ComposeTag(TeamId team, int playerNumber)
        {
            return ComposeTag(team, playerNumber.ToString(CultureInfo.InvariantCulture));
        }

        public NumberedPlayer(int number, TeamId team, float dispRadius)
          : base(team, dispRadius)
        {
            Number = number;
        }
    }
}