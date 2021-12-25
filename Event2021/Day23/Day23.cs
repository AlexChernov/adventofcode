namespace AdventOfCode.Solutions.Event2021
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;
    using AdventOfCode.Solutions.Event2021.Day23Model;

    /// <summary>
    /// Incapsulates logic for Day 23 of event.
    /// </summary>
    public class Day23 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HasVisualization() => true;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var hall = values[1].Substring(1, values[1].Length - 2);
            var room = new char[8];
            room[0] = values[2][3];
            room[1] = values[3][3];
            room[2] = values[2][5];
            room[3] = values[3][5];
            room[4] = values[2][7];
            room[5] = values[3][7];
            room[6] = values[2][9];
            room[7] = values[3][9];

            var root = new State(hall, new string(room), 2, State.TargetRoom);
            var open = new HashSetOrderedBy<State, int>(s => s.G);
            var close = new HashSet<State>();
            open.Add(root);
            State path = null;

            while (open.Any() && path == null)
            {
                var currentNode = open.ValueWithMinSelector();

                var children = currentNode.GetChildren();

                foreach (var child in children)
                {
                    if (shouldVisualise)
                    {
                        yield return child.Parent.ToString();
                        yield return child.ToString();
                    }
                    if (open.TryGetValue(child, out var existingNode))
                    {
                        if (existingNode.H <= child.H)
                        {
                            continue;
                        }
                        open.Remove(existingNode);
                    }
                    if (close.TryGetValue(child, out existingNode))
                    {
                        if (existingNode.H <= child.H)
                        {
                            continue;
                        }
                        close.Remove(existingNode);
                    }

                    open.Add(child);
                    if (child.Room == State.TargetRoom)
                    {
                        path = child;
                        break;
                    }

                }
                open.Remove(currentNode);
                close.Add(currentNode);
            }

            if (shouldVisualise)
            {
                var str = new StringBuilder();
                var _path = path;
                while (_path != null)
                {
                    str.AppendLine(_path.ToString());
                    _path = _path.Parent;
                }
                yield return str.ToString();
            }

            yield return path?.H.ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var hall = values[1].Substring(1, values[1].Length - 2);
            var room = new char[16];
            room[0] = values[2][3];
            room[1] = 'D';
            room[2] = 'D';
            room[3] = values[3][3];

            room[4] = values[2][5];
            room[5] = 'C';
            room[6] = 'B';
            room[7] = values[3][5];


            room[8] = values[2][7];
            room[9] = 'B';
            room[10] = 'A';
            room[11] = values[3][7];

            room[12] = values[2][9];
            room[13] = 'A';
            room[14] = 'C';
            room[15] = values[3][9];

            var root = new State(hall, new string(room), 4, State.TargetRoom2);
            var open = new HashSetOrderedBy<State, int>(s => s.G);
            var close = new HashSet<State>();
            open.Add(root);
            State path = null;

            var wrongParent = 0;
            while (open.Any() && path == null)
            {
                var currentNode = open.ValueWithMinSelector();

                var children = currentNode.GetChildren();

                foreach (var child in children)
                {
                    if (shouldVisualise)
                    {
                        yield return child.Parent.ToString();
                        yield return child.ToString();
                    }
                    if (open.TryGetValue(child, out var existingNode))
                    {
                        if (existingNode.H <= child.H)
                        {
                            continue;
                        }
                        open.Remove(existingNode);
                    }
                    if (close.TryGetValue(child, out existingNode))
                    {
                        if (existingNode.H <= child.H)
                        {
                            continue;
                        }
                        close.Remove(existingNode);
                        wrongParent++;
                    }

                    open.Add(child);
                    if (child.Room == State.TargetRoom2)
                    {
                        path = child;
                        break;
                    }

                }
                open.Remove(currentNode);
                close.Add(currentNode);
            }

            if (shouldVisualise)
            {
                var str = new StringBuilder();
                var _path = path;
                while (_path != null)
                {
                    str.AppendLine(_path.ToString());
                    _path = _path.Parent;
                }
                yield return str.ToString();
            }

            yield return path?.H.ToString();
        }
    }
}
