using System;

namespace AdventOfCode.Solutions.Task2016_19
{
    public class Task2016_19
    {
        public static string Run1(string input)
        {
            if (!Int32.TryParse(input, out var inputInt))
            {
                return "Wrong input!";
            }
            var fullLaps = Math.Floor(Math.Log(inputInt, 2));
            var nextFullLapsCount = (int)Math.Pow(2, fullLaps + 1) - 1;
            var winner = nextFullLapsCount - (nextFullLapsCount - inputInt) * 2;

            return winner.ToString() + " Elf gets all the presents";
        }

        public static string Run2(string input)
        {
            if (!Int32.TryParse(input, out var inputInt))
            {
                return "Wrong input!";
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

            return winner.ToString() + " Elf gets all the presents";
        }
    }
}
