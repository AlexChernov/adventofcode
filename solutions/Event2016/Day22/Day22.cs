using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using AdventOfCode.Solutions.Common;

namespace AdventOfCode.Solutions.Event2016.Day22
{
    public partial class Day22
    {
        public static string Run1(string input)
        {
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var nodes = InitNodes(lines);

            if (nodes == null)
            {
                return "Wrong input!";
            }

            var listOfAvailabe = GetAvailable(nodes);

            var result = 0;
            foreach (var node in nodes)
            {
                if (node.Used <= 0)
                {
                    continue;
                }
                var index = Utils.IndexOfFirstGreaterValue(listOfAvailabe, node.Used);

                if (index < 0)
                {
                    continue;
                }

                var count = listOfAvailabe.Count - index;
                if (node.Available >= node.Used)
                {
                    count--;
                }
                result += count;
            }


            return result.ToString();
        }

        public static IEnumerable<State> Run2(string input)
        {
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var nodes = InitNodes(lines);

            if (nodes == null)
            {
                return null;
            }
            var posOfEmptyNode = GetEmptyNode(nodes);

            return FindPath(nodes, posOfEmptyNode);
        }

        private static IEnumerable<State> FindPath(Node[,] nodes, X_Y emptyNodePos)
        {
            var targetNodePos = new X_Y { X = nodes.GetLength(0) - 1, Y = 0 };
            var maxAvail = nodes[emptyNodePos.X, emptyNodePos.Y].Available;
            var initState = new GraphNode
            {
                TargetNodePos = targetNodePos,
                EmptyNodePos = emptyNodePos,
                H = 0,
                F = 0,
            };
            var endTargetNodePos = new X_Y { X = 0, Y = 0 };

            var open = new HashSetOrderedBy<GraphNode, int>((state) => state.G);
            //var open = new HashSet<GraphNode>();
            var close = new HashSet<GraphNode>();
            open.Add(initState);
            var count = 0;
            GraphNode path = null;

            while (open.Any() && path == null)
            {
                count++;
                var state = open.ValueWithMinSelector();

                var children = GenerateChildren(state, close, open, nodes, maxAvail, endTargetNodePos);

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
                    open.Add(child);
                    if (child.TargetNodePos.Equals(endTargetNodePos))
                    {
                        path = child;
                        break;
                    }
                }
                open.Remove(state);
                close.Add(state);
                yield return new State
                {
                    Close = close,
                    Open = open,
                    MaxAvail = maxAvail,
                    Nodes = nodes,
                    LastClosed = state,
                    I = count,
                    Path = path,
                };
            }
        }

        private static IEnumerable<GraphNode> GenerateChildren(GraphNode state, HashSet<GraphNode> close, ISet<GraphNode> open, Node[,] nodes, int maxAvail, X_Y endTargetNodePos)
        {
            var children = new X_Y[]
            {
                new X_Y() { X = state.EmptyNodePos.X - 1, Y = state.EmptyNodePos.Y},
                new X_Y() { X = state.EmptyNodePos.X + 1, Y = state.EmptyNodePos.Y},
                new X_Y() { X = state.EmptyNodePos.X, Y = state.EmptyNodePos.Y+1},
                new X_Y() { X = state.EmptyNodePos.X, Y = state.EmptyNodePos.Y-1},
            };

            foreach (var childPos in children)
            {
                if (!(InBound(childPos, nodes) && CanMove(childPos, nodes, maxAvail)))
                {
                    continue;
                }

                var targetPos = state.TargetNodePos.Equals(childPos) ? new X_Y() { X = state.EmptyNodePos.X, Y = state.EmptyNodePos.Y } : new X_Y() { X = state.TargetNodePos.X, Y = state.TargetNodePos.Y };

                var child = new GraphNode
                {
                    EmptyNodePos = childPos,
                    TargetNodePos = targetPos,
                    F = CalcF(targetPos, childPos, endTargetNodePos),
                    H = state.H + 1,
                    Parent = state,
                };

                yield return child;
            }
        }

        private static bool CanMove(X_Y childPos, Node[,] nodes, int maxAvail)
        {
            return nodes[childPos.X, childPos.Y].Used <= maxAvail;
        }

