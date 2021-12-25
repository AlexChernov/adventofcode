namespace AdventOfCode.Solutions.Event2021
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 14 of event.
    /// </summary>
    public class Day18 : IAdventOfCodeDayRunner
    {

        /// <inheritdoc/>
        public bool HasVisualization() => true;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            Number left = null;
            foreach (var value in values)
            {
                var number = Number.Parse(value);
                if (left == null)
                {
                    left = number;
                    continue;
                }
                left = new Number(left, number);
                left.Reduce();
            }

            yield return left.Magnitude().ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var max = 0;
            for (var i = 0; i < values.Length; i++)
            {
                for (var j = 0; j < values.Length; ++j)
                {
                    if (i == j) continue;
                    var number = new Number(Number.Parse(values[i]), Number.Parse(values[j]));
                    number.Reduce();
                    var mag = number.Magnitude();
                    if (mag > max) max = mag;
                }
            }

            yield return max.ToString();
        }

    }
}
