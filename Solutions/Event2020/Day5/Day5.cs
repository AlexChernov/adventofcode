namespace AdventOfCode.Solutions.Event2020.Day5
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 19 of event.
    /// </summary>
    public class Day5 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HasVisualization() => false;

       /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            var max = values.Select(v => v.Replace("F", "0").Replace("B", "1").Replace("R", "1").Replace("L", "0")).Max(v => Convert.ToInt32(v, 2));

            yield return max.ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            var valuesInt = values.Select(v => v.Replace("F", "0").Replace("B", "1").Replace("R", "1").Replace("L", "0")).Select(v => Convert.ToInt32(v, 2)).OrderBy(v => v).ToArray();

            var current = valuesInt[0];

            for (var i = 1; i < valuesInt.Length; ++i)
            {
                ++current;
                if (current != valuesInt[i])
                {
                    break;
                }
            }

            yield return current.ToString();
        }

    }
}
