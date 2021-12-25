namespace AdventOfCode.Solutions.Event2021
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 13 of event.
    /// </summary>
    public class Day13 : IAdventOfCodeDayRunner
    {

        /// <inheritdoc/>
        public bool HasVisualization() => true;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var folds = new List<(string dir, int lineCoor)>();
            var dots = new List<(int x, int y)>();
            var regex = new Regex(@"fold along (?<dir>\S)=(?<line>\d+)");
            foreach (var line in values)
            {
                var match = regex.Match(line);
                if (match.Success)
                {
                    var dir = match.Groups["dir"].Value;
                    var l = int.Parse(match.Groups["line"].Value);
                    folds.Add((dir, l));
                }
                else
                {
                    var numbers = line.Split(new char[] { ',' });
                    dots.Add((x: int.Parse(numbers[0]), y: int.Parse(numbers[1])));
                }
            }

            var fold = folds[0];
            var afterFold = new HashSet<(int, int)>();
            foreach (var dot in dots)
            {
                var nextDot = dot;
                if (fold.dir == "x" && dot.x > fold.lineCoor)
                {
                    nextDot.x = 2 * fold.lineCoor - dot.x;
                }
                if (fold.dir == "y" && dot.y > fold.lineCoor)
                {
                    nextDot.y = 2 * fold.lineCoor - dot.y;
                }
                afterFold.Add(nextDot);
            }

            yield return afterFold.Count.ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var folds = new List<(string dir, int lineCoor)>();
            var dots = new HashSet<(int x, int y)>();
            var regex = new Regex(@"fold along (?<dir>\S)=(?<line>\d+)");
            foreach (var line in values)
            {
                var match = regex.Match(line);
                if (match.Success)
                {
                    var dir = match.Groups["dir"].Value;
                    var l = int.Parse(match.Groups["line"].Value);
                    folds.Add((dir, l));
                }
                else
                {
                    var numbers = line.Split(new char[] { ',' });
                    dots.Add((x: int.Parse(numbers[0]), y: int.Parse(numbers[1])));
                }
            }

            foreach (var fold in folds)
            {
                var afterFold = new HashSet<(int, int)>();
                foreach (var dot in dots)
                {
                    var nextDot = dot;
                    if (fold.dir == "x" && dot.x > fold.lineCoor)
                    {
                        nextDot.x = 2 * fold.lineCoor - dot.x;
                    }
                    if (fold.dir == "y" && dot.y > fold.lineCoor)
                    {
                        nextDot.y = 2 * fold.lineCoor - dot.y;
                    }
                    afterFold.Add(nextDot);
                }
                dots = afterFold;
            }

            int maxX  = 0, maxY = 0;
            foreach (var dot in dots)
            {
                if (dot.x > maxX) maxX = dot.x;
                if (dot.y > maxY) maxY = dot.y;
            }

            var map = new char[maxX + 1, maxY + 1];
            foreach (var dot in dots)
            {
                map[dot.x, dot.y] = '#';
            }

            var str = new StringBuilder();
            for (var i = 0; i <= maxY; i++)
            {
                for (var j = 0; j <= maxX; j++)
                {
                    str.Append(map[j, i] == '#' ? '#' : '.');
                }
                str.AppendLine();
            }

            yield return str.ToString();
        }
    }
}
