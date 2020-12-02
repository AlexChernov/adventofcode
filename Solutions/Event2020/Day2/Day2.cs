namespace AdventOfCode.Solutions.Event2020.Day2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 19 of event.
    /// </summary>
    public class Day2 : IAdventOfCodeDayRunner
    {

        /// <inheritdoc/>
        public bool HasVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Select(v => int.Parse(v)).OrderBy(v => v).ToArray();

            if (FindPair(values, 2020, out var mult))
            {
                yield return mult.ToString();
            }
        }

        private bool FindPair(int[] values, int target, out int mult)
        {
            int highIndex = values.Length - 1;
            for (var lowIndex = 0; lowIndex < values.Length; ++lowIndex)
            {
                var low = values[lowIndex];
                while (highIndex > 0 && values[highIndex] + low > target)
                {
                    --highIndex;
                }

                if (values[highIndex] + low == target)
                {
                    mult = values[highIndex] * low;
                    return true;
                }
            }

            mult = 0;
            return false;
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Select(v => int.Parse(v)).OrderBy(v => v).ToArray();

            foreach (var first in values)
            {
                if (FindPair(values, 2020-first, out var mult))
                {
                    yield return (mult * first).ToString();
                }
            }
        }
    }
}
