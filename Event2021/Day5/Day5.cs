namespace AdventOfCode.Solutions.Event2021.Day5
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 4 of event.
    /// </summary>
    public class Day5 : IAdventOfCodeDayRunner
    {

        /// <inheritdoc/>
        public bool HasVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            var pipes = InitPipes(values);

            var map = new int[1000, 1000];
            var intersect = 0;

            foreach (var pipe in pipes)
            {
                var ((x1, y1), (x2, y2)) = pipe;

                if (x1 == x2)
                {
                    var l = Math.Min(y1, y2);
                    var r = Math.Max(y1, y2);
                    for (var i = l; i <= r; i++)
                    {
                        if (map[x1, i]++ == 1)
                        {
                            intersect++;
                        }
                    }
                }
                if (y1 == y2)
                {
                    var l = Math.Min(x1, x2);
                    var r = Math.Max(x1, x2);
                    for (var i = l; i <= r; ++i)
                    {
                        if (map[i, y1]++ == 1)
                        {
                            intersect++;
                        }
                    }
                }
            }

            yield return intersect.ToString();
        }


        private List<((int x, int y), (int x, int y))> InitPipes(string[] lines)
        {
            var pipesMap = new List<((int x, int y), (int x, int y))>();

            var regex = new Regex(@"(?<x1>\d+),(?<y1>\d+) -> (?<x2>\d+),(?<y2>\d+)");
            foreach (var line in lines)
            {
                var match = regex.Match(line);
                if (match.Success)
                {
                    var x1 = int.Parse(match.Groups["x1"].Value);
                    var y1 = int.Parse(match.Groups["y1"].Value);
                    var x2 = int.Parse(match.Groups["x2"].Value);
                    var y2 = int.Parse(match.Groups["y2"].Value);

                    var pipe = ((x1, y1), (x2, y2));
                    pipesMap.Add(pipe);
                }
            }

            return pipesMap;
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            var pipes = InitPipes(values);

            var map = new int[1000, 1000];
            var intersect = 0;

            foreach (var pipe in pipes)
            {
                var ((x1, y1), (x2, y2)) = pipe;

                if (x1 == x2)
                {
                    var l = Math.Min(y1, y2);
                    var r = Math.Max(y1, y2);
                    for (var i = l; i <= r; i++)
                    {
                        if (map[x1, i]++ == 1)
                        {
                            intersect++;
                        }
                    }
                }
                else if (y1 == y2)
                {
                    var l = Math.Min(x1, x2);
                    var r = Math.Max(x1, x2);
                    for (var i = l; i <= r; ++i)
                    {
                        if (map[i, y1]++ == 1)
                        {
                            intersect++;
                        }
                    }
                }
                else
                {
                    var delta = Math.Abs(x1 - x2);
                    var xd = x2>x1 ? 1 : -1;
                    var yd = y2>y1 ? 1 : -1;
                    for (var i = 0; i <= delta; ++i)
                    {
                        if (map[x1+i*xd, y1+i*yd]++ == 1)
                        {
                            intersect++;
                        }
                    }
                }
            }

            yield return intersect.ToString();
        }
    }
}
