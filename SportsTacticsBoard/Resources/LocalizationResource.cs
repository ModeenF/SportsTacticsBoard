// Sports Tactics Board
//
// http://github.com/manio143/SportsTacticsBoard
// 
// Sports Tactics Board is a utility that allows coaches, trainers and 
// officials to describe sports tactics, strategies and positioning using 
// a magnetic or chalk-board style approach.
// 
// Copyright (C) 2016 Marian Dziubiak
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
using System;

namespace SportsTacticsBoard.Resources
{
    [Serializable]
    public class LocalizationResource
    {
        public string CurrentLayoutNumberEmpty { get; set; }
        public string CurrentLayoutNumberFormat { get; set; }
        public string CustomLabelLongerThanRecommended { get; set; }
        public string ExceptionMessageLayoutFieldTagInvalid { get; set; }
        public string ExceptionMessageLayoutNameInvalid { get; set; }
        public string FailedToOpenFileFormatStr { get; set; }
        public string FieldObjectBall { get; set; }
        public string FieldObjectBaseConeLabelFormat { get; set; }
        public string FieldObjectBaseConeNameFormat { get; set; }
        public string FieldObjectPlayer { get; set; }
        public string FieldObjectPlayerNameFormat { get; set; }
        public string FieldObjectPuck { get; set; }
        public string FieldObjectReferee_Soccer_CR { get; set; }
        public string FieldObjectReferee_Soccer_AR1 { get; set; }
        public string FieldObjectReferee_Soccer_AR2 { get; set; }
        public string FieldObjectReferee_Soccer_4th { get; set; }
        public string FieldTypeFloorball { get; set; }
        public string FieldTypeFutsal { get; set; }
        public string FieldTypeHandball { get; set; }
        public string FieldTypeHockeyNhl { get; set; }
        public string FieldTypeNflFootball { get; set; }
        public string FieldTypeSoccer { get; set; }
        public string FieldTypeVolleyball { get; set; }
        public string FileFilter { get; set; }
        public string ImageFileNamePattern { get; set; }
        public string InvalidCultureOptionFormat { get; set; }
        public string InvalidParametersTitle { get; set; }
        public string MissingCultureOptionValueFormat { get; set; }
        public string NoSavedLayoutsMenuItemText { get; set; }
        public string NotImplementedYet { get; set; }
        public string SaveAsImageFileFilter { get; set; }
        public string SavedLayoutInformationErrorMessageAtLeastOneItemMustBeChecked { get; set; }
        public string SavedLayoutInformationErrorMessageNameMustNotBeBlank { get; set; }
        public string SaveImageDialogTitle { get; set; }
        public string SaveImageSequenceDialogTitle { get; set; }
        public string SaveSequenceEntryBeforeSwitchingEntries { get; set; }
        public string TeamNameAttacking { get; set; }
        public string TeamNameDefending { get; set; }
        public string TitleFormatString { get; set; }
        public string UnableToOpenFileInstallationMayBeIncomplete { get; set; }

        public string MenuAbout { get; set; }
        public string MenuChangePlayingSurfaceType { get; set; }
        public string MenuCommonSavedLayouts { get; set; }
        public string MenuCopy { get; set; }
        public string MenuCopyTooltip { get; set; }
        public string MenuExit { get; set; }
        public string MenuExport { get; set; }
        public string MenuExportTooltip { get; set; }
        public string MenuHelp { get; set; }
        public string MenuOrientation { get; set; }
        public string MenuHorizontal { get; set; }
        public string MenuVertical { get; set; }
        public string MenuLayout { get; set; }
        public string MenuNewSequence { get; set; }
        public string MenuOpenSequence { get; set; }
        public string MenuOptions { get; set; }
        public string MenuPlayingSurface { get; set; }
        public string MenuPrint { get; set; }
        public string MenuRemoveSavedLayout { get; set; }
        public string MenuResetView { get; set; }
        public string MenuSaveCurrentLayout { get; set; }
        public string MenuSaveSequenceAs { get; set; }
        public string MenuSaveSequence { get; set; }
        public string MenuSequence { get; set; }
        public string MenuUserSavedLayouts { get; set; }

        public string GoToFirst { get; set; }
        public string GoToLast { get; set; }
        public string PreviousLayout { get; set; }
        public string NextLayout { get; set; }
        public string PlayPause { get; set; }
        public string RecordNewPosition { get; set; }
        public string RecordOverCurrentPosition { get; set; }
        public string RemoveCurrent { get; set; }
        public string Repeat { get; set; }
        public string RevertCurrent { get; set; }
        public string ShowMovement { get; set; }

        public string Version { get; set; }
        public string License { get; set; }

        public string CustomLabel { get; set; }
        public string FieldObject { get; set; }
        public string RevertToDefault { get; set; }
        public string Ok { get; set; }
        public string Cancel { get; set; }

        public string ChangeLabel { get; set; }       
    }
}
