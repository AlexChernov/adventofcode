namespace BenchmarkRunner
{
    using System.Linq;
    using AdventOfCode.Solutions.Event2016.Day24;
    using BenchmarkDotNet.Attributes;

    /// <summary>
    /// Incapsulates benchmark runner logic.
    /// </summary>
    public class BenchmarkRunner
    {
        private readonly string text;

        /// <summary>
        /// Initializes a new instance of the <see cref="BenchmarkRunner"/> class.
        /// </summary>
        public BenchmarkRunner()
        {
            this.text = System.IO.File.ReadAllText(@".\input.txt");
        }

        /// <summary>
        /// Runs A star algorithm.
        /// </summary>
        /// <returns>The result.</returns>
        [Benchmark]
        public object DirectAStar()
        {
            return new Day24().RunTask1(this.text, false).First();
        }

        /// <summary>
        /// Runs BFS+TSP algorithm.
        /// </summary>
        /// <returns>The result.</returns>
        [Benchmark]
        public object BFSSImplifyPlusTSP()
        {
            return new Day24().RunTask2(this.text, false).First();
        }
    }
}
