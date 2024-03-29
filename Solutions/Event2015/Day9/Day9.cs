﻿namespace AdventOfCode.Solutions.Event2015.Day9
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic of the Day 9.
    /// </summary>
    public class Day9 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HasVisualization() => true;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var map = this.InitMap(lines);

            GraphNode path = null;
            var spin = new Spinner();
            var drawing = new Lazy<Drawing>(() => new Drawing(10));

            foreach (var node in this.SolveGraph(map))
            {
                path = node;

                if (!shouldVisualise)
                {
                    continue;
                }

                drawing.Value.Update(node);
                spin.Turn();

                var outstr = new StringBuilder("Calculating... ");
                outstr.AppendLine(spin.State);
                outstr.AppendLine(drawing.Value.GetStateStr());

                yield return outstr.ToString();
            }

            if (path != null)
            {
                yield return path.Cost.ToString() + " is the distance of the shortest route";
            }
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
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

        private IEnumerable<GraphNode> SolveTSP(Dictionary<string, Dictionary<string, int>> graph)
        {
            var path = new GraphNode
            {
                Cost = 0,
            };

            foreach (var node in this.TSP(graph, new HashSet<string>(), null, graph.Count, 0, path))
            {
                yield return node;
            }

            yield return path;
        }

        private IEnumerable<GraphNode> TSP(Dictionary<string, Dictionary<string, int>> graph, HashSet<string> visited, GraphNode currPos, int n, int count, GraphNode ans)
        {
            if (count == n)
            {
                if (ans.Cost < currPos.Cost)
                {
                    ans.Parent = currPos.Parent;
                    ans.Cost = currPos.Cost;
                    ans.CurrentPos = currPos.CurrentPos;
                }

                yield break;
            }

            if (currPos == null)
            {
                foreach (var nextLocation in graph.Keys)
                {
                    visited.Add(nextLocation);
                    var next = new GraphNode
                    {
                        Parent = currPos,
                        Cost = 0,
                        CurrentPos = nextLocation,
                    };
                    foreach (var innerNodes in this.TSP(graph, visited, next, n, count + 1, ans))
                    {
                        yield return innerNodes;
                    }

                    visited.Remove(nextLocation);
                }
            }
            else
            {
                foreach (var nextLocation in graph[currPos.CurrentPos].Keys)
                {
                    if (!visited.Contains(nextLocation))
                    {
                        visited.Add(nextLocation);
                        var next = new GraphNode
                        {
                            Parent = currPos,
                            Cost = currPos.Cost + graph[currPos.CurrentPos][nextLocation],
                            CurrentPos = nextLocation,
                        };

                        yield return next;

                        foreach (var innerNodes in this.TSP(graph, visited, next, n, count + 1, ans))
                        {
                            yield return innerNodes;
                        }

                        visited.Remove(nextLocation);
                    }
                }
            }
        }

        private IEnumerable<GraphNode> SolveGraph(Dictionary<string, Dictionary<string, int>> graph)
        {
            GraphNode path = null;

            var open = new HashSetOrderedBy<GraphNode, int>((state) => state.Cost);
            var close = new HashSet<GraphNode>();
            var keyToHash = new Dictionary<string, int>();

            var hash = 1;
            foreach (var key in graph.Keys)
            {
                keyToHash.Add(key, hash);
                hash <<= 1;
            }

            var locationsToVisit = hash - 1;

            foreach (var location in graph.Keys)
            {
                var initState = new GraphNode
                {
                    CurrentPos = location,
                    LocationsToVisit = locationsToVisit - keyToHash[location],
                    Cost = 0,
                };
                open.Add(initState);
            }

            while (open.Any())
            {
                var currentNode = open.ValueWithMinSelector();

                var children = this.GenerateChildren(currentNode, graph, path, keyToHash);

                foreach (var child in children)
                {
                    if (open.TryGetValue(child, out GraphNode existingNode) || close.TryGetValue(child, out existingNode))
                    {
                        if (existingNode.Cost <= child.Cost)
                        {
                            continue;
                        }

                        open.Remove(existingNode);
                    }

                    open.Add(child);
                    if (child.LocationsToVisit == 0)
                    {
                        path = child;
                        var nodesToDelete = open.Where(n => n.Cost >= path.Cost).ToList();
                        foreach (var nd in nodesToDelete)
                        {
                            open.Remove(nd);
                        }
                    }
                }

                open.Remove(currentNode);
                close.Add(currentNode);

                yield return currentNode;
            }

            yield return path;
        }

        private IEnumerable<GraphNode> GenerateChildren(GraphNode currentNode, Dictionary<string, Dictionary<string, int>> graph, GraphNode path, Dictionary<string, int> keyToHash)
        {
            foreach (var kvp in graph[currentNode.CurrentPos])
            {
                if ((currentNode.LocationsToVisit & keyToHash[kvp.Key]) == 0)
                {
                    continue;
                }

                var h = currentNode.Cost + kvp.Value;

                if (path != null && path.Cost < h)
                {
                    continue;
                }

                yield return new GraphNode
                {
                    CurrentPos = kvp.Key,
                    Cost = h,
                    LocationsToVisit = currentNode.LocationsToVisit - keyToHash[kvp.Key],
                    Parent = currentNode,
                };
            }
        }

        private Dictionary<string, Dictionary<string, int>> InitMap(string[] lines)
        {
            var distances = new Dictionary<string, Dictionary<string, int>>();

            var regex = new Regex(@"(?<from>\S+) to (?<to>\S+) = (?<distance>\d+)");
            foreach (var line in lines)
            {
                var match = regex.Match(line);
                if (match.Success)
                {
                    var from = match.Groups["from"].Value;
                    var to = match.Groups["to"].Value;
                    var distance = int.Parse(match.Groups["distance"].Value);

                    this.PushPair(distances, from, to, distance);
                }
            }

            return distances;
        }

        private void PushPair(Dictionary<string, Dictionary<string, int>> map, string from, string to, int distance)
        {
            if (!map.ContainsKey(from))
            {
                map.Add(from, new Dictionary<string, int>());
            }

            if (!map.ContainsKey(to))
            {
                map.Add(to, new Dictionary<string, int>());
            }

            map[from].Add(to, distance);
            map[to].Add(from, distance);
        }
    }
}
