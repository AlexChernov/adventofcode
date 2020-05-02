using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using AdventOfCode.Solutions.Common;

namespace AdventOfCode.Solutions.Event2016.Day24
{
    public partial class Day24
    {
        private IEnumerable<State> FindPath(MapNode[,] map, Dictionary<char, X_Y> locations, X_Y startPos)
        {
            var locationsToVisit = new string(locations.Keys.OrderBy(c => c).ToArray());
            locationsToVisit = VisitLocation(locationsToVisit, '0');

            var initState = new GraphNode
            {
                CurrentPos = startPos,
                LocationsToVisit = locationsToVisit,
                LastLocation = '0',
                H = 0,
                F = 0,
            };

            var open = new HashSetOrderedBy<GraphNode, int>((state) => state.G);
            var close = new HashSet<GraphNode>();
            open.Add(initState);
            var count = 0;
            GraphNode path = null;

            while (open.Any() && path == null)
            {
                count++;
                var currentNode = open.ValueWithMinSelector();
                var lastOpen = new LinkedList<GraphNode>();

                var children = GenerateChildren(currentNode, close, open, map, locations);

                foreach (var child in children)
                {
                    GraphNode existingNode;
                    if (open.TryGetValue(child, out existingNode) || close.TryGetValue(child, out existingNode))
                    {
                        if (existingNode.H <= child.H)
                        {
                            continue;
                        }
                        open.Remove(existingNode);
                    }
                    lastOpen.AddLast(child);
                    open.Add(child);
                    if (!child.LocationsToVisit.Any())
                    {
                        path = child;
                        break;
                    }
                }
                open.Remove(currentNode);
                close.Add(currentNode);
                yield return new State
                {
                    LastOpen = lastOpen,
                    LastClosed = currentNode,
                    Path = path,
                };
            }
        }

        private static string VisitLocation(string locationsToVisit, char charToRemove)
        {
            return locationsToVisit.Replace(charToRemove.ToString(), "");
        }

        private static IEnumerable<GraphNode> GenerateChildren(GraphNode node, HashSet<GraphNode> close, HashSetOrderedBy<GraphNode, int> open, MapNode[,] map, Dictionary<char, X_Y> locations)
        {
            var childrenPos = new X_Y[]
            {
                new X_Y() { X = node.CurrentPos.X - 1, Y = node.CurrentPos.Y},
                new X_Y() { X = node.CurrentPos.X + 1, Y = node.CurrentPos.Y},
                new X_Y() { X = node.CurrentPos.X, Y = node.CurrentPos.Y+1},
                new X_Y() { X = node.CurrentPos.X, Y = node.CurrentPos.Y-1},
            };

            foreach (var childPos in childrenPos)
            {
                if (!(map.InBound(childPos) && map[childPos.X, childPos.Y].canMove))
                {
                    continue;
                }

                var location = map[childPos.X, childPos.Y].location;
                var locationsToVisit = location.HasValue ? VisitLocation(node.LocationsToVisit, location.Value) : node.LocationsToVisit;

                var child = new GraphNode
                {
                    CurrentPos = childPos,
                    F = CalcF(locationsToVisit, locations, childPos),
                    H = node.H + 1,
                    Parent = node,
                    LocationsToVisit = locationsToVisit,
                    LastLocation = location.HasValue ? location.Value : node.LastLocation,
                };

                yield return child;
            }
        }

        private static int CalcF(string locationsToVisit, Dictionary<char, X_Y> locations, X_Y childPos)
        {
            if (locationsToVisit.Length == 0)
            {
                return 0;
            }

            var min = Int32.MaxValue;
            var nextLocation = locationsToVisit[0];
            foreach (var location in locationsToVisit)
            {
                var dist = locations[location].CalcManhattanDistance(childPos);
                if (dist < min)
                {
                    min = dist;
                    nextLocation = location;
                }
            }

            return min + CalcF(VisitLocation(locationsToVisit, nextLocation), locations, locations[nextLocation]);
        }
    }
}
