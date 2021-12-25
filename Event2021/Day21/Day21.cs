namespace AdventOfCode.Solutions.Event2021
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 21 of event.
    /// </summary>
    public class Day21 : IAdventOfCodeDayRunner
    {
        private Dictionary<(int, int, int, int, bool), (long, long)> Cache;

        /// <inheritdoc/>
        public bool HasVisualization() => true;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            var score1 = 8;
            var score2 = 4;
            var offset = 6;
            var total1 = 0;
            var total2 = 0;
            var isFirst = true;
            var turn = 0;
            while (total1 < 1000 && total2 < 1000)
            {
                if (isFirst)
                {
                    score1 += offset;
                    score1 = score1 % 10;
                    score1 = score1 == 0 ? 10 : score1;
                    total1 += score1;
                }
                else
                {
                    score2 += offset;
                    score2 = score2 % 10;
                    score2 = (score2 == 0) ? 10 : score2;
                    total2 += score2;
                }
                offset = --offset < 0 ? 9 : offset;
                isFirst = !isFirst;
                ++turn;
            }

            yield return ((turn * 3) * Math.Min(total1, total2)).ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            var score1 = 8;
            var score2 = 4;

            Cache = new Dictionary<(int, int, int, int, bool), (long, long)>();
            var (win1, win2) = QuantumDice(score1, score2, 21, 21, true);


            yield return Math.Max(win1, win2).ToString();
        }

        private (long win1, long win2) QuantumDice(int score1, int score2, int totalLeft1, int totalLeft2, bool isFirst)
        {
            if (Cache.TryGetValue((score1, score2, totalLeft1, totalLeft2, isFirst), out var ret))
            {
                return ret;
            }

            if (isFirst)
            {
                var nextScores = NextScores(score1).OrderBy(((int score, int number) x) => x.score).ToArray<(int score, int number)>();
                long win1 = 0;
                long win2 = 0;
                var autowinLeft = 27;
                int index = 0;
                for (index = 0; index < nextScores.Length; index++)
                {
                    var nextScore = nextScores[index];

                    if (totalLeft1 <= nextScore.score)
                    {
                        win1 += autowinLeft;
                        break;
                    }
                    autowinLeft -= nextScore.number;
                    var (twin1, twin2) = QuantumDice(nextScore.score, score2, totalLeft1 - nextScore.score, totalLeft2, !isFirst);
                    win1 += twin1 * nextScore.number;
                    win2 += twin2 * nextScore.number;
                }

                Cache.TryAdd((score1, score2, totalLeft1, totalLeft2, isFirst), (win1, win2));

                return (win1, win2);
            }
            else
            {
                var nextScores = NextScores(score2).OrderBy(((int score, int number) x) => x.score).ToArray<(int score, int number)>();
                long win1 = 0;
                long win2 = 0;
                var autowinLeft = 27;
                int index = 0;
                for (index = 0; index < nextScores.Length; index++)
                {
                    var nextScore = nextScores[index];

                    if (totalLeft2 <= nextScore.score)
                    {
                        win2 += autowinLeft;
                        break;
                    }
                    autowinLeft -= nextScore.number;
                    var (twin1, twin2) = QuantumDice(score1, nextScore.score, totalLeft1, totalLeft2 - nextScore.score, !isFirst);
                    win1 += twin1 * nextScore.number;
                    win2 += twin2 * nextScore.number;
                }

                Cache.TryAdd((score1, score2, totalLeft1, totalLeft2, isFirst), (win1, win2));

                return (win1, win2);
            }
        }

        private IEnumerable<(int, int)> NextScores(int _score)
        {
            (int score, int number)[] dices = new[]
            {
                (3,1), (4,3), (5,6), (6,7), (7,6), (8,3), (9,1),
            };

            foreach (var dice in dices)
            {
                var score = _score;
                score += dice.score;
                score = score % 10;
                score = score == 0 ? 10 : score;
                yield return (score, dice.number);
            }
        }
    }
}
