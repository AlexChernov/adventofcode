namespace ConsoleRunerCore
{
    using System;
    using System.Threading;
    using AdventOfCode.Solutions.Event2021;

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

            var i = 0;
            foreach (var state in new Day25().RunTask1(text, false))
            {
                // if (skip < skipNumber)
                // {
                //     ++skip;
                //     continue;
                // }
                // skip = 0;
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(state);
                Thread.Sleep(10);
            }
        }
    }
}
