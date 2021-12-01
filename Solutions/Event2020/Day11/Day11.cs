namespace AdventOfCode.Solutions.Event2020.Day11
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 11 of event.
    /// </summary>
    public class Day11 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>   
        public bool HasVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var inputs = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            Node[,] map = new Node[inputs.Length, inputs[0].Length];

            for (int i = 0; i < inputs.Length; ++i)
            {
                for (int j = 0; j < inputs[i].Length; ++j)
                {
                    map[i, j] = inputs[i][j] == 'L' ? Node.Empty : Node.Floor;
                }
            }

            var repeat = true;

            while (repeat)
            {
                repeat = false;
                var nextMap = new Node[map.GetLength(0), map.GetLength(1)];

                for (int i = 0; i < map.GetLength(0); ++i)
                {
                    for (int j = 0; j < map.GetLength(1); ++j)
                    {
                        var adjacent = GetAdjacent(map, i, j);

                        if (map[i, j] == Node.Empty && adjacent.All(n => n != Node.Occupied))
                        {
                            nextMap[i, j] = Node.Occupied;
                        }
                        else if (map[i, j] == Node.Occupied && adjacent.Count(n => n == Node.Occupied) >= 4)
                        {
                            nextMap[i, j] = Node.Empty;
                        }
                        else
                        {
                            nextMap[i, j] = map[i, j];
                        }

                        if (nextMap[i, j] != map[i, j])
                        {
                            repeat = true;
                        }
                    }
                }

                map = nextMap;
            }

            var count = 0;
            foreach (var a in map)
            {
                count += a == Node.Occupied ? 1 : 0;
            }

            yield return count.ToString();
        }

        private IEnumerable<Node> GetAdjacent(Node[,] map, int i, int j)
        {
            var ii = i - 1;

            if (i != 0)
            {
                if (j != 0)
                {
                    yield return map[ii, j - 1];
                }

                yield return map[ii, j];

                if (j != map.GetLength(1) - 1)
                {
                    yield return map[ii, j + 1];
                }
            }

            ii = i;

            if (j != 0)
            {
                yield return map[ii, j - 1];
            }

            if (j != map.GetLength(1) - 1)
            {
                yield return map[ii, j + 1];
            }

            ii = i + 1;
            if (i != map.GetLength(0) - 1)
            {
                if (j != 0)
                {
                    yield return map[ii, j - 1];
                }

                yield return map[ii, j];

                if (j != map.GetLength(1) - 1)
                {
                    yield return map[ii, j + 1];
                }
            }
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var inputs = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            Node[,] map = new Node[inputs.Length, inputs[0].Length];

            for (int i = 0; i < inputs.Length; ++i)
            {
                for (int j = 0; j < inputs[i].Length; ++j)
                {
                    map[i, j] = inputs[i][j] == 'L' ? Node.Empty : Node.Floor;
                }
            }

            var repeat = true;

            while (repeat)
            {
                repeat = false;
                var nextMap = new Node[map.GetLength(0), map.GetLength(1)];

                for (int i = 0; i < map.GetLength(0); ++i)
                {
                    for (int j = 0; j < map.GetLength(1); ++j)
                    {
                        var adjacent = GetSeatsCanSee(map, i, j);

                        if (map[i, j] == Node.Empty && adjacent.All(n => n != Node.Occupied))
                        {
                            nextMap[i, j] = Node.Occupied;
                        }
                        else if (map[i, j] == Node.Occupied && adjacent.Count(n => n == Node.Occupied) >= 5)
                        {
                            nextMap[i, j] = Node.Empty;
                        }
                        else
                        {
                            nextMap[i, j] = map[i, j];
                        }

                        if (nextMap[i, j] != map[i, j])
                        {
                            repeat = true;
                        }
                    }
                }

                map = nextMap;
            }

            var count = 0;
            foreach (var a in map)
            {
                count += a == Node.Occupied ? 1 : 0;
            }

            yield return count.ToString();
        }

        private IEnumerable<Node> GetSeatsCanSee(Node[,] map, int i, int j)
        {
            var node = Node.Floor;
            node = GetSeatsCanSee(map, i, j, (i) => i - 1, (j) => j - 1);
            if (node != Node.Floor)
            {
                yield return node;
            }
            node = GetSeatsCanSee(map, i, j, (i) => i - 1, (j) => j);
            if (node != Node.Floor)
            {
                yield return node;
            }
            node = GetSeatsCanSee(map, i, j, (i) => i - 1, (j) => j + 1);
            if (node != Node.Floor)
            {
                yield return node;
            }
            node = GetSeatsCanSee(map, i, j, (i) => i, (j) => j - 1);
            if (node != Node.Floor)
            {
                yield return node;
            }
            node = GetSeatsCanSee(map, i, j, (i) => i, (j) => j + 1);
            if (node != Node.Floor)
            {
                yield return node;
            }
            node = GetSeatsCanSee(map, i, j, (i) => i + 1, (j) => j - 1);
            if (node != Node.Floor)
            {
                yield return node;
            }
            node = GetSeatsCanSee(map, i, j, (i) => i + 1, (j) => j);
            if (node != Node.Floor)
            {
                yield return node;
            }
            node = GetSeatsCanSee(map, i, j, (i) => i + 1, (j) => j + 1);
            if (node != Node.Floor)
            {
                yield return node;
            }
        }

        private Node GetSeatsCanSee(Node[,] map, int i, int j, Func<int, int> nextI, Func<int, int> nextJ)
        {
            var node = Node.Floor;
            var ii = nextI(i);
            var jj = nextJ(j);

            while (map.InBound(ii, jj) && node == Node.Floor)
            {
                node = map[ii, jj];
                ii = nextI(ii);
                jj = nextJ(jj);
            }

            return node;
        }
    }

    enum Node
    {
        Empty,
        Occupied,
        Floor,
    }
}
