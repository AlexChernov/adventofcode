namespace AdventOfCode.Solutions.Event2015.Day8
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode.Solutions.Common;

    public class Day8 : IAdventOfCodeDayRunner
    {
        public bool HaveVisualization() => false;

        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            var count = 0;
            foreach (var line in lines)
            {
                count += 2;
                for (int i = 0; i < line.Length; ++i)
                {
                    if (line[i] == '\\')
                    {
                        if (line[i + 1] == 'x')
                        {
                            count += 3;
                            i += 3;
                        }
                        else
                        {
                            ++count;
                            ++i;
                        }
                    }
                }
            }

            yield return count.ToString();
        }

        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            var count = 0;
            foreach (var line in lines)
            {
                count += 2 + line.Count(ch => ch == '\\' || ch == '"');
            }

            yield return count.ToString();
        }
    }
}
