namespace AdventOfCode.Solutions.Event2015.Day10
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic of the Day 10.
    /// </summary>
    public class Day10 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HasVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var next = input;
            for (int i = 0; i < 40; ++i)
            {
                next = this.LookAndSay(next);
            }

            yield return next.Length.ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var next = input;
            for (int i = 0; i < 50; ++i)
            {
                next = this.LookAndSay(next);
            }

            yield return next.Length.ToString();
        }

        private string LookAndSay(string input)
        {
            var lastCh = input[0];
            var lastSeqIndex = 0;
            var next = new StringBuilder();
            for (int i = 1; i < input.Length; ++i)
            {
                if (input[i] != lastCh)
                {
                    next.Append(i - lastSeqIndex);
                    next.Append(lastCh);
                    lastCh = input[i];
                    lastSeqIndex = i;
                }
            }

            next.Append(input.Length - lastSeqIndex);
            next.Append(lastCh);

            return next.ToString();
        }
    }
}
