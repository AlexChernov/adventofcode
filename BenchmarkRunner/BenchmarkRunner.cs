namespace BenchmarkRunner
{
    using System.Linq;
    using AdventOfCode.Solutions.Event2016.Day24;
    using BenchmarkDotNet.Attributes;

    public class BenchmarkRunner
    {
        private readonly string text;

        public BenchmarkRunner()
        {
            this.text = System.IO.File.ReadAllText(@".\input.txt");
        }

        [Benchmark]
        public object DirectAStar()
        {
            return new Day24().RunTask1(this.text, false).First();
        }

        [Benchmark]
        public object BFSSImplifyPlusTSP()
        {
            return new Day24().RunTask2(this.text, false).First();
        }
    }
}
