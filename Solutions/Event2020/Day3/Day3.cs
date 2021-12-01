namespace AdventOfCode.Solutions.Event2020.Day3
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 19 of event.
    /// </summary>
    public class Day3 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HasVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            long count = Run(values, 3, 1);

            yield return count.ToString();
        }

        private static long Run(string[] values, int right, int down)
        {
            var len = values.First().Length;

            var count = 0;
            var index = 0;
            for (var i = 0; i < values.Length; i += down)
            {
                if (values[i][index] == '#')
                {
                    ++count;
                }

                index = Common.Utils.Mod(index + right, len);
            }

            return count;
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            var count = new[]
            {
                (1,1),
                (3,1),
                (5,1),
                (7,1),
                (1,2),
            }
            .Aggregate(1L, (mult, nextPair) =>
            {
                var r = Run(values, nextPair.Item1, nextPair.Item2);
                return mult * r;
            });

            yield return count.ToString();
        }
    }
}
