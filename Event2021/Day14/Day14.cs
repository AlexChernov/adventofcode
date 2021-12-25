namespace AdventOfCode.Solutions.Event2021
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 14 of event.
    /// </summary>
    public class Day14 : IAdventOfCodeDayRunner
    {

        /// <inheritdoc/>
        public bool HasVisualization() => true;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var start = values.First();

            var regex = new Regex(@"(?<pair>\S\S) -> (?<letter>\S)");
            var genMap = new Dictionary<string, (string, string)>();
            foreach (var value in values.Skip(1))
            {
                var match = regex.Match(value);
                if (match.Success)
                {
                    var pair = match.Groups["pair"].Value;
                    var letter = match.Groups["letter"].Value[0];
                    genMap[pair] = (new string(new char[] { pair[0], letter }), new string(new char[] { letter, pair[1] }));
                }
                else
                {
                    throw new Exception();
                }
            }

            var pairs = new Dictionary<string, int>();
            for (int i = 1; i < start.Length; i++)
            {
                var pair = new string(new char[] { start[i - 1], start[i] });
                pairs[pair] = pairs.GetValueOrDefault(pair) + 1;
            }

            for (int i = 0; i < 10; i++)
            {
                var nextPairs = new Dictionary<string, int>();
                foreach (var kvp in pairs)
                {
                    var pair = kvp.Key;
                    var number = kvp.Value;

                    if (genMap.TryGetValue(pair, out var gen))
                    {
                        nextPairs[gen.Item1] = nextPairs.GetValueOrDefault(gen.Item1) + number;
                        nextPairs[gen.Item2] = nextPairs.GetValueOrDefault(gen.Item2) + number;
                    }
                    else
                    {
                        nextPairs[pair] = nextPairs.GetValueOrDefault(gen.Item1) + number;
                    }
                }
                pairs = nextPairs;
            }

            var counts = new Dictionary<char, int>();
            foreach (var kvp in pairs)
            {
                var pair = kvp.Key;
                var number = kvp.Value;

                counts[pair[0]] = counts.GetValueOrDefault(pair[0]) + number;
                counts[pair[1]] = counts.GetValueOrDefault(pair[1]) + number;
            }

            var max = counts.Values.Max();
            var min = counts.Values.Min();

            yield return ((max - min) / 2).ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var start = values.First();

            var regex = new Regex(@"(?<pair>\S\S) -> (?<letter>\S)");
            var genMap = new Dictionary<string, (string, string)>();
            foreach (var value in values.Skip(1))
            {
                var match = regex.Match(value);
                if (match.Success)
                {
                    var pair = match.Groups["pair"].Value;
                    var letter = match.Groups["letter"].Value[0];
                    genMap[pair] = (new string(new char[] { pair[0], letter }), new string(new char[] { letter, pair[1] }));
                }
                else
                {
                    throw new Exception();
                }
            }

            var pairs = new Dictionary<string, long>();
            for (int i = 1; i < start.Length; i++)
            {
                var pair = new string(new char[] { start[i - 1], start[i] });
                pairs[pair] = pairs.GetValueOrDefault(pair) + 1;
            }

            for (int i = 0; i < 40; i++)
            {
                var nextPairs = new Dictionary<string, long>();
                foreach (var kvp in pairs)
                {
                    var pair = kvp.Key;
                    var number = kvp.Value;

                    if (genMap.TryGetValue(pair, out var gen))
                    {
                        nextPairs[gen.Item1] = nextPairs.GetValueOrDefault(gen.Item1) + number;
                        nextPairs[gen.Item2] = nextPairs.GetValueOrDefault(gen.Item2) + number;
                    }
                    else
                    {
                        nextPairs[pair] = nextPairs.GetValueOrDefault(gen.Item1) + number;
                    }
                }
                pairs = nextPairs;
            }

            var counts = new Dictionary<char, long>();
            foreach (var kvp in pairs)
            {
                var pair = kvp.Key;
                var number = kvp.Value;

                counts[pair[0]] = counts.GetValueOrDefault(pair[0]) + number;
                counts[pair[1]] = counts.GetValueOrDefault(pair[1]) + number;
            }

            var max = counts.Values.Max();
            var min = counts.Values.Min();

            yield return ((max - min) / 2).ToString();
        }
    }
}
