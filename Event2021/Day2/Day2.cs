namespace AdventOfCode.Solutions.Event2021.Day2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 1 of event.
    /// </summary>
    public class Day2 : IAdventOfCodeDayRunner
    {

        /// <inheritdoc/>
        public bool HasVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(v => {
                    var split = v.Split(" ");
                    return (dir: split[0], value: int.Parse(split[1]));
                });

            var depth = 0;
            var hor = 0;

            foreach (var v in values)
            {
                switch (v.dir)
                {
                    case "forward":
                        hor += v.value;
                        break;
                    case "down":
                        depth += v.value;
                        break;
                    case "up":
                        depth -= v.value;
                        break;
                    default:
                        throw new Exception("Unexpected");
                }
            }

            yield return (depth*hor).ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(v => {
                    var split = v.Split(" ");
                    return (dir: split[0], value: int.Parse(split[1]));
                });

            var depth = 0;
            var hor = 0;
            var aim = 0;

            foreach (var v in values)
            {
                switch (v.dir)
                {
                    case "forward":
                        hor += v.value;
                        depth += aim * v.value;
                        break;
                    case "down":
                        aim += v.value;
                        break;
                    case "up":
                        aim -= v.value;
                        break;
                    default:
                        throw new Exception("Unexpected");
                }
            }

            yield return (depth * hor).ToString();
        }
    }
}
