namespace AdventOfCode.Solutions.Event2021.Day4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 4 of event.
    /// </summary>
    public class Day4 : IAdventOfCodeDayRunner
    {

        /// <inheritdoc/>
        public bool HasVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var order = values[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(v => int.Parse(v)).ToArray();
            var numbers = values.Skip(1).SelectMany(l => l.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)).Select(v => int.Parse(v)).ToArray();
            var subscribtions = new Dictionary<int, List<Board>>();

            for (var i = 0; i < numbers.Length / 25; ++i)
            {
                var board = new Board();
                for (var row = 0; row < 5; row++)
                {
                    for (var col = 0; col < 5; col++)
                    {
                        int number = numbers[i * 25 + 5 * row + col];
                        board.Init(number, row, col);
                        if (!subscribtions.TryGetValue(number, out var boards))
                        {
                            boards = new List<Board>();
                            subscribtions[number] = boards;
                        }
                        boards.Add(board);
                    }
                }
            }

            int winBoardScore = FindWinner(order, subscribtions);

            yield return winBoardScore.ToString();
        }

        private static int FindWinner(int[] order, Dictionary<int, List<Board>> subscribtions)
        {
            foreach (var number in order)
            {
                if (!subscribtions.ContainsKey(number))
                {
                    continue;
                }
                foreach (var board in subscribtions[number])
                {
                    if (board.CheckWin(number))
                    {
                        return board.Score * number;
                    }
                }
            }

            return 0;
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var order = values[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(v => int.Parse(v)).ToArray();
            var numbers = values.Skip(1).SelectMany(l => l.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)).Select(v => int.Parse(v)).ToArray();
            var subscribtions = new Dictionary<int, List<Board>>();

            for (var i = 0; i < numbers.Length / 25; ++i)
            {
                var board = new Board();
                for (var row = 0; row < 5; row++)
                {
                    for (var col = 0; col < 5; col++)
                    {
                        int number = numbers[i * 25 + 5 * row + col];
                        board.Init(number, row, col);
                        if (!subscribtions.TryGetValue(number, out var boards))
                        {
                            boards = new List<Board>();
                            subscribtions[number] = boards;
                        }
                        boards.Add(board);
                    }
                }
            }

            var lastScore = 0;
            foreach (var number in order)
            {
                if (!subscribtions.ContainsKey(number))
                {
                    continue;
                }
                foreach (var board in subscribtions[number])
                {
                    if (!board.IsWon && board.CheckWin(number))
                    {
                        lastScore = board.Score * number;
                    }
                }
            }

            yield return lastScore.ToString();
        }
    }
}
