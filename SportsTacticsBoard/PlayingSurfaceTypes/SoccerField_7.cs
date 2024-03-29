// Sports Tactics Board
//
// http://sportstacticsbd.sourceforge.net/
// http://sourceforge.net/projects/sportstacticsbd/
// 
// Sports Tactics Board is a utility that allows coaches, trainers and 
// officials to describe sports tactics, strategies and positioning using 
// a magnetic or chalk-board style approach.
// 
// Copyright (C) 2021- Fredrik Mod�en
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
using System.Drawing;

namespace SportsTacticsBoard.PlayingSurfaceTypes
{
    /// <summary>
    /// Swedish 7*7
    ///
    /// </summary>
    public class SoccerField7 : SoccerField
    {
        public SoccerField7()
        {
            Length = 50.0F;
            Width = 30.0F;
            Margin = 6.5F;
            PlayerSize = 0.60F;
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
            PlayersPerTeam = 7;
            Tag = "Soccer" + PlayersPerTeam;
            UseCenterCircel = false;
            UseAllRef = false;
            UsePenaltyMarks = false;
            Use18yard = false;
            UseRetreatLine = true;

            WidthOf6YardBoxInPixels = 4.0F;
            HeightOf6YardBoxInPixels = 6.0F * 2.0F + 8.0F;
            HalfHeightOf6YardBoxInPixels = 6.0F + 4.0F;
        }

        public override string Name
        {
            get
            {
                ResourceManager resourceManager = ResourceManager.GetInstance();
                return resourceManager.LocalizationResource.FieldTypeSoccer7;
            }
        }
    }
}
