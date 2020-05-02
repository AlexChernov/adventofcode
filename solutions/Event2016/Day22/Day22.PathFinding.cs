using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Solutions.Common;

namespace AdventOfCode.Solutions.Event2016.Day22
{
    public partial class Day22
    {
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
            var close = new HashSet<GraphNode>();
            open.Add(initState);
            var count = 0;
            GraphNode path = null;

            while (open.Any() && path == null)
            {
                count++;
                var currentNode = open.ValueWithMinSelector();

                var children = GenerateChildren(currentNode, close, open, nodes, maxAvail, endTargetNodePos);

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
                open.Remove(currentNode);
                close.Add(currentNode);
                yield return new State
                {
                    Close = close,
                    Open = open,
                    LastClosed = currentNode,
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
                if (!(nodes.InBound(childPos) && CanMove(childPos, nodes, maxAvail)))
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

        private static bool InBound(X_Y xy, Node[,] nodes)
        {
            return xy.X >= 0 && xy.X < nodes.GetLength(0) && xy.Y >= 0 && xy.Y < nodes.GetLength(1);
        }
    }
}
