namespace AdventOfCode.Solutions.Event2021.Day6
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 6 of event.
    /// </summary>
    public class Day6 : IAdventOfCodeDayRunner
    {

        /// <inheritdoc/>
        public bool HasVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { ',', }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            var state = new int[9];

            foreach (var v in values)
            {
                state[v] += 1;
            }

            for (var i = 0; i < 80; i++)
            {
                var next = state[0];
                for (int j = 0; j < 8; j++)
                {
                    state[j] = state[j + 1];
                }
                state[6] += next;
                state[8] = next;
            }

            var sum = state.Sum(x => x);

            yield return sum.ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { ',', }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            var state = new long[9];

            foreach (var v in values)
            {
                state[v] += 1;
            }

            for (var i = 0; i < 256; i++)
            {
                var next = state[0];
                for (int j = 0; j < 8; j++)
                {
                    state[j] = state[j + 1];
                }
                state[6] += next;
                state[8] = next;
            }

            long sum = state.Sum(x => x);

            yield return sum.ToString();
        }
    }
}
