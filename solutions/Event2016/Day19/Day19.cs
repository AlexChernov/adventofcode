namespace AdventOfCode.Solutions.Event2016.Day19
{
    using System;
    using System.Collections.Generic;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 19 of event.
    /// </summary>
    public class Day19 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HaveVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            if (!int.TryParse(input, out var inputInt))
            {
                yield return "Wrong input!";
                yield break;
            }

            var fullLaps = Math.Floor(Math.Log(inputInt, 2));
            var nextFullLapsCount = (int)Math.Pow(2, fullLaps + 1) - 1;
            var winner = nextFullLapsCount - ((nextFullLapsCount - inputInt) * 2);

            yield return winner.ToString() + " Elf gets all the presents";
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            if (!int.TryParse(input, out var inputInt))
            {
                yield return "Wrong input!";
                yield break;
            }

            var fullLaps = Math.Floor(Math.Log(inputInt - 1, 3));

            var lowerBound = (int)Math.Pow(3, fullLaps);
            var midBound = 2 * lowerBound;
            int winner;
            if (inputInt <= midBound)
            {
                winner = inputInt - lowerBound;
            }
            else
            {
                winner = midBound + ((inputInt - midBound) * 2);
            }

            yield return winner.ToString() + " Elf gets all the presents";
        }
    }
}
