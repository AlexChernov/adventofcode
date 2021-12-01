namespace AdventOfCode.Solutions.Event2020.Day6
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 19 of event.
    /// </summary>
    public class Day6 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HasVisualization() => false;

       /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var normalize = input.Replace("\n\n", "!");
            normalize = normalize.Replace("\n", "");
            var values = normalize.Split(new char[] { '!' }, StringSplitOptions.RemoveEmptyEntries);

            var sum = values.Sum(v => v.ToHashSet().Count);

            yield return sum.ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var normalize = input.Replace("\n\n", "!");
            var values = normalize.Split(new char[] { '!' }, StringSplitOptions.RemoveEmptyEntries);

            var sum = values.Sum(v => Everyone(v));

            yield return sum.ToString();
        }

        private decimal Everyone(string input)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            var chCount = new Dictionary<char, int>();
            var count = 0;
            foreach (var v in values)
            {
                foreach (var ch in v)
                {
                    if (!chCount.ContainsKey(ch))
                    {
                        chCount.Add(ch, 0);
                    }

                    chCount[ch] += 1;
                }

                ++count;
            }

            return chCount.Count(kvp => kvp.Value == count);
        }
    }
}