        public static int CalcF(X_Y targetPos, X_Y emptyPos, X_Y endTargetNodePos)
        {
            var adjustedTargets = new X_Y[]
            {
                new X_Y() { X = targetPos.X - 1, Y = targetPos.Y},
                new X_Y() { X = targetPos.X + 1, Y = targetPos.Y},
                new X_Y() { X = targetPos.X,     Y = targetPos.Y+1},
                new X_Y() { X = targetPos.X,     Y = targetPos.Y-1},
            };

            var fs = adjustedTargets.Select(t =>
            {
                var distanceTargetY = Math.Abs(targetPos.Y - endTargetNodePos.Y);
                var distanceEmptyY = Math.Abs(t.Y - endTargetNodePos.Y);

                var distanceTargetX = Math.Abs(targetPos.X - endTargetNodePos.X);
                var distanceEmptyX = Math.Abs(t.X - endTargetNodePos.X);

                var f = CalcFAdjusted(distanceTargetX, distanceTargetY, distanceEmptyX, distanceEmptyY);

                return new { h = emptyPos.CalcManhattanDistance(t), f };
            })
                .OrderBy(x => x.h)
                .ThenBy(x => x.f);
            var target = fs.First();

            return target.h + target.f;
        }

        private static int CalcFAdjusted(int distanceTargetX, int distanceTargetY, int distanceEmptyX, int distanceEmptyY)
        {
            if (distanceTargetX == 0 && distanceTargetY == 0)
            {
                return 0;
            }

            if (distanceTargetX == 0)
            {
                return CalcFAdjustedLine(distanceTargetY, distanceEmptyY);
            }
            if (distanceTargetY == 0)
            {
                return CalcFAdjustedLine(distanceTargetX, distanceEmptyX);
            }

            var near = distanceEmptyX + distanceEmptyY < distanceTargetX + distanceTargetY;
            var diag = Math.Min(distanceTargetY, distanceTargetX);
            var count = 4;
            count += 6 * (diag - 1);
            count += near ? 0 : 2;

            if (distanceTargetY == distanceTargetX)
            {
                return count;
            }

            if (diag == distanceTargetX)
            {
                return count + 2 + CalcFAdjustedLine(distanceTargetY - diag, Math.Abs(distanceEmptyY - diag));
            }

            return count + 2 + CalcFAdjustedLine(distanceTargetX - diag, Math.Abs(distanceEmptyX - diag));
        }

        private static int CalcFAdjustedLine(int distanceTarget, int distanceEmpty)
        {
            if (distanceTarget == distanceEmpty)
            {
                return 3 + 5 * (distanceTarget - 1);
            }
            else
            {
                if (distanceTarget < distanceEmpty)
                {
                    // far 
                    return 5 * distanceTarget;
                }
                // near
                return 1 + 5 * (distanceTarget - 1);
            }
        }

        private static bool InBound(X_Y xy, Node[,] nodes)
        {
            return xy.X >= 0 && xy.X < nodes.GetLength(0) && xy.Y >= 0 && xy.Y < nodes.GetLength(1);
        }

        private static X_Y GetEmptyNode(Node[,] nodes)
        {
            var posOfEmptyNode = new X_Y();
            var maxAvail = 0;

            for (int i = 0; i < nodes.GetLength(0); ++i)
            {
                for (int j = 0; j < nodes.GetLength(1); ++j)
                {
                    if (maxAvail < nodes[i, j].Available)
                    {
                        maxAvail = nodes[i, j].Available;
                        posOfEmptyNode.X = i;
                        posOfEmptyNode.Y = j;
                    }
                }
            }

            return posOfEmptyNode;
        }

        private static List<int> GetAvailable(Node[,] arr)
        {
            var list = new List<int>(arr.Length);
            foreach (var node in arr)
            {
                list.Add(node.Available);
            }
            list.Sort();
            return list;
        }

        private static Node[,] InitNodes(string[] lines)
        {
            Node[,] nodes;
            var last = lines.Last();
            var pattern = @"/dev.+x(?<X>\d+)-y(?<Y>\d+)\s+(?<size>\d+)T\s+(?<used>\d+)T\s+(?<Available>\d+)T\s+(?<UsedPerc>\d+)%";
            var regex = new Regex(pattern);
            var match = regex.Match(last);
            if (match.Success)
            {
                var x = Int32.Parse(match.Groups["X"].Value);
                var y = Int32.Parse(match.Groups["Y"].Value);
                nodes = new Node[x + 1, y + 1];
            }
            else
            {
                return null;
            }

            foreach (var line in lines)
            {
                match = regex.Match(line);
                if (match.Success)
                {
                    var x = Int32.Parse(match.Groups["X"].Value);
                    var y = Int32.Parse(match.Groups["Y"].Value);
                    var used = Int32.Parse(match.Groups["used"].Value);
                    var available = Int32.Parse(match.Groups["Available"].Value);
                    nodes[x, y] = new Node { Used = used, Available = available };
                }
            }

            return nodes;
        }
    }
}
