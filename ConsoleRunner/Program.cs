using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Solutions.Task2016_20;

namespace AdventOfCode.ConsoleRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = System.IO.File.ReadAllText(@".\input.txt");

            var result = Task2016_20.Run2(text);
            Console.Out.WriteLine(result);
            Console.Read();
        }
    }
}
