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
using System.Xml.Serialization;

namespace SportsTacticsBoard
{
  [XmlType(TypeName = "customLabelCollection")]
  public class CustomLabelCollection : ICollection<CustomLabel>
  {
    private Dictionary<string, CustomLabel> dictionary = new Dictionary<string, CustomLabel>();

    public string Get(string tag)
    {
      if (null == dictionary) {
        return null;
      }
      CustomLabel l;
      if (dictionary.TryGetValue(tag, out l)) {
        return l.Label;
      }
      return null;
    }

    public void AddOrUpdate(string tag, string customLabel)
    {
      CustomLabel l;
      if (dictionary.TryGetValue(tag, out l)) {
        l.Label = customLabel;
      } else {
        l = new CustomLabel() { Tag = tag, Label = customLabel };
        dictionary.Add(tag, l);
      }
    }

    public void Remove(string tag)
    {
      dictionary.Remove(tag);
    }

    #region ICollection<CustomLabel> Members

    public void Add(CustomLabel item)
    {
      if (null != item) {
        AddOrUpdate(item.Tag, item.Label);
      }
    }

    public void Clear()
    {
      dictionary.Clear();
    }

    public bool Contains(CustomLabel item)
    {
      if (null == item) {
        return false;
      }
      CustomLabel l;
      if (dictionary.TryGetValue(item.Tag, out l)) {
        return l.Label == item.Label;
      }
      return false;
    }

    public void CopyTo(CustomLabel[] array, int arrayIndex)
    {
      dictionary.Values.CopyTo(array, arrayIndex);
    }

    public int Count
    {
      get { return dictionary.Count; }
    }

    public bool IsReadOnly
    {
      get { return false; }
    }

    public bool Remove(CustomLabel item)
    {
      if (null == item) {
        return false;
      }
      CustomLabel l;
      if (dictionary.TryGetValue(item.Tag, out l)) {
        if (l.Label == item.Label) {
          dictionary.Remove(item.Tag);
          return true;
        }
      }
      return false;
    }

    #endregion

    #region IEnumerable<CustomLabel> Members

    public IEnumerator<CustomLabel> GetEnumerator()
    {
      return dictionary.Values.GetEnumerator();
    }

    #endregion

    #region IEnumerable Members

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    #endregion
  }
}