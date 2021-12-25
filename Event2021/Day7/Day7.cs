namespace AdventOfCode.Solutions.Event2021.Day7
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 7 of event.
    /// </summary>
    public class Day7 : IAdventOfCodeDayRunner
    {

        /// <inheritdoc/>
        public bool HasVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { ',', }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var ordered = values.OrderBy(x => x).ToArray();

            var med = ordered[(ordered.Length / 2)];
            var sum = 0;
            for (var i = 0; i < values.Length; i++)
            {
                sum += Math.Abs(values[i]-med);
            }

            yield return sum.ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { ',', }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var mean = (int) Math.Round((decimal)values.Sum(x => x) / (decimal)values.Length) - 2;

            long sum = 0;
            for (var i = 0; i < values.Length; i++)
            {
                sum += Fib(Math.Abs(mean-values[i]));
            }

            yield return sum.ToString();
        }

        private int Fib(int v)
        {
            var sum = 0;
            for (int i = 0; i <= v;++i)
            {
                sum += i;
            }
            return sum;
        }
    }
}
