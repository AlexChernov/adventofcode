namespace AdventOfCode.Solutions.Event2021.Day10
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 8 of event.
    /// </summary>
    public class Day10 : IAdventOfCodeDayRunner
    {

        /// <inheritdoc/>
        public bool HasVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var sum = 0;

            foreach (var line in values)
            {
                sum += GetScore(line);
            }

            yield return sum.ToString();
        }

        private int GetScore(string line)
        {
            var stack = new Stack<char>();
            foreach (var ch in line)
            {
                if (IsOpen(ch))
                {
                    stack.Push(ch);
                    continue;
                }
                var lastOpen = stack.Pop();
                if (!Match(lastOpen, ch))
                {
                    return GetScore(ch);
                }
            }
            return 0;
        }

        private long GetScore2(string line)
        {
            var stack = new Stack<char>();
            foreach (var ch in line)
            {
                if (IsOpen(ch))
                {
                    stack.Push(ch);
                    continue;
                }
                var lastOpen = stack.Pop();
                if (!Match(lastOpen, ch))
                {
                    return 0;
                }
            }

            long score = 0;
            while (stack.Count > 0)
            {
                score *= 5;
                score += GetChScore2(stack.Pop());
            }
            return score;
        }

        private int GetChScore2(char ch)
        {
            switch (ch)
            {
                case '(': return 1;
                case '[': return 2;
                case '{': return 3;
                case '<': return 4;
                default:
                    throw new ArgumentException();
            }
        }

        private int GetScore(char ch)
        {
            switch (ch)
            {
                case ')': return 3;
                case ']': return 57;
                case '}': return 1197;
                case '>': return 25137;
                default: throw new ArgumentException();
            }
        }

        private bool Match(char lastOpen, char ch)
        {
            return lastOpen == '(' && ch == ')' ||
                   lastOpen == '[' && ch == ']' ||
                   lastOpen == '<' && ch == '>' ||
                   lastOpen == '{' && ch == '}';
        }

        private bool IsOpen(char ch)
        {
            return ch == '(' || ch == '[' || ch == '<' || ch == '{';
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(GetScore2).Where(v => v>0).OrderBy(v => v).ToArray();

            var ret = values[values.Length / 2];

            yield return ret.ToString();
        }
    }
}
