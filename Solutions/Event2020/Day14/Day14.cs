namespace AdventOfCode.Solutions.Event2020.Day13
{
    using System;
    using System.Collections.Generic;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 11 of event.
    /// </summary>
    public class Day14 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HasVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var result = 0;

            IReadOnlyCollection<int> a = new[] { 1, 2, 3 };

            foreach (var aa in a)
            {
            }

            yield return result.ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            var count = 0;

            yield return count.ToString();
        }
    }
}
