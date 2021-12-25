namespace AdventOfCode.Solutions.Event2021.Day12
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 12 of event.
    /// </summary>
    public class Day12 : IAdventOfCodeDayRunner
    {

        /// <inheritdoc/>
        public bool HasVisualization() => true;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var map = Init(values);

            var count = RouteCount(map, "start", new Dictionary<string, int>(), false);

            yield return count.ToString();
        }

        private Dictionary<string, List<string>> Init(string[] values)
        {
            var map = new Dictionary<string, List<string>>();

            foreach (var v in values.Select(x => x.Split(new char[] { '-' })))
            {
                UpdateMap(map, v[0], v[1]);
            }


            return map;
        }

        private int RouteCount(Dictionary<string, List<string>> map, string cave, Dictionary<string, int> visitedSmall, bool extraLife)
        {
            if (cave == "end")
            {
                return 1;
            }

            if (cave != cave.ToUpperInvariant())
            {
                visitedSmall[cave] = visitedSmall.GetValueOrDefault(cave) + 1;
            }

            var count = 0;
            foreach (var nextCave in map[cave])
            {
                var nextExtraLife = extraLife;
                if (visitedSmall.GetValueOrDefault(nextCave) > 0)
                {
                    if (!nextExtraLife || nextCave == "start")
                    {
                        continue;
                    }
                    nextExtraLife = false;
                }
                count += RouteCount(map, nextCave, visitedSmall, nextExtraLife);
            }

            visitedSmall[cave] = visitedSmall.GetValueOrDefault(cave) - 1;

            return count;
        }

        private void UpdateMap(Dictionary<string, List<string>> map, string v1, string v2)
        {
            if (!map.ContainsKey(v1))
            {
                map.Add(v1, new List<string>());
            }
            if (!map.ContainsKey(v2))
            {
                map.Add(v2, new List<string>());
            }

            map[v1].Add(v2);
            map[v2].Add(v1);
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            var map = Init(values);

            var count = RouteCount(map, "start", new Dictionary<string, int>(), true);

            yield return count.ToString();
        }
    }
}
