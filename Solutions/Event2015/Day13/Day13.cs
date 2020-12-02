namespace AdventOfCode.Solutions.Event2015.Day13
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic of the Day 13.
    /// </summary>
    public class Day13 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HasVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            var map = this.InitMap(lines);

            GraphNode path = null;
            var spin = new Spinner();
            var drawing = new Lazy<Drawing>(() => new Drawing(10));

            var skip = 0;
            var skipNumber = 250;

            foreach (var node in this.SolveTSP(map))
            {
                path = node;

                if (!shouldVisualise)
                {
                    continue;
                }

                if (skip < skipNumber)
                {
                    ++skip;
                    continue;
                }

                skip = 0;

                drawing.Value.Update(node);
                spin.Turn();

                var outstr = new StringBuilder("Calculating... ");
                outstr.AppendLine(spin.State);
                outstr.AppendLine(drawing.Value.GetStateStr());

                yield return outstr.ToString();
            }

            if (path != null)
            {
                yield return path.Cost.ToString() + " is the distance of the longest route.";
            }
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            var map = this.InitMap(lines);
            var me = new Dictionary<string, int>();

            foreach (var kvp in map)
            {
                me.Add(kvp.Key, 0);
                kvp.Value.Add("Me", 0);
            }

            map.Add("Me", me);

            GraphNode path = null;
            var spin = new Spinner();
            var drawing = new Lazy<Drawing>(() => new Drawing(10));

            var skip = 0;
            var skipNumber = 250;

            foreach (var node in this.SolveTSP(map))
            {
                path = node;

                if (!shouldVisualise)
                {
                    continue;
                }

                if (skip < skipNumber)
                {
                    ++skip;
                    continue;
                }

                skip = 0;

                drawing.Value.Update(node);
                spin.Turn();

                var outstr = new StringBuilder("Calculating... ");
                outstr.AppendLine(spin.State);
                outstr.AppendLine(drawing.Value.GetStateStr());

                yield return outstr.ToString();
            }

            if (path != null)
            {
                yield return path.Cost.ToString() + " is the distance of the longest route.";
            }
        }

        private Dictionary<string, Dictionary<string, int>> InitMap(string[] lines)
        {
            var happiness = new Dictionary<string, Dictionary<string, int>>();

            var regex = new Regex(@"(?<from>\S+) would ((?<lose>lose)|gain) (?<value>\d+) happiness units by sitting next to (?<to>\S+).");
            foreach (var line in lines)
            {
                var match = regex.Match(line);
                if (match.Success)
                {
                    var from = match.Groups["from"].Value;
                    var to = match.Groups["to"].Value;
                    var value = int.Parse(match.Groups["value"].Value);
                    var lose = match.Groups["lose"].Success;

                    if (!happiness.ContainsKey(from))
                    {
                        happiness.Add(from, new Dictionary<string, int>());
                    }

                    happiness[from].Add(to, lose ? -value : value);
                }
            }

            return happiness;
        }

        private IEnumerable<GraphNode> SolveTSP(Dictionary<string, Dictionary<string, int>> graph)
        {
            var path = new GraphNode
            {
                Cost = 0,
            };

            var first = graph.First().Key;

            var start = new GraphNode
            {
                Cost = 0,
                CurrentPerson = first,
                Parent = null,
            };
            var visited = new HashSet<string>();
            visited.Add(first);

            foreach (var node in this.TSP(graph, visited, start, graph.Count, 1, path, first))
            {
                yield return node;
            }

            yield return path;
        }

        private IEnumerable<GraphNode> TSP(Dictionary<string, Dictionary<string, int>> graph, HashSet<string> visited, GraphNode currPos, int n, int count, GraphNode ans, string first)
        {
            if (count == n)
            {
                var cost = currPos.Cost + graph[currPos.CurrentPerson][first] + graph[first][currPos.CurrentPerson];
                if (ans.Cost < cost)
                {
                    ans.Parent = currPos.Parent;
                    ans.Cost = cost;
                    ans.CurrentPerson = currPos.CurrentPerson;
                }

                yield break;
            }

            foreach (var nextLocation in graph[currPos.CurrentPerson].Keys)
            {
                if (!visited.Contains(nextLocation))
                {
                    visited.Add(nextLocation);
                    var next = new GraphNode
                    {
                        Parent = currPos,
                        Cost = currPos.Cost + graph[currPos.CurrentPerson][nextLocation] + graph[nextLocation][currPos.CurrentPerson],
                        CurrentPerson = nextLocation,
                    };

                    yield return next;

                    foreach (var innerNodes in this.TSP(graph, visited, next, n, count + 1, ans, first))
                    {
                        yield return innerNodes;
                    }

                    visited.Remove(nextLocation);
                }
            }
        }
    }
}
