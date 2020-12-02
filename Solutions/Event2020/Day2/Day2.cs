namespace AdventOfCode.Solutions.Event2020.Day2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
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
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            var regex = new Regex(@"(?<from>\d+)-(?<to>\d+) (?<target>\w): (?<password>\w+)");

            var count = 0;
            foreach (var value in values)
            {
                var match = regex.Match(value);

                var from = int.Parse(match.Groups["from"].Value);
                var to = int.Parse(match.Groups["to"].Value);
                var target = match.Groups["target"].Value.Single();
                var password = match.Groups["password"].Value;

                var entryCount = 0;

                foreach (var ch in password)
                {
                    if (ch == target)
                    {
                        ++entryCount;
                    }
                }

                if (from <= entryCount && entryCount <= to)
                {
                    ++count;
                }
            }

            yield return count.ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            var regex = new Regex(@"(?<from>\d+)-(?<to>\d+) (?<target>\w): (?<password>\w+)");

            var count = 0;
            foreach (var value in values)
            {
                var match = regex.Match(value);

                var from = int.Parse(match.Groups["from"].Value);
                var to = int.Parse(match.Groups["to"].Value);
                var target = match.Groups["target"].Value.Single();
                var password = match.Groups["password"].Value;

                if ((password[from - 1] == target) ^ (password[to - 1] == target))
                {
                    ++count;
                }
            }

            yield return count.ToString();
        }
    }
}
