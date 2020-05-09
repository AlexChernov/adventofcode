namespace AdventOfCode.Solutions.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The hash set stored by hash code, but ordered by specified selector.
    /// </summary>
    /// <typeparam name="T">The type of stored date.</typeparam>
    /// <typeparam name="TS">The type of selector.</typeparam>
    public class HashSetOrderedBy<T, TS> : ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>, ISet<T>
    {
        private readonly HashSet<T> set;
        private readonly SortedDictionary<TS, HashSet<T>> sortingMap;
        private readonly Func<T, TS> keySelector;

        /// <summary>
        /// Initializes a new instance of the <see cref="HashSetOrderedBy{T, TS}"/> class.
        /// </summary>
        /// <param name="selector">The selector of value to be ordered by.</param>
        public HashSetOrderedBy(Func<T, TS> selector)
        {
            this.keySelector = selector;
            this.set = new HashSet<T>();
            this.sortingMap = new SortedDictionary<TS, HashSet<T>>();
        }

        private HashSetOrderedBy()
            : this(null)
        {
        }

        /// <inheritdoc/>
        public int Count => this.set.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        /// <summary>
        /// Gets the value with min selector.
        /// </summary>
        /// <returns>The value with min selector.</returns>
        public T ValueWithMinSelector()
        {
            var kvp = this.sortingMap.First();
            return kvp.Value.First();
        }

        /// <inheritdoc/>
        public void Add(T item)
        {
            ((ISet<T>)this).Add(item);
        }

        /// <inheritdoc/>
        bool ISet<T>.Add(T item)
        {
            var added = this.set.Add(item);
            var key = this.keySelector(item);

            if (added)
            {
                if (!this.sortingMap.ContainsKey(key))
                {
                    this.sortingMap.Add(key, new HashSet<T>());
                }

                this.sortingMap[key].Add(item);
            }

            return added;
        }

        /// <inheritdoc/>
        public void Clear()
        {
            this.set.Clear();
            this.sortingMap.Clear();
        }

        /// <inheritdoc/>
        public bool Contains(T item)
        {
            return this.set.Contains(item);
        }

        /// <inheritdoc/>
        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void ExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void IntersectWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            return this.set.GetEnumerator();
        }

        /// <inheritdoc/>
        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return this.set.IsProperSubsetOf(other);
        }

        /// <inheritdoc/>
        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return this.set.IsProperSupersetOf(other);
        }

        /// <inheritdoc/>
        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return this.set.IsSubsetOf(other);
        }

        /// <inheritdoc/>
        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return this.set.IsSupersetOf(other);
        }

        /// <inheritdoc/>
        public bool Overlaps(IEnumerable<T> other)
        {
            return this.set.Overlaps(other);
        }

        /// <summary>
        /// Gets the actual value by equal value if existed.
        /// </summary>
        /// <param name="equalValue">The equal value.</param>
        /// <param name="actualValue">The actual value.</param>
        /// <returns>True if the element is successfully found and removed; otherwise, false.</returns>
        public bool TryGetValue(T equalValue, out T actualValue)
        {
            return this.set.TryGetValue(equalValue, out actualValue);
        }

        /// <inheritdoc/>
        public bool Remove(T item)
        {
            if (this.set.TryGetValue(item, out var actualItem))
            {
                var key = this.keySelector(actualItem);
                this.set.Remove(actualItem);
                this.sortingMap[key].Remove(actualItem);
                if (!this.sortingMap[key].Any())
                {
                    this.sortingMap.Remove(key);
                }

                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public bool SetEquals(IEnumerable<T> other)
        {
            return this.set.SetEquals(other);
        }

        /// <inheritdoc/>
        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void UnionWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.set).GetEnumerator();
        }
    }
}
