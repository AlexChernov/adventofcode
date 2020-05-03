using System.Linq;
using AdventOfCode.Solutions.Event2016.Day24;
using BenchmarkDotNet.Attributes;

namespace BenchmarkRunner
{
    public class BenchmarkRunner
    {
        private readonly string text;

        public BenchmarkRunner()
        {
            text = System.IO.File.ReadAllText(@".\input.txt");
        }

        [Benchmark]
        public object DirectAStar()
        {
            return new Day24().RunTask1(text, false).First();
        }

        [Benchmark]
        public object BFSSImplifyPlusTSP()
        {
            return new Day24().RunTask2(text, false).First();
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkDotNet.Running.BenchmarkRunner.Run<BenchmarkRunner>();
        }
    }
}