using AdventOfCode.Solutions.Common;
using System;
using System.Collections.Generic;

namespace AdventOfCode.Solutions.Event2016.Day19
{
    public class Day19 : IAdventOfCodeDayRunner
    {
        public bool HaveVisualization() => false;

        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            if (!Int32.TryParse(input, out var inputInt))
            {
                yield return "Wrong input!";
                yield break;
            }

            var fullLaps = Math.Floor(Math.Log(inputInt, 2));
            var nextFullLapsCount = (int)Math.Pow(2, fullLaps + 1) - 1;
            var winner = nextFullLapsCount - (nextFullLapsCount - inputInt) * 2;

            yield return winner.ToString() + " Elf gets all the presents";
        }

        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            if (!Int32.TryParse(input, out var inputInt))
            {
                yield return "Wrong input!";
                yield break;
            }

            var fullLaps = Math.Floor(Math.Log(inputInt - 1, 3));

            var lowerBound = (int)Math.Pow(3, fullLaps);
            var midBound = 2 * lowerBound;
            var winner = 0;
            if (inputInt <= midBound)
            {
                winner = inputInt - lowerBound;
            }
            else 
            {
                winner = midBound + (inputInt - midBound) * 2;
            }

            yield return winner.ToString() + " Elf gets all the presents";
        }
    }
}
