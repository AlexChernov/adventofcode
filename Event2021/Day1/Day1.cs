namespace AdventOfCode.Solutions.Event2021.Day1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 1 of event.
    /// </summary>
    public class Day1 : IAdventOfCodeDayRunner
    {

        /// <inheritdoc/>
        public bool HasVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Select(v => int.Parse(v));

            int prev = -1;
            int count = 0;

            foreach (var value in values)
            {
                if (value > prev && prev != -1)
                {
                    count++;
                }

                prev = value;
            }

            yield return count.ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Select(v => int.Parse(v)).ToArray();

            int window = 3;
            int count = 0;
            for (int i = window; i < values.Length; ++i)
            {
                if (values[i] > values[i - window])
                {
                    count++;
                }
            }

            yield return count.ToString();
        }
    }
}
