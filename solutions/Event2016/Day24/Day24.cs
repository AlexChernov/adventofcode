using AdventOfCode.Solutions.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Event2016.Day24
{
    partial class Day24 : IAdventOfCodeDayRunner
    {
        public bool HaveVisualization() => true;

        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {

            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            InitNodes(lines, out var locations, out var map);
            if (!locations.TryGetValue('0', out _))
            {
                yield return "Wrong input!";
                yield break;
            }

            var graph = SimplifyMapBFS(map, locations);
            var path = SolveTSP(graph, locations, '0');

            var title = path.H.ToString() + " is the fewest number of steps required to move your goal data to target node.";

            yield return title;
        }

        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            throw new NotImplementedException();
        }

        private GraphNode SolveTSP(Dictionary<char, Dictionary<char, GraphNode>> graph, Dictionary<char, X_Y> locations, char v)
        {
            GraphNode path = null;

            var locationsToVisit = new string(locations.Keys.OrderBy(c => c).ToArray());
            locationsToVisit = VisitLocation(locationsToVisit, v);

            var initState = new GraphNode
            {
                CurrentPos = locations[v],
                LocationsToVisit = locationsToVisit,
                LastLocation = '0',
                H = 0,
                F = 0,
            };

            var open = new HashSetOrderedBy<GraphNode, int>((state) => state.G);
            var close = new HashSet<GraphNode>();
            open.Add(initState);
            var count = 0;

            while (open.Any())
            {
                count++;
                var currentNode = open.ValueWithMinSelector();
                var lastOpen = new LinkedList<GraphNode>();

                var children = GenerateChildrenTSP(currentNode, graph, locations, path);

                foreach (var child in children)
                {
                    if (open.TryGetValue(child, out GraphNode existingNode) || close.TryGetValue(child, out existingNode))
                    {
                        if (existingNode.H <= child.H)
                        {
                            continue;
                        }
                        open.Remove(existingNode);
                    }
                    lastOpen.AddLast(child);
                    open.Add(child);
                    if (!child.LocationsToVisit.Any() && (path == null || path.H > child.H))
                    {
                        path = child;
                        var nodesToDelete = open.Where(n => n.H >= path.H).ToList();
                        foreach (var nd in nodesToDelete)
                        {
                            open.Remove(nd);
                        }

                    }
                }
                open.Remove(currentNode);
                close.Add(currentNode);
            }

            return path;
        }

        private IEnumerable<GraphNode> GenerateChildrenTSP(GraphNode currentNode, Dictionary<char, Dictionary<char, GraphNode>> graph, Dictionary<char, X_Y> locations, GraphNode path)
        {
            foreach (var nextLocationPath in graph[currentNode.LastLocation])
            {
                var h = currentNode.H + nextLocationPath.Value.H;

                if (path != null && path.H < h)
                {
                    continue;
                }

                yield return new GraphNode
                {
                    CurrentPos = locations[nextLocationPath.Key],
                    F = 0,
                    H = h,
                    LastLocation = nextLocationPath.Key,
                    LocationsToVisit = VisitLocation(currentNode.LocationsToVisit, nextLocationPath.Key),
                    Parent = currentNode,
                };
            }
        }

        public static Dictionary<char, Dictionary<char, GraphNode>> SimplifyMapBFS(MapNode[,] map, Dictionary<char, X_Y> locations)
        {
            var simpleMap = new Dictionary<char, Dictionary<char, GraphNode>>();
            foreach (var location in locations.Keys)
            {
                var visited = new bool[map.GetLength(0), map.GetLength(1)];

                var open = new HashSet<GraphNode>
                {
                    new GraphNode
                    {
                        CurrentPos = locations[location],
                        LocationsToVisit = "",
                    }
                };
                var paths = new Dictionary<char, GraphNode>();
                while (open.Any())
                {
                    var nextOpen = new HashSet<GraphNode>();
                    foreach (var node in open)
                    {
                        var childrenPos = new X_Y[]
                        {
                            new X_Y() { X = node.CurrentPos.X - 1, Y = node.CurrentPos.Y},
                            new X_Y() { X = node.CurrentPos.X + 1, Y = node.CurrentPos.Y},
                            new X_Y() { X = node.CurrentPos.X, Y = node.CurrentPos.Y + 1},
                            new X_Y() { X = node.CurrentPos.X, Y = node.CurrentPos.Y - 1},
                        };

                        foreach (var childPos in childrenPos)
                        {
                            var childNode = new GraphNode
                            {
                                CurrentPos = childPos,
                                LocationsToVisit = node.LocationsToVisit,
                                H = node.H + 1,
                                Parent = node,
                            };
                            if (!map.InBound(childPos) || !map[childPos.X, childPos.Y].canMove || visited[childPos.X, childPos.Y] || nextOpen.Contains(childNode))
                            {
                                continue;
                            }
                            nextOpen.Add(childNode);
                            if (map[childPos.X, childPos.Y].location.HasValue)
                            {
                                paths.Add(map[childPos.X, childPos.Y].location.Value, childNode);
                            }
                        }
                        visited[node.CurrentPos.X, node.CurrentPos.Y] = true;
                    }
                    open = nextOpen;
                }
                simpleMap.Add(location, paths);

            }
            return simpleMap;
        }

        private string PrintPathFindingState(char[,] printMap, string title)
        {
            var str = new StringBuilder(title);
            str.AppendLine();

            for (int i = 0; i < printMap.GetLength(0); ++i)
            {
                for (int j = 0; j < printMap.GetLength(1); ++j)
                {
                    str.Append(printMap[i, j]);
                }
                str.AppendLine();
            }

            return str.ToString();
        }

        private void UpdateCalcMap(char[,] calcMap, State state)
        {
            calcMap[state.LastClosed.CurrentPos.X, state.LastClosed.CurrentPos.Y] = '!';

            foreach (var open in state.LastOpen)
            {
                calcMap[open.CurrentPos.X, open.CurrentPos.Y] = '?';
            }
        }

        private char[,] InitPrintMap(MapNode[,] map)
        {
            var printMap = new char[map.GetLength(0), map.GetLength(1)];

            for (int i = 0; i < printMap.GetLength(0); ++i)
            {
                for (int j = 0; j < printMap.GetLength(1); ++j)
                {
                    printMap[i, j] = map[i, j].location.HasValue ? map[i, j].location.Value : (map[i, j].canMove ? '.' : '#');
                }
            }

            return printMap;
        }

        public static void InitNodes(string[] lines, out Dictionary<char, X_Y> locations, out MapNode[,] map)
        {
            locations = new Dictionary<char, X_Y>();
            map = new MapNode[lines.Length, lines[0].Length];
            for (int i = 0; i < lines.Length; ++i)
            {
                for (int j = 0; j < lines[0].Length; ++j)
                {
                    map[i, j] = new MapNode { canMove = lines[i][j] != '#' };
                    if ('0' <= lines[i][j] && lines[i][j] <= '9')
                    {
                        map[i, j].location = lines[i][j];
                        locations.Add(lines[i][j], new X_Y { X = i, Y = j });
                    }
                }
            }
        }
    }
}
