namespace AdventOfCode.Solutions.Event2015.Day2
{
    using System;
    using System.Collections.Generic;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic of the Day 1.
    /// </summary>
    public class Day2 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HaveVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            yield return 0.ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            yield return 0.ToString();
        }
    }
}
