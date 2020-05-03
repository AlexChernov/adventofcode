using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode.Solutions.Common;

namespace AdventOfCode.Solutions.Event2016.Day24
{
    public partial class Day24 : IAdventOfCodeDayRunner
    {
        public bool HaveVisualization()
        {
            return true;
        }

        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            InitNodes(lines, out var locations, out var map);
            if (!locations.TryGetValue('0', out _))
            {
                yield return "Wrong input!";
                yield break;
            }

            var spinner = new Lazy<Spinner>(() => new Spinner());
            var printMap = new Lazy<char[,]>(() => InitPrintMap(map));
            var graph = new Dictionary<char, Dictionary<char, GraphNode>>();
            foreach (var location in locations.Keys)
            {
                foreach (var state in FillGraphForLocation(graph, map, locations, location))
                {
                    if (!shouldVisualise)
                    {
                        continue;
                    }

                    UpdatePrintMap(printMap.Value, state);
                    spinner.Value.Turn();
                    var titleCalc = "Calculating path... " + spinner.Value.State;

                    yield return PrintPathFindingState(printMap.Value, titleCalc);
                }
            }

            var path = SolveTSP(graph, locations, '0');
            var title = path.H.ToString() + " is the fewest number of steps required to move your goal data to target node.";

            if (!shouldVisualise)
            {
                yield return title;
                yield break;
            }

            var printPath = new LinkedList<GraphNode>();
            char lastLocation = path.LastLocation;
            while (path.Parent != null)
            {
                path = path.Parent;
                var subPath = graph[path.LastLocation][lastLocation];

                while (subPath.Parent != null)
                {
                    printPath.AddFirst(new GraphNode
                    {
                        CurrentPos = subPath.CurrentPos,
                        LastLocation = path.LastLocation,
                    });
                    subPath = subPath.Parent;
                }
                lastLocation = path.LastLocation;
            }

            var printPathMap = InitPrintMap(map);
            foreach (var node in printPath)
            {
                printPathMap[node.CurrentPos.X, node.CurrentPos.Y] = node.LastLocation;
                yield return PrintPathFindingState(printPathMap, title);
            }
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

        private static string VisitLocation(string locationsToVisit, char charToRemove)
        {
            return locationsToVisit.Replace(charToRemove.ToString(), "");
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

        private static IEnumerable<State> FillGraphForLocation(Dictionary<char, Dictionary<char, GraphNode>> graph, MapNode[,] map, Dictionary<char, X_Y> locations, char location)
        {
            var visited = new bool[map.GetLength(0), map.GetLength(1)];
            var paths = new Dictionary<char, GraphNode>();
            var open = new HashSet<GraphNode>
            {
                new GraphNode
                {
                    CurrentPos = locations[location],
                    LocationsToVisit = "",
                    LastLocation = location,
                }
            };

            while (open.Any())
            {
                var nextOpen = new HashSet<GraphNode>();
                foreach (var node in open)
                {
                    var childrenPos = new X_Y[]
                    {
                            new X_Y() { X = node.CurrentPos.X - 1, Y = node.CurrentPos.Y },
                            new X_Y() { X = node.CurrentPos.X + 1, Y = node.CurrentPos.Y },
                            new X_Y() { X = node.CurrentPos.X, Y = node.CurrentPos.Y + 1 },
                            new X_Y() { X = node.CurrentPos.X, Y = node.CurrentPos.Y - 1 },
                    };

                    foreach (var childPos in childrenPos)
                    {
                        var childNode = new GraphNode
                        {
                            CurrentPos = childPos,
                            LocationsToVisit = node.LocationsToVisit,
                            H = node.H + 1,
                            Parent = node,
                            LastLocation = node.LastLocation,
                        };

                        if (!map.InBound(childPos) ||
                            !map[childPos.X, childPos.Y].canMove ||
                            visited[childPos.X, childPos.Y] ||
                            nextOpen.Contains(childNode))
                        {
                            continue;
                        }

                        nextOpen.Add(childNode);

                        if (map[childPos.X, childPos.Y].location.HasValue)
                        {
                            paths.Add(map[childPos.X, childPos.Y].location.Value, childNode);

                            yield return new State
                            {
                                Path = childNode,
                            };
                        }
                    }

                    visited[node.CurrentPos.X, node.CurrentPos.Y] = true;
                }

                open = nextOpen;
            }

            graph.Add(location, paths);
        }

        private char[,] InitPrintMap(MapNode[,] map)
        {
            var printMap = new char[map.GetLength(0), map.GetLength(1)];

            for (int i = 0; i < printMap.GetLength(0); ++i)
            {
                for (int j = 0; j < printMap.GetLength(1); ++j)
                {
                    printMap[i, j] = map[i, j].location ?? (map[i, j].canMove ? '.' : '#');
                }
            }

            return printMap;
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

        private void UpdatePrintMap(char[,] printMap, State state)
        {
            var path = state.Path;
            while (path.Parent != null)
            {
                printMap[path.CurrentPos.X, path.CurrentPos.Y] = path.LastLocation;
                path = path.Parent;
            }
        }

        private static void InitNodes(string[] lines, out Dictionary<char, X_Y> locations, out MapNode[,] map)
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
