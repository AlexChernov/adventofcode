namespace AdventOfCode.Solutions.Event2021
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 20 of event.
    /// </summary>
    public class Day20 : IAdventOfCodeDayRunner
    {

        /// <inheritdoc/>
        public bool HasVisualization() => true;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            var generator = values[0];
            var len = values.Length - 1;
            var len2 = values[1].Length;
            var map = new bool[len, len2];
            for (var i = 0; i < len; i++)
            {
                for (var j = 0; j < len2; j++)
                {
                    map[i, j] = values[i + 1][j] == '#';
                }
            }

            var toogleMode = false;
            if (generator[0] == '#')
            {
                if (generator[generator.Length - 1] != '#')
                {
                    toogleMode = true;
                }
                else
                {
                    throw new Exception();
                }
            }

            int defaultValue = 0;
            map = NextMap(map, generator, toogleMode, ref defaultValue);
            map = NextMap(map, generator, toogleMode, ref defaultValue);

            var count = 0;

            foreach (var item in map)
            {
                count += item ? 1 : 0;
            }

            yield return count.ToString();
        }

        private bool[,] NextMap(bool[,] map, string generator, bool toogleMode, ref int defaultValue)
        {
            var len = map.GetLength(0) + 2;
            var len2 = map.GetLength(1) + 2;
            var nextMap = new bool[len, len2];
            for (var i = 0; i < len; i++)
            {
                for (var j = 0; j < len2; j++)
                {
                    nextMap[i, j] = Generate(map, i, j, generator, defaultValue);
                }
            }

            if (toogleMode)
            {
                defaultValue = defaultValue == 0 ? 1 : 0;
            }
            return nextMap;
        }

        private bool Generate(bool[,] map, int i, int j, string generator, int defaultValue)
        {
            var mapi = i - 1;
            var mapj = j - 1;

            var index = 0;

            for (var x = mapi - 1; x <= mapi + 1; x++)
            {
                for (int y = mapj - 1; y <= mapj + 1; y++)
                {
                    var value = map.InBound(x, y) ? map[x, y] ? 1 : 0 : defaultValue;
                    index = index * 2 + value;
                }
            }

            var result = generator[index] == '#';
            return result;
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            var generator = values[0];
            var len = values.Length - 1;
            var len2 = values[1].Length;
            var map = new bool[len, len2];
            for (var i = 0; i < len; i++)
            {
                for (var j = 0; j < len2; j++)
                {
                    map[i, j] = values[i + 1][j] == '#';
                }
            }

            var toogleMode = false;
            if (generator[0] == '#')
            {
                if (generator[generator.Length - 1] != '#')
                {
                    toogleMode = true;
                }
                else
                {
                    throw new Exception();
                }
            }

            int defaultValue = 0;

            for (var i =0; i < 50; ++i)
            {
                map = NextMap(map, generator, toogleMode, ref defaultValue);
            }

            var count = 0;

            foreach (var item in map)
            {
                count += item ? 1 : 0;
            }

            yield return count.ToString();
        }

    }
}
