// This code is distributed under MIT license. 
// Copyright (c) 2010-2018 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Gma.System.MouseKeyHook
{
    public class Sequence : SequenceBase<TriggerChord>
    {
        public Sequence(params TriggerChord[] triggerChords) : base(triggerChords)
        {
            
        } 
    }


    public abstract class SequenceBase<T> : IEnumerable<T>
    {
        private readonly T[] _elements;

        protected SequenceBase(params T[] elements)
        {
            _elements = elements;
        }

        public int Length
        {
            get { return _elements.Length; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _elements.Cast<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join(",", _elements);
        }


        protected bool Equals(SequenceBase<T> other)
        {
            if (_elements.Length != other._elements.Length) return false;
            return _elements.SequenceEqual(other._elements);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TriggerChord) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_elements.Length + 13) ^
                       ((_elements.Length != 0
                            ? _elements[0].GetHashCode() ^ _elements[_elements.Length - 1].GetHashCode()
                            : 0) * 397);
            }
        }

        public static bool operator ==(SequenceBase<T> left, SequenceBase<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SequenceBase<T> left, SequenceBase<T> right)
        {
            return !Equals(left, right);
        }
    }
}