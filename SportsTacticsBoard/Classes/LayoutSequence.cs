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
using System.Xml.Serialization;

namespace SportsTacticsBoard
{
    [XmlType(TypeName = "SportsTacticsBoardDocument")]
    public class LayoutSequence
    {
        [XmlElement(ElementName = "fieldType")]
        public string FieldTypeTag { get; set; }

        [XmlElement(ElementName = "layoutSequence")]
        public Collection<FieldLayout> Sequence { get; }

        [XmlArray(ElementName = "CustomLabels")]
        public CustomLabelCollection CustomLabels { get; }

        public LayoutSequence()
        {
            Sequence = new Collection<FieldLayout>();
            CustomLabels = new CustomLabelCollection();
        }

        public LayoutSequence(string fieldTypeTag)
        {
            Sequence = new Collection<FieldLayout>();
            CustomLabels = new CustomLabelCollection();
            FieldTypeTag = fieldTypeTag;
        }

        public int NumberOfLayouts
        {
            get { return Sequence.Count; }
        }

        public FieldLayout GetLayout(int index)
        {
            if ((index >= 0) && (index < Sequence.Count))
            {
                return Sequence[index];
            }
            else
            {
                return null;
            }
        }

        public void SetLayout(int index, FieldLayout layout)
        {
            if ((index >= 0) && (index < Sequence.Count))
            {
                Sequence[index] = layout;
            }
        }

        public int AddNewLayout(int index, FieldLayout layout)
        {
            if (index >= Sequence.Count)
            {
                Sequence.Add(layout);
                return Sequence.Count - 1;
            }
            else
            {
                Sequence.Insert(index, layout);
                return index;
            }
        }

        public void RemoveFromSequence(int index)
        {
            if ((index >= 0) && (index < Sequence.Count))
            {
                Sequence.RemoveAt(index);
            }
        }

        public string GetCustomLabel(string tag)
        {
            if (CustomLabels != null)
            {
                CustomLabels.Get(tag);
            }

            return null;
        }

        public void AddOrUpdateCustomLabel(string tag, string customLabel)
        {
            if (CustomLabels != null)
            {
                CustomLabels.AddOrUpdate(tag, customLabel);
            }

            return;
        }

        public void RemoveCustomLabel(string tag)
        {
            if (CustomLabels != null)
            {
                CustomLabels.Remove(tag);
            }

            return;
        }
    }
}
