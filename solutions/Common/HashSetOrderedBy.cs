using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Common
{
    public class HashSetOrderedBy<T, TS> : ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>, ISet<T>
    {
        private readonly HashSet<T> set;
        private readonly SortedDictionary<TS, HashSet<T>> sortingMap;
        private readonly Func<T, TS> KeySelector;

        private HashSetOrderedBy() : this(null)
        {
        }

        public HashSetOrderedBy(Func<T, TS> selector)
        {
            this.KeySelector = selector;
            this.set = new HashSet<T>();
            this.sortingMap = new SortedDictionary<TS, HashSet<T>>();
        }

        public T ValueWithMinSelector()
        {
            var kvp = this.sortingMap.First();
            return kvp.Value.First();
        }

        public int Count => set.Count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            ((ISet<T>)this).Add(item);
        }

        bool ISet<T>.Add(T item)
        {
            var added = set.Add(item);
            var key = KeySelector(item);

            if (added)
            {
                if (!sortingMap.ContainsKey(key))
                {
                    sortingMap.Add(key, new HashSet<T>());
                }
                sortingMap[key].Add(item);
            }

            return added;
        }

        public void Clear()
        {
            set.Clear();
            sortingMap.Clear();
        }

        public bool Contains(T item)
        {
            return set.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return set.GetEnumerator();
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return set.IsProperSubsetOf(other);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return set.IsProperSupersetOf(other);
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return set.IsSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return set.IsSupersetOf(other);
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            return set.Overlaps(other);
        }

        public bool TryGetValue(T equalValue, out T actualValue)
        {
            return set.TryGetValue(equalValue, out actualValue);
        }

        public bool Remove(T item)
        {
            if (set.TryGetValue(item, out var actualItem))
            {
                var key = KeySelector(actualItem);
                set.Remove(actualItem);
                sortingMap[key].Remove(actualItem);
                if (!sortingMap[key].Any())
                {
                    sortingMap.Remove(key);
                }

                return true;
            }

            return false;
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            return set.SetEquals(other);
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void UnionWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)set).GetEnumerator();
        }
    }
}
