namespace AdventOfCode.Solutions.Event2016.Day22
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 22 of event.
    /// </summary>
    public partial class Day22 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HasVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var nodes = InitNodes(lines);

            if (nodes == null)
            {
                yield return "Wrong input!";
                yield break;
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

            yield return "There are " + result.ToString() + " viable pairs of nodes.";
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var nodes = InitNodes(lines);

            if (nodes == null)
            {
                yield return "Wrong input!";
                yield break;
            }

            var posOfEmptyNode = GetEmptyNode(nodes);
            GraphNode pathNode = null;
            var calcMap = shouldVisualise ? this.InitMap(nodes, posOfEmptyNode) : null;
            var spinner = new Spinner();
            foreach (var state in FindPath(nodes, posOfEmptyNode))
            {
                pathNode = state.Path;
                if (!shouldVisualise)
                {
                    continue;
                }

                this.UpdateCalcMap(calcMap, state);
                spinner.Turn();
                var titleCalc = "Calculating path... " + spinner.State;

                yield return this.PrintPathFindingState(calcMap, state.LastClosed, titleCalc);
            }

            var title = pathNode.H.ToString() + " is the fewest number of steps required to move your goal data to target node.";

            if (!shouldVisualise)
            {
                yield return title;
                yield break;
            }

            var path = new LinkedList<GraphNode>();
            while (pathNode != null)
            {
                path.AddFirst(pathNode);
                pathNode = pathNode.Parent;
            }

            var pathMap = InitPathMap(calcMap);
            var step = 0;
            foreach (var node in path)
            {
                pathMap[node.TargetNodePos.X, node.TargetNodePos.Y] = "GGGG";
                pathMap[node.EmptyNodePos.X, node.EmptyNodePos.Y] = "_" + step.ToString().PadLeft(3, '_');

                yield return PrintState(pathMap, node.EmptyNodePos, title);
                ++step;
            }
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
                var x = int.Parse(match.Groups["X"].Value);
                var y = int.Parse(match.Groups["Y"].Value);
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
                    var x = int.Parse(match.Groups["X"].Value);
                    var y = int.Parse(match.Groups["Y"].Value);
                    var used = int.Parse(match.Groups["used"].Value);
                    var available = int.Parse(match.Groups["Available"].Value);
                    nodes[x, y] = new Node { Used = used, Available = available };
                }
            }

            return nodes;
        }
    }
}
