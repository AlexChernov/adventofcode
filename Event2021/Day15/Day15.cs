namespace AdventOfCode.Solutions.Event2021
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 15 of event.
    /// </summary>
    public class Day15 : IAdventOfCodeDayRunner
    {

        /// <inheritdoc/>
        public bool HasVisualization() => true;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var len = values.Length;
            var len2 = values[0].Length;
            var map = new int[len, len2];

            for (var i = 0; i < len; i++)
            {
                for (var j = 0; j < len2; j++)
                {
                    map[i, j] = int.Parse(values[i][j].ToString());
                }
            }

            var pathCost = FindPath(map);

            yield return pathCost.ToString();
        }

        private int FindPath(int[,] map)
        {
            var len = map.GetLength(0);
            var len2 = map.GetLength(1);

            var initState = new GraphNode
            {
                pos = (0, 0),
                F = len + len2 - 2,
            };
            var target = (x: len - 1, y: len2 - 1);

            var open = new HashSetOrderedBy<GraphNode, int>((state) => state.G);
            var close = new HashSet<GraphNode>();
            open.Add(initState);
            var count = 0;
            GraphNode path = null;

            while (open.Any() && path == null)
            {
                count++;
                var currentNode = open.ValueWithMinSelector();

                var children = GenerateChildren(currentNode, map, target);

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

                    open.Add(child);
                    if (child.pos.Equals(target))
                    {
                        path = child;
                        break;
                    }
                }

                open.Remove(currentNode);
                close.Add(currentNode);
            }
            return path?.H ?? 0;
        }

        private IEnumerable<GraphNode> GenerateChildren(GraphNode currentNode, int[,] map, (int x, int y) target)
        {
            var (x, y) = currentNode.pos;
            var children = new[]
            {
                (x: x+1 ,y:y),
                (x: x   ,y:y+1),
                (x: x-1 ,y:y),
                (x: x   ,y:y-1),
            };

            foreach (var childPos in children)
            {
                if (!map.InBound(childPos.x, childPos.y))
                {
                    continue;
                }

                var child = new GraphNode
                {
                    pos = childPos,
                    F = target.x - childPos.x + target.y - childPos.y,
                    H = currentNode.H + map[childPos.x, childPos.y],
                    Parent = currentNode,
                };

                yield return child;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var len = values.Length;
            var len2 = values[0].Length;
            var map = new int[len * 5, len2 * 5];

            for (var i = 0; i < len; i++)
            {
                for (var j = 0; j < len2; j++)
                {
                    map[i, j] = int.Parse(values[i][j].ToString());
                }
            }

            for (var r = 1; r < 5; r++)
            {
                for (var i = 0; i < len; i++)
                {
                    for (var j = 0; j < len2; j++)
                    {
                        map[len * r + i, j] = NextValue(map[i, j], r);
                    }
                }
            }

            for (var r = 0; r < 5; r++)
            {
                for (var c = 1; c < 5; ++c)
                {
                    for (var i = 0; i < len; i++)
                    {
                        for (var j = 0; j < len2; j++)
                        {
                            map[len * r + i, len2 * c + j] = NextValue(map[len * r + i, j], c);
                        }
                    }
                }
            }

            var pathCost = FindPath(map);

            yield return pathCost.ToString();
        }

        private int NextValue(int v, int r)
        {
            var next = (v + r) % 9;
            next = next == 0 ? 9 : next;
            return next;
        }
    }
}
