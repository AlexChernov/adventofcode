namespace AdventOfCode.Solutions.Event2015.Day1
{
    using System;
    using System.Collections.Generic;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic of the Day 1.
    /// </summary>
    public class Day1 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HasVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var floor = 0;
            foreach (var instruction in input)
            {
                if (instruction == '(')
                {
                    ++floor;
                }
                else
                {
                    --floor;
                }
            }

            yield return floor.ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var floor = 0;
            var index = 0;
            for (index = 0; index < input.Length; ++index)
            {
                var instruction = input[index];
                if (instruction == '(')
                {
                    ++floor;
                }
                else
                {
                    --floor;
                }

                if (floor == -1)
                {
                    break;
                }
            }

            yield return (index + 1).ToString();
        }
    }
}
