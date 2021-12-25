namespace AdventOfCode.Solutions.Event2021.Day3
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 1 of event.
    /// </summary>
    public class Day3 : IAdventOfCodeDayRunner
    {

        /// <inheritdoc/>
        public bool HasVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var len = values[0].Length;
            int[] counters = new int[len];

            foreach (var v in values)
            {
                for (int i = 0; i < len; ++i)
                {
                    counters[i] += v[i] == '1' ? 1 : -1;
                }
            }

            int m = 1;
            int gamma = 0;
            int epsilon = 0;
            for (int i = 0; i < len; ++i)
            {
                gamma += counters[len - 1 - i] > 0 ? m : 0;
                epsilon += counters[len - 1 - i] > 0 ? 0 : m;
                m *= 2;
            }

            yield return (gamma * epsilon).ToString();
        }

        class Node
        {
            public Node Zero { get; private set; }
            public Node One { get; private set; }
            public int Count { get; set; } = 0;

            public Node SetNext(char ch)
            {
                Node next = null;

                if (ch == '1')
                {
                    if (One is null)
                    {
                        One = new Node();
                    }
                    next = One;
                }
                else
                {
                    if (Zero is null)
                    {
                        Zero = new Node();
                    }
                    next = Zero;
                }

                next.Count++;
                return next;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var len = values[0].Length;
            int[] counters = new int[len];

            var root = new Node();
            Node next = null;
            foreach (var v in values)
            {
                next = root;
                for (int i = 0; i < len; ++i)
                {
                    next = next.SetNext(v[i]);
                }
            }

            int oxygen = 0;

            next = root;
            while (next.One != null || next.Zero != null)
            {
                var v = 0;
                if ((next.One?.Count ?? 0) >= (next.Zero?.Count ?? 0))
                {
                    next = next.One;
                    v = 1;
                }
                else
                {
                    next = next.Zero;
                }
                oxygen = oxygen * 2 + v;
            }

            int co2 = 0;
            next = root;
            while (next.One != null || next.Zero != null)
            {
                var v = 0;
                if (next.Zero == null || (((next.One?.Count ?? 0) < (next.Zero?.Count ?? 0)) && next.One != null))
                {
                    next = next.One;
                    v = 1;
                }
                else
                {
                    next = next.Zero;
                }
                co2 = co2 * 2 + v;
            }



            yield return (oxygen * co2).ToString();
        }
    }
}
