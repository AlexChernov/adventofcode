using System.Text;
using AdventOfCode.Solutions.Common;

namespace AdventOfCode.Solutions.Event2016.Day22
{
    public partial class Day22
    {
        private static string PrintState(string[,] output, X_Y emptyNodePos, string title)
        {
            var outStr = new StringBuilder();
            outStr.AppendLine(title);
            for (int i = 0; i < output.GetLength(0); ++i)
            {
                if (i == emptyNodePos.X && 0 == emptyNodePos.Y)
                {
                    outStr.Append("[");

                }
                else if (i == 0)
                {
                    outStr.Append("(");
                }
                else
                {
                    outStr.Append(" ");
                }
                for (int j = 0; j < output.GetLength(1); ++j)
                {
                    outStr.Append(output[i, j].ToString());
                    if (i == emptyNodePos.X && j == emptyNodePos.Y - 1)
                    {
                        outStr.Append("[");
                    }
                    else if (i == emptyNodePos.X && j == emptyNodePos.Y)
                    {
                        outStr.Append("]");
                    }
                    else if (i == 0 && j == 0)
                    {
                        outStr.Append(")");
                    }
                    else
                    {
                        outStr.Append(" ");
                    }
                }
                outStr.AppendLine();
            }
            return outStr.ToString();
        }

        private static string[,] InitPathMap(MapNode[,] input)
        {
            string[,] output = new string[input.GetLength(0), input.GetLength(1)];
            for (int i = 0; i < output.GetLength(0); ++i)
            {
                for (int j = 0; j < output.GetLength(1); ++j)
                {
                    output[i, j] = input[i, j].canMove ? "...." : "####";
                }
            }

            return output;
        }

        private void UpdateCalcMap(MapNode[,] calcMap, State state)
        {
            for (int i = 0; i < calcMap.GetLength(0); ++i)
            {
                for (int j = 0; j < calcMap.GetLength(1); ++j)
                {
                    calcMap[i, j].openCount = 0;
                    calcMap[i, j].closeCount = 0;
                }
            }

            foreach (var openNode in state.Open)
            {
                calcMap[openNode.EmptyNodePos.X, openNode.EmptyNodePos.Y].openCount++;
            }

            foreach (var closeNode in state.Close)
            {
                calcMap[closeNode.EmptyNodePos.X, closeNode.EmptyNodePos.Y].closeCount++;
            }
        }

        private string PrintPathFindingState(MapNode[,] output, GraphNode lastClosed, string title)
        {
            var outStr = new StringBuilder();
            outStr.AppendLine(title);

            for (int i = 0; i < output.GetLength(0); ++i)
            {
                if (i == lastClosed.EmptyNodePos.X && 0 == lastClosed.EmptyNodePos.Y)
                {
                    outStr.Append("[");

                }
                else
                {
                    outStr.Append(" ");
                }

                for (int j = 0; j < output.GetLength(1); ++j)
                {
                    outStr.Append(output[i, j].ToString());

                    if (i == lastClosed.EmptyNodePos.X && j == lastClosed.EmptyNodePos.Y)
                    {
                        outStr.Append("]");
                    }
                    else if (i == lastClosed.EmptyNodePos.X && j == lastClosed.EmptyNodePos.Y - 1)
                    {
                        outStr.Append("[");

                    }
                    else
                    {
                        outStr.Append(" ");
                    }
                }
                outStr.AppendLine();
            }

            return outStr.ToString();
        }

        private static MapNode[,] InitMap(Node[,] nodes, X_Y posOfEmptyNode)
        {
            var maxAvail = nodes[posOfEmptyNode.X, posOfEmptyNode.Y].Available;
            MapNode[,] output = new MapNode[nodes.GetLength(0), nodes.GetLength(1)];
            for (int i = 0; i < output.GetLength(0); ++i)
            {
                for (int j = 0; j < output.GetLength(1); ++j)
                {
                    output[i, j] = new MapNode
                    {
                        canMove = nodes[i, j].Used <= maxAvail
                    };
                }
            }

            return output;
        }
    }
}
