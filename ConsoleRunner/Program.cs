namespace ConsoleRunerCore
{
    using System;
    using System.Threading;
    using AdventOfCode.Solutions.Event2020.Day10;
    using AdventOfCode.Solutions.Event2020.Day11;
    using AdventOfCode.Solutions.Event2020.Day12;
    using AdventOfCode.Solutions.Event2020.Day13;
    using AdventOfCode.Solutions.Event2020.Day8;
    using AdventOfCode.Solutions.Event2020.Day9;

    /// <summary>
    /// The program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The entry point of the program.
        /// </summary>
        private static void Main()
        {
            var text = System.IO.File.ReadAllText(@".\input.txt");
            /*
             * var lines = text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
             *
             * var t = FindPathAStar(map, locations['0'], locations['1']);
             *
             * var skip = 0;
             * var skipNumber = 50;
             */

            foreach (var state in new Day13().RunTask2(text, true))
            {
                // if (skip < skipNumber)
                // {
                //     ++skip;
                //     continue;
                // }
                // skip = 0;
                //Console.SetCursorPosition(0, 0);
                Console.WriteLine(state);
                Thread.Sleep(20);
            }
        }
    }
}
