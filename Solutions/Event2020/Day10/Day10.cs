namespace AdventOfCode.Solutions.Event2020.Day10
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.WebSockets;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 9 of event.
    /// </summary>
    public class Day10 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HasVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Append(0).OrderBy(v=>v).ToArray();
            var result1 = 0;
            var result3 = 1;

            for (int i = 1; i < values.Length; ++i)
            {
                if (values[i] - values[i - 1] == 1)
                {
                    ++result1;
                }
                else if (values[i] - values[i - 1] == 3)
                {
                    ++result3;
                }
                else if (values[i] - values[i - 1] == 2)
                {
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }

            yield return (result1*result3).ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input
                .Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .Append(0)
                .OrderBy(v => v)
                .ToArray();
            var paths = values.ToDictionary(v => v, v => 0L);
            paths[0] = 1;

            foreach (var value in values)
            {
                var cur = paths[value];

                var o1 = value + 1;
                if (paths.ContainsKey(o1))
                {
                    paths[o1] += cur;
                }

                var o2 = value + 2;
                if (paths.ContainsKey(o2))
                {
                    paths[o2] += cur;
                }

                var o3 = value + 3;
                if (paths.ContainsKey(o3))
                {
                    paths[o3] += cur;
                }
            }

            yield return paths[values[values.Length-1]].ToString();
        }
    }
}
