namespace AdventOfCode.Solutions.Event2015.Day17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic of the Day 17.
    /// </summary>
    public class Day17 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HasVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var containers = this.InitContainers(input);

            var cache = new Dictionary<Tuple<int, int>, int>();
            var combinations = this.Combinations(containers, 150, 0, cache);

            yield return combinations.ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var containers = this.InitContainers(input);

            var cache = new Dictionary<Tuple<int, int>, Tuple<int, int>>();
            var combinations = this.CombinationsWithMin(containers, 150, 0, cache);

            yield return combinations.ToString();
        }

        private IList<int> InitContainers(string input)
        {
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            var containers = lines
                .Select(line => int.TryParse(line, out var value) ? value : default(int?))
                .Where(value => value.HasValue)
                .Select(value => value.Value)
                .ToList();
            containers.Sort((a, b) => b.CompareTo(a));

            return containers;
        }

        private int Combinations(IList<int> containers, int value, int minIndex, Dictionary<Tuple<int, int>, int> cache)
        {
            var key = new Tuple<int, int>(value, minIndex);
            if (cache.TryGetValue(key, out var ret))
            {
                return ret;
            }

            var count = 0;
            for (var i = minIndex; i < containers.Count; ++i)
            {
                if (containers[i] > value)
                {
                    continue;
                }

                if (containers[i] == value)
                {
                    count += 1;
                }
                else
                {
                    count += this.Combinations(containers, value - containers[i], i + 1, cache);
                }
            }

            cache.Add(key, count);

            return count;
        }

        private Tuple<int, int> CombinationsWithMin(IList<int> containers, int value, int minIndex, Dictionary<Tuple<int, int>, Tuple<int, int>> cache)
        {
            var key = new Tuple<int, int>(value, minIndex);
            if (cache.TryGetValue(key, out var ret))
            {
                return ret;
            }

            var count = 0;
            var index = minIndex;
            for (; index < containers.Count; ++index)
            {
                if (containers[index] > value)
                {
                    continue;
                }

                break;
            }

            for (; index < containers.Count; ++index)
            {
                if (containers[index] == value)
                {
                    count += 1;
                    continue;
                }

                break;
            }

            ret = new Tuple<int, int>(1, count);

            if (count == 0)
            {
                ret = new Tuple<int, int>(int.MaxValue - 1, 0);
                for (; index < containers.Count; ++index)
                {
                    var temp = this.CombinationsWithMin(containers, value - containers[index], index + 1, cache);

                    if (temp.Item1 == ret.Item1)
                    {
                        ret = new Tuple<int, int>(ret.Item1, ret.Item2 + temp.Item2);
                    }

                    if (temp.Item1 < ret.Item1)
                    {
                        ret = temp;
                    }
                }

                ret = new Tuple<int, int>(ret.Item1 + 1, ret.Item2);
            }

            cache.Add(key, ret);

            return ret;
        }
    }
}
