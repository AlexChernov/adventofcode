namespace AdventOfCode.Solutions.Event2020.Day12
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 11 of event.
    /// </summary>
    public partial class Day12 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HasVisualization() => false;


        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Select(str => (c: str[0], v: int.Parse(str.Substring(1))));

            var cur = new Navigation()
            {
                direction = Direction.East,
                xy = new X_Y()
            };

            foreach (var value in values)
            {
                switch (value.c)
                {
                    case 'N':
                        MoveByNav(cur, Direction.North, value.v);
                        break;
                    case 'E':
                        MoveByNav(cur, Direction.East, value.v);
                        break;
                    case 'S':
                        MoveByNav(cur, Direction.South, value.v);
                        break;
                    case 'W':
                        MoveByNav(cur, Direction.West, value.v);
                        break;
                    case 'L':
                        Turn(cur, -value.v);
                        break;
                    case 'R':
                        Turn(cur,value.v);
                        break;
                    case 'F':
                        MoveByNav(cur, cur.direction, value.v);
                        break;
                    default:
                        break;
                }
            }

            yield return cur.xy.CalcManhattanDistance(new X_Y()).ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Select(str => (c: str[0], v: int.Parse(str.Substring(1))));

            var cur = new Navigation()
            {
                direction = Direction.East,
                xy = new X_Y(),
                navPoint = new X_Y
                {
                    X = 1,
                    Y = 10,
                }
            };

            foreach (var value in values)
            {
                switch (value.c)
                {
                    case 'N':
                        MoveNav(cur, Direction.North, value.v);
                        break;
                    case 'E':
                        MoveNav(cur, Direction.East, value.v);
                        break;
                    case 'S':
                        MoveNav(cur, Direction.South, value.v);
                        break;
                    case 'W':
                        MoveNav(cur, Direction.West, value.v);
                        break;
                    case 'L':
                        TurnNav(cur, -value.v);
                        break;
                    case 'R':
                        TurnNav(cur, value.v);
                        break;
                    case 'F':
                        MoveByNav(cur, value.v);
                        break;
                    default:
                        break;
                }
            }

            yield return cur.xy.CalcManhattanDistance(new X_Y()).ToString();
        }

        private void MoveByNav(Navigation cur, int value)
        {
            cur.xy.X += cur.navPoint.X * value;
            cur.xy.Y += cur.navPoint.Y * value;
        }

        private void TurnNav(Navigation cur, int v)
        {
            var turns = Utils.Mod(v / 90, 4);

            for (var i = 0; i < turns; ++i)
            {
                var temp = cur.navPoint.X;
                cur.navPoint.X = -cur.navPoint.Y;
                cur.navPoint.Y = temp;
            }
        }

        private void MoveNav(Navigation cur, Direction direction, int value)
        {
            switch (direction)
            {
                case Direction.North:
                    cur.navPoint.X += value;
                    break;
                case Direction.East:
                    cur.navPoint.Y += value;
                    break;
                case Direction.South:
                    cur.navPoint.X -= value;
                    break;
                case Direction.West:
                    cur.navPoint.Y -= value;
                    break;
                default:
                    break;
            }
        }

        private void Turn(Navigation cur, int v)
        {
            var newDir = Utils.Mod(v / 90 + (int)cur.direction, 4);
            cur.direction = (Direction)newDir;
        }

        private void MoveByNav(Navigation cur, Direction direction, int value)
        {
            switch (direction)
            {
                case Direction.North:
                    cur.xy.X += value;
                    break;
                case Direction.East:
                    cur.xy.Y += value;
                    break;
                case Direction.South:
                    cur.xy.X -= value;
                    break;
                case Direction.West:
                    cur.xy.Y -= value;
                    break;
                default:
                    break;
            }
        }
    }
}
