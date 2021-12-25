namespace AdventOfCode.Solutions.Event2021.Day9
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 8 of event.
    /// </summary>
    public class Day9 : IAdventOfCodeDayRunner
    {

        /// <inheritdoc/>
        public bool HasVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            int length = values.Length;
            int len2 = values[0].Length;
            var map = new int[length, len2];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < len2; j++)
                {
                    map[i, j] = int.Parse(values[i][j].ToString());
                }
            }

            var sum = 0;

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < len2; j++)
                {
                    var lowest = true;
                    lowest &= i == 0 || map[i - 1, j] > map[i, j];
                    lowest &= j == 0 || map[i, j - 1] > map[i, j];
                    lowest &= i == length - 1 || map[i + 1, j] > map[i, j];
                    lowest &= j == len2 - 1 || map[i, j + 1] > map[i, j];

                    if (lowest)
                    {
                        sum += 1 + (map[i, j]);
                    }
                }
            }

            yield return sum.ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            int length = values.Length;
            int len2 = values[0].Length;
            var map = new int[length, len2];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < len2; j++)
                {
                    map[i, j] = int.Parse(values[i][j].ToString());
                }
            }

            var basinSizes = new List<int>();

            var closed = new HashSet<(int i, int j)>();

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < len2; j++)
                {
                    var curSize = closed.Count;
                    if (closed.Contains((i, j)) || map[i, j] == 9)
                    {
                        continue;
                    }

                    var open = new HashSet<(int i, int j)>();
                    open.Add((i, j));
                    while (open.Count != 0)
                    {
                        var nextOpen = new HashSet<(int i, int j)>();
                        foreach (var item in open)
                        {
                            foreach (var child in GenerateChildren(item, length, len2, map, open, closed))
                            {
                                nextOpen.Add(child);
                            }
                            closed.Add(item);
                        }
                        open = nextOpen;
                    }
                    basinSizes.Add(closed.Count - curSize);
                }
            }

            var ret = basinSizes.OrderBy(x => -x).Take(3).Aggregate(1, (mult, next) => mult*next);
            yield return ret.ToString();
        }

        private IEnumerable<(int, int)> GenerateChildren((int i, int j) item, int len1, int len2, int[,] map, HashSet<(int i, int j)> open, HashSet<(int i, int j)> closed)
        {
            var (i, j) = item;
            (int i, int j)? c1;
            if (i != 0 && (c1 = CheckCandidate(i - 1, j, map, open, closed)) != null) yield return c1.Value;
            if (j != 0 && (c1 = CheckCandidate(i, j - 1, map, open, closed)) != null) yield return c1.Value;
            if (i != len1 - 1 && (c1 = CheckCandidate(i + 1, j, map, open, closed)) != null) yield return c1.Value;
            if (j != len2 - 1 && (c1 = CheckCandidate(i, j + 1, map, open, closed)) != null) yield return c1.Value;
        }

        private (int i, int j)? CheckCandidate(int i, int j, int[,] map, HashSet<(int i, int j)> open, HashSet<(int i, int j)> closed)
        {
            var c = (i, j);
            if (map[i, j] != 9 && !open.Contains(c) && !closed.Contains(c))
            {
                return c;
            }

            return null;
        }
    }
}
