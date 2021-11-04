// Sports Tactics Board
//
// http://sportstacticsbd.sourceforge.net/
// http://sourceforge.net/projects/sportstacticsbd/
// 
// Sports Tactics Board is a utility that allows coaches, trainers and 
// officials to describe sports tactics, strategies and positioning using 
// a magnetic or chalk-board style approach.
// 
// Copyright (C) 2012 Robert Turner
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
  /// Implements a standard Handball pitch based on the FIFA Futsal pitch.
  /// 
  /// Playing surface units are in metres and the default pitch 
  /// dimensions are 40 metres by 22 metres. Pitch is drawn to
  /// correct dimensions without any specific compromises.
  /// 
  /// 2 referee field objects are supported for referee training.
  /// </summary>
  class Handball : FutsalHandballBase
  {
    public override string Tag
    {
      get { return "Handball"; }
    }

    protected override int PlayersPerTeam
    {
      get { return 7; }
    }
  }
}
