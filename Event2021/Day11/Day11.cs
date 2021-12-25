namespace AdventOfCode.Solutions.Event2021.Day11
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 11 of event.
    /// </summary>
    public class Day11 : IAdventOfCodeDayRunner
    {

        /// <inheritdoc/>
        public bool HasVisualization() => true;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            var len = values.Length;
            var len2 = values[0].Length;
            var map = new int[len, len2];

            for (int i = 0; i < len; ++i)
            {
                for (int j = 0; j < len2; ++j)
                    map[i, j] = int.Parse(values[i][j].ToString());
            }

            var sum = 0;
            for (int s = 0; s < 100; ++s)
            {
                sum += Step(map, len, len2);
                var str = new StringBuilder();
                for (int i = 0; i < len; ++i)
                {
                    for (int j = 0; j < len2; ++j)
                        str.Append(map[i, j]);
                    str.AppendLine();
                }
                yield return str.ToString();
            }

            yield return sum.ToString();
        }

        private int Step(int[,] map, int len, int len2)
        {
            var readyToFlash = new HashSet<(int i, int j)>();

            for (int i = 0; i < len; ++i)
            {
                for (var j = 0; j < len2; ++j)
                {
                    if (++map[i, j] > 9)
                    {
                        readyToFlash.Add((i, j));
                    }
                }
            }

            if (readyToFlash.Count == 0)
            {
                return 0;
            }

            var sum = 0;
            var flashed = new HashSet<(int i, int j)>();
            while (readyToFlash.Count != 0)
            {
                sum += readyToFlash.Count;
                flashed = flashed.Concat(readyToFlash).ToHashSet();
                var nextReadyToFlash = new HashSet<(int i, int j)>();
                foreach (var flash in readyToFlash)
                {
                    foreach (var child in Flash(flash, map, len, len2, flashed))
                    {
                        nextReadyToFlash.Add(child);
                    }
                }
                readyToFlash = nextReadyToFlash;
            }
            foreach (var flash in flashed)
            {
                map[flash.i, flash.j] = 0;
            }

            return sum;
        }

        private IEnumerable<(int i, int j)> Flash((int i, int j) item, int[,] map, int len1, int len2, HashSet<(int i, int j)> flashed)
        {
            var (i, j) = item;

            var candidates = new[] {
                (i-1, j-1), (i-1, j), (i-1, j+1),
                (i  , j-1),           (i, j+1),
                (i+1, j-1), (i+1, j), (i+1, j+1),
            };

            foreach (var candidate in candidates)
            {
                var child = CheckCandidate(candidate, map, len1, len2, flashed);
                if (child != null) yield return child.Value;
            }
        }

        private (int i, int j)? CheckCandidate((int i, int j) c, int[,] map, int len1, int len2, HashSet<(int i, int j)> closed)
        {
            var (i, j) = c;

            if (i < 0 || j < 0 || i >= len1 || j >= len2) return null;

            if (++map[i, j] > 9 && !closed.Contains(c))
            {
                return c;
            }

            return null;
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            var len = values.Length;
            var len2 = values[0].Length;
            var map = new int[len, len2];

            for (int i = 0; i < len; ++i)
            {
                for (int j = 0; j < len2; ++j)
                    map[i, j] = int.Parse(values[i][j].ToString());
            }

            var last = 0;
            var step = 0;
            while(last < 100)
            {
                last = Step(map, len, len2);
                ++step;
            }

            yield return step.ToString();
        }
    }
}
