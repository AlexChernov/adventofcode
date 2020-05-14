namespace AdventOfCode.Solutions.Event2015.Day12
{
    using System.Collections.Generic;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic of the Day 12.
    /// </summary>
    public class Day12 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HaveVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var value = string.Empty;

            var sum = 0;

            foreach (var ch in input)
            {
                if (char.IsDigit(ch) || (string.IsNullOrEmpty(value) && ch == '-'))
                {
                    value += ch;
                }
                else
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        sum += int.Parse(value);
                        value = string.Empty;
                    }
                }
            }

            if (!string.IsNullOrEmpty(value))
            {
                sum += int.Parse(value);
            }

            yield return sum.ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var value = string.Empty;

            var nestedContext = new Stack<Context>();
            var currentContext = new Context()
            {
                Sum = 0,
                IsRed = false,
                IsArray = false,
            };
            var wordFinder = new WordFinder("red");

            foreach (var ch in input)
            {
                if (char.IsDigit(ch) || (string.IsNullOrEmpty(value) && ch == '-'))
                {
                    value += ch;
                }
                else
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        currentContext.Sum += int.Parse(value);
                        value = string.Empty;
                    }
                }

                if (ch == '[')
                {
                    nestedContext.Push(currentContext);
                    currentContext = new Context()
                    {
                        Sum = 0,
                        IsArray = true,
                        IsRed = false,
                    };
                }

                if (ch == '{')
                {
                    nestedContext.Push(currentContext);
                    currentContext = new Context()
                    {
                        Sum = 0,
                        IsArray = false,
                        IsRed = false,
                    };
                }

                if (ch == '}' || ch == ']')
                {
                    var sum = (currentContext.IsRed && !currentContext.IsArray) ? 0 : currentContext.Sum;
                    currentContext = nestedContext.Pop();
                    currentContext.Sum += sum;
                }

                if (wordFinder.NextChar(ch))
                {
                    currentContext.IsRed = true;
                }
            }

            yield return currentContext.Sum.ToString();
        }
    }
}
