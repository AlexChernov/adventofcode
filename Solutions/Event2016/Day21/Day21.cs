namespace AdventOfCode.Solutions.Event2016.Day21
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 21 of event.
    /// </summary>
    public class Day21 : IAdventOfCodeDayRunner
    {
        private static readonly Dictionary<int, int> DerotateOnLetterMap = new Dictionary<int, int>()
        {
            { 1, 0 },
            { 3, 1 },
            { 5, 2 },
            { 7, 3 },
            { 2, 4 },
            { 4, 5 },
            { 6, 6 },
            { 0, 7 },
        };

        /// <inheritdoc/>
        public bool HasVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var commands = InitCommands(lines);
            var state = new State { Word = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' } };

            foreach (var cmd in commands)
            {
                cmd(state);
            }

            yield return new string(state.Word);
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var commands = InitReverseCommands(lines);
            var state = new State { Word = "fbgdceah".ToCharArray() };

            for (int i = commands.Count - 1; i >= 0; --i)
            {
                commands[i](state);
            }

            yield return new string(state.Word);
        }

        private static IEnumerable<Action<State>> InitCommands(string[] lines)
        {
            return lines
                .Select<string, Action<State>>(l =>
                {
                    var pattern = @"swap position (?<X>\d+) with position (?<Y>\d+)";
                    var match = Regex.Match(l, pattern);
                    if (match.Success)
                    {
                        var x = int.Parse(match.Groups["X"].Value);
                        var y = int.Parse(match.Groups["Y"].Value);
                        return (State state) => Swap(state, x, y);
                    }

                    pattern = @"swap letter (?<X>\w) with letter (?<Y>\w)";
                    match = Regex.Match(l, pattern);
                    if (match.Success)
                    {
                        var x = match.Groups["X"].Value[0];
                        var y = match.Groups["Y"].Value[0];
                        return (State state) => Swap(state, x, y);
                    }

                    pattern = @"rotate left (?<X>\d+) step";
                    match = Regex.Match(l, pattern);
                    if (match.Success)
                    {
                        var x = int.Parse(match.Groups["X"].Value);
                        return (State state) => RotateLeft(state, x);
                    }

                    pattern = @"rotate right (?<X>\d+) step";
                    match = Regex.Match(l, pattern);
                    if (match.Success)
                    {
                        var x = int.Parse(match.Groups["X"].Value);
                        return (State state) => RotateRight(state, x);
                    }

                    pattern = @"rotate based on position of letter (?<X>\w)";
                    match = Regex.Match(l, pattern);
                    if (match.Success)
                    {
                        var x = match.Groups["X"].Value[0];
                        return (State state) => RotateOnLetter(state, x);
                    }

                    pattern = @"reverse positions (?<X>\d+) through (?<Y>\d+)";
                    match = Regex.Match(l, pattern);
                    if (match.Success)
                    {
                        var x = int.Parse(match.Groups["X"].Value);
                        var y = int.Parse(match.Groups["Y"].Value);
                        return (State state) => Reverse(state, x, y);
                    }

                    pattern = @"move position (?<X>\d+) to position (?<Y>\d+)";
                    match = Regex.Match(l, pattern);
                    if (match.Success)
                    {
                        var x = int.Parse(match.Groups["X"].Value);
                        var y = int.Parse(match.Groups["Y"].Value);
                        return (State state) => Move(state, x, y);
                    }

                    return (State state) => { };
                })
                .ToList();
        }

        private static List<Action<State>> InitReverseCommands(string[] lines)
        {
            return lines
                .Select<string, Action<State>>(l =>
                {
                    var pattern = @"swap position (?<X>\d+) with position (?<Y>\d+)";
                    var match = Regex.Match(l, pattern);
                    if (match.Success)
                    {
                        var x = int.Parse(match.Groups["X"].Value);
                        var y = int.Parse(match.Groups["Y"].Value);
                        return (State state) => Swap(state, x, y);
                    }

                    pattern = @"swap letter (?<X>\w) with letter (?<Y>\w)";
                    match = Regex.Match(l, pattern);
                    if (match.Success)
                    {
                        var x = match.Groups["X"].Value[0];
                        var y = match.Groups["Y"].Value[0];
                        return (State state) => Swap(state, x, y);
                    }

                    pattern = @"rotate left (?<X>\d+) step";
                    match = Regex.Match(l, pattern);
                    if (match.Success)
                    {
                        var x = int.Parse(match.Groups["X"].Value);
                        return (State state) => RotateRight(state, x);
                    }

                    pattern = @"rotate right (?<X>\d+) step";
                    match = Regex.Match(l, pattern);
                    if (match.Success)
                    {
                        var x = int.Parse(match.Groups["X"].Value);
                        return (State state) => RotateLeft(state, x);
                    }

                    pattern = @"rotate based on position of letter (?<X>\w)";
                    match = Regex.Match(l, pattern);
                    if (match.Success)
                    {
                        var x = match.Groups["X"].Value[0];
                        return (State state) => DerotateOnLetter(state, x);
                    }

                    pattern = @"reverse positions (?<X>\d+) through (?<Y>\d+)";
                    match = Regex.Match(l, pattern);
                    if (match.Success)
                    {
                        var x = int.Parse(match.Groups["X"].Value);
                        var y = int.Parse(match.Groups["Y"].Value);
                        return (State state) => Reverse(state, x, y);
                    }

                    pattern = @"move position (?<X>\d+) to position (?<Y>\d+)";
                    match = Regex.Match(l, pattern);
                    if (match.Success)
                    {
                        var x = int.Parse(match.Groups["X"].Value);
                        var y = int.Parse(match.Groups["Y"].Value);
                        return (State state) => Move(state, y, x);
                    }

                    return (State state) => { };
                })
                .ToList();
        }

        private static void DerotateOnLetter(State state, char x)
        {
            var index = IndexOf(state, x);
            var indexToBe = DerotateOnLetterMap[index];
            RotateRight(state, indexToBe - index);
        }

        private static void RotateRight(State state, int x)
        {
            RotateRight(state, x, 0, state.Word.Length - 1);
        }

        private static void Move(State state, int from, int to)
        {
            if (from < to)
            {
                RotateRight(state, -1, from, to);
            }
            else
            {
                RotateRight(state, 1, to, from);
            }
        }

        private static void Reverse(State state, int x, int y)
        {
            var len = (y - x + 1) / 2;
            for (int i = 0; i < len; ++i)
            {
                Swap(state, x + i, y - i);
            }
        }

        private static void RotateOnLetter(State state, char x)
        {
            var index = IndexOf(state, x);
            index += index >= 4 ? 2 : 1;
            RotateRight(state, index, 0, state.Word.Length - 1);
        }

        private static int IndexOf(State state, char x)
        {
            for (int i = 0; i < state.Word.Length; ++i)
            {
                var ch = state.Word[i];
                if (ch == x)
                {
                    return i;
                }
            }

            return -1;
        }

        private static void RotateRight(State state, int shift, int from, int to)
        {
            var currentChainStart = from;

            var currentIndex = from;
            var prevValue = state.Word[currentIndex];
            var len = to - from + 1;
            var nextIndex = CalcNextIndex(currentIndex, shift, from, len);
            if (currentIndex == nextIndex)
            {
                return;
            }

            for (int i = 0; i < len; ++i)
            {
                nextIndex = CalcNextIndex(currentIndex, shift, from, len);

                var temp = state.Word[nextIndex];
                state.Word[nextIndex] = prevValue;
                prevValue = temp;

                currentIndex = nextIndex;

                if (currentIndex == currentChainStart)
                {
                    ++currentChainStart;
                    currentIndex = currentChainStart;
                    prevValue = state.Word[currentIndex];
                }
            }
        }

        private static int CalcNextIndex(int index, int shift, int from, int len)
        {
            return from + Utils.Mod(index - from + shift, len);
        }

        private static void RotateLeft(State state, int x)
        {
            RotateRight(state, -x);
        }

        private static void Swap(State state, int x, int y)
        {
            var temp = state.Word[x];
            state.Word[x] = state.Word[y];
            state.Word[y] = temp;
        }

        private static void Swap(State state, char x, char y)
        {
            var xIndex = IndexOf(state, x);
            var yIndex = IndexOf(state, y);

            Swap(state, xIndex, yIndex);
        }
    }
}
