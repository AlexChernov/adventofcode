using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Event2021.Day23Model
{
    internal class State
    {
        public static string TargetRoom = "AABBCCDD";
        public static string TargetRoom2 = "AAAABBBBCCCCDDDD";
        private readonly int Roomsize;
        private string Target;

        private State()
        {
        }

        public State(string hall, string room, int roomsize, string target)
        {
            this.Hall = hall;
            this.Room = room;
            this.Roomsize = roomsize;
            this.Target = target;

            this.F = CalcF2();
        }

        private int CalcF2()
        {
            var f = 0;
            for (int i = 0; i < 4; ++i)
            {
                var validRoom = true;
                for (int j = 0; j < Roomsize; ++j)
                {
                    var index = i * Roomsize + (Roomsize - j - 1);
                    char amphipod = Room[index];
                    if (amphipod == '.') continue;
                    if (Target[index] == amphipod)
                    {
                        if (validRoom) continue;
                        else
                        {
                            f += (Roomsize - j + 3) * Cost(amphipod);
                        }
                    }
                    else
                    {
                        validRoom = false;
                        var targetHallIndex = GetHallIndex(GetRoomIndex(amphipod));
                        var hallIndex = GetHallIndex(index);
                        f += (Math.Abs(targetHallIndex - hallIndex) + 1 + Roomsize - j) * Cost(amphipod);
                    }
                }
            }

            for (int i = 0; i < Hall.Length; ++i)
            {
                char amphipod = Hall[i];
                if (amphipod == '.') continue;

                var targetHallIndex = GetHallIndex(GetRoomIndex(amphipod));
                f += (Math.Abs(targetHallIndex - i) + 1) * Cost(amphipod);
            }

            for (int i = 0; i < 4; ++i)
            {
                var validRoom = true;
                int j = 0;
                for (j = 0; j < Roomsize && validRoom; ++j)
                {
                    var index = i * Roomsize + (Roomsize - j - 1);
                    char amphipod = Room[index];
                    if (amphipod == '.') continue;
                    if (Target[index] != amphipod)
                    {
                        validRoom = false;
                    }
                }
                if (!validRoom)
                {
                    var steps = GetExtraSteps(j);
                    f += steps * Cost(Target[i*Roomsize]);
                }
            }

            return f;
        }

        private int GetExtraSteps(int j)
        {
            switch (j)
            {
                case 0: return 6;
                case 1: return 3;
                case 2: return 1;
                default: return 0;
            }
        }

        private int CalcF()
        {
            return 0;

            var f = 0;
            for (int i = 0; i < Room.Length; i++)
            {
                char amphipod = Room[i];
                if (amphipod == '.') continue;
                if (Target[i] == amphipod)
                {
                    continue;
                }
                var targetHallIndex = GetHallIndex(GetRoomIndex(amphipod));
                var hallIndex = GetHallIndex(i);
                f += (Math.Abs(targetHallIndex - hallIndex) + 2 + (i % Roomsize)) * Cost(amphipod);
            }

            for (int i = 0; i < Hall.Length; ++i)
            {
                char amphipod = Hall[i];
                if (amphipod == '.') continue;

                var targetHallIndex = GetHallIndex(GetRoomIndex(amphipod));
                f += (Math.Abs(targetHallIndex - i) + 1) * Cost(amphipod);
            }

            return f;
        }

        private int Cost(char amphipod)
        {
            switch (amphipod)
            {
                case 'A': return 1;
                case 'B': return 10;
                case 'C': return 100;
                case 'D': return 1000;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        internal string Hall { get; set; }

        internal string Room { get; set; }

        /// <summary>
        /// Gets heuristic distance from start to target node.
        /// </summary>
        internal int G
        {
            get => this.F + this.H;
        }

        /// <summary>
        /// Gets or sets distance from start to current node.
        /// </summary>
        internal int H { get; set; }

        /// <summary>
        /// Gets or sets heuristic distance from current node to target node.
        /// </summary>
        internal int F { get; set; }

        /// <summary>
        /// Gets or sets the parent node.
        /// </summary>
        internal State Parent { get; set; } = null;

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is State other &&
                other.Hall.Equals(this.Hall) &&
                other.Room.Equals(this.Room);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Hall, Room);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            var str = new StringBuilder();
            str.AppendLine(Hall);
            for (int i = 0; i < this.Roomsize; i++)
            {
                str.AppendLine($"  {Room[i]} {Room[Roomsize + i]} {Room[2 * Roomsize + i]} {Room[3 * Roomsize + i]}");
            }
            str.AppendLine($"G:{G}-H:{H}-F:{F}");
            return str.ToString();
        }

        public IEnumerable<State> GetChildren()
        {
            for (int i = 0; i < Room.Length; i++)
            {
                char amphipod = Room[i];
                if (amphipod == '.')
                {
                    continue;
                }

                if (i % Roomsize != 0 && Room[i - 1] != '.')
                {
                    continue;
                }

                var hallIndex = GetHallIndex(i);
                var isLeftBlocked = false;
                var isRightBlocked = false;

                if (Hall[hallIndex] != '.') continue;

                if (i % 2 == 1 && GetRoomIndex(amphipod) == i - 1)
                {
                    continue;
                }

                for (var s = 0; s < Hall.Length && !(isLeftBlocked && isRightBlocked); ++s)
                {
                    var h = hallIndex - 1 - s;
                    State child;
                    if (h >= 0 && !BlockedPlace(h))
                    {
                        if (Hall[h] != '.') isLeftBlocked = true;
                        if (!isLeftBlocked)
                        {
                            child = GenerateChild(i, amphipod, h);
                            child.H = this.H + ((s + 2 + (i % Roomsize)) * Cost(amphipod));
                            yield return child;
                        }
                    }

                    h = hallIndex + 1 + s;
                    if (h >= Hall.Length) continue;
                    if (BlockedPlace(h)) continue;
                    if (Hall[h] != '.') isRightBlocked = true;
                    if (isRightBlocked) continue;

                    child = GenerateChild(i, amphipod, h);
                    child.H = this.H + ((s + 2 + (i % Roomsize)) * Cost(amphipod));
                    yield return child;
                }
            }

            for (int i = 0; i < Hall.Length; ++i)
            {
                var amphipod = Hall[i];
                if (amphipod == '.') continue;
                int height;
                int roomIndex = GetRoomIndex(amphipod);
                int roomHallIndex = GetHallIndex(roomIndex);
                if (RoomIsValid(roomIndex, amphipod, out height) && PathIsValid(roomHallIndex, i))
                {
                    var hall = new string(Hall).ToCharArray();
                    var room = new string(Room).ToCharArray();
                    hall[i] = '.';
                    room[roomIndex + height] = amphipod;
                    yield return new State(new string(hall), new string(room), Roomsize, Target)
                    {
                        Parent = this,
                        H = this.H + ((Math.Abs(roomHallIndex - i) + height + 1) * Cost(amphipod)),
                    };
                }
            }
        }

        private bool BlockedPlace(int h)
        {
            return h == 2 || h == 4 || h == 6 || h == 8;
        }

        private State GenerateChild(int roomIndex, char amphipod, int hallIndex)
        {
            var hall = new string(Hall).ToCharArray();
            var room = new string(Room).ToCharArray();
            hall[hallIndex] = amphipod;
            room[roomIndex] = '.';
            var child = new State(new string(hall), new string(room), Roomsize, Target)
            {
                Parent = this,
            };
            return child;
        }

        private int GetRoomIndex(char amphipod)
        {
            switch (amphipod)
            {
                case 'A': return 0;
                case 'B': return Roomsize;
                case 'C': return 2 * Roomsize;
                case 'D': return 3 * Roomsize;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool PathIsValid(int targetHallIndex, int hallIndex)
        {
            var from = Math.Min(hallIndex, targetHallIndex);
            var to = Math.Max(hallIndex, targetHallIndex);
            for (int j = from; j < to; ++j)
            {
                if (j == hallIndex) continue;
                if (Hall[j] != '.') return false;
            }

            return true;
        }

        private bool RoomIsValid(int index, char amphipod, out int h)
        {
            var room = Room.AsSpan().Slice(index, Roomsize);
            h = -1;
            foreach (var c in room)
            {
                if (c == '.')
                {
                    ++h;
                    continue;
                }

                if (c == amphipod)
                {
                    continue;
                }
                return false;
            }

            return true;
        }

        private int GetHallIndex(int i)
        {
            if (i < Roomsize)
            {
                return 2;
            }
            if (i < 2 * Roomsize)
            {
                return 4;
            }
            if (i < 3 * Roomsize)
            {
                return 6;
            }
            if (i < 4 * Roomsize)
            {
                return 8;
            }
            throw new ArgumentOutOfRangeException();
        }
    }
}
