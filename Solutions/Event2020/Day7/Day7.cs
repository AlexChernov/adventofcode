namespace AdventOfCode.Solutions.Event2020.Day7
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 19 of event.
    /// </summary>
    public class Day7 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HasVisualization() => false;

        private Dictionary<string, Node> map = new Dictionary<string, Node>();

        private Dictionary<string, int> memo = new Dictionary<string, int>();

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            Init(values);

            var start = map["shiny gold"];
            var result = new HashSet<string>();
            var next = start.Parents;

            while (next.Any())
            {
                var nextNext = new List<Node>();

                foreach (var value in next)
                {
                    if (!result.Contains(value.Color))
                    {
                        result.Add(value.Color);
                    }

                    foreach (var parent in value.Parents)
                    {
                        nextNext.Add(parent);
                    }
                }

                next = nextNext;
            }

            yield return result.Count.ToString();
        }

        private void Init(string[] values)
        {
            var regex = new Regex(@"^(?<pcolor>.+) bags contain((?<nothing> no other bags.)|(?<children> \d+.+))");
            var regexChild = new Regex(@" (?<count>\d+) (?<ccolor>.+) bag[s\.]?");
            foreach (var value in values)
            {
                var match = regex.Match(value);

                var parentColor = match.Groups["pcolor"].Value;

                var parentNode = GetOrCreate(parentColor);

                if (match.Groups["nothing"].Success)
                {
                    continue;
                }

                var children = match.Groups["children"].Value.Split(',');

                foreach (var child in children)
                {
                    var cmatch = regexChild.Match(child);

                    var childColor = cmatch.Groups["ccolor"].Value;
                    var count = int.Parse(cmatch.Groups["count"].Value);

                    var childNode = GetOrCreate(childColor);
                    childNode.Parents.Add(parentNode);
                    parentNode.Children.Add((childNode, count));
                }
            }
        }

        private Node GetOrCreate(string color)
        {
            if (!map.TryGetValue(color, out var node))
            {
                map.Add(color, new Node(color));
                node = map[color];
            }

            return node;
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            Init(values);

            var count = Count("shiny gold");

            yield return count.ToString();
        }

        private int Count(string color)
        {
            if (memo.TryGetValue(color, out var result))
            {
                return result;
            }

            var start = map[color];
            var count = 1;
            foreach (var child in start.Children)
            {
                count += child.count * Count(child.node.Color);
            }

            memo.Add(color, count);
            return count;
        }
    }
}
