namespace AdventOfCode.Solutions.Event2021
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 25 of event.
    /// </summary>
    public class Day25 : IAdventOfCodeDayRunner
    {
        private Dictionary<(int, int, int, int, bool), (long, long)> Cache;

        /// <inheritdoc/>
        public bool HasVisualization() => true;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            var len = values.Length;
            var len2 = values[0].Length;
            var map = new char[values.Length, len2];

            for (int i = 0; i < values.Length; i++)
            {
                for (int j = 0; j < len2; j++)
                {
                    map[i, j] = values[i][j];
                }
            }

            var needChange = true;
            var step = 0;
            while (needChange)
            {
                needChange = false;
                var nextMap = new char[len, len2];

                for (int i = 0; i < len; i++)
                {
                    for (int j = 0; j < len2; j++)
                    {
                        switch (map[i, j])
                        {
                            case '>':
                                var nexti = i;
                                var nextj = j + 1;
                                if (!map.InBound(nexti, nextj))
                                {
                                    nextj = 0;
                                }

                                if (map[nexti, nextj] == '.')
                                {
                                    nextMap[nexti, nextj] = map[i, j];
                                    nextMap[i, j] = '.';
                                    needChange = true;
                                }
                                else
                                {
                                    nextMap[i, j] = map[i, j];
                                }
                                break;
                            case 'v':
                                nextMap[i, j] = map[i, j];
                                break;
                            case '.':
                                if (nextMap[i, j] != '>')
                                {
                                    nextMap[i, j] = map[i, j];
                                }
                                break;
                            default:
                                throw new Exception();
                        }
                    }
                }

                map = nextMap;
                nextMap = new char[len, len2];

                if (shouldVisualise)
                {
                    var str = new StringBuilder();
                    str.Append("Step: ");
                    str.AppendLine(step.ToString());
                    for (int i = 0; i < len; ++i)
                    {
                        for (int j = 0; j < len2; ++j)
                        {
                            str.Append(map[i, j]);
                        }
                        str.AppendLine();
                    }
                    yield return str.ToString();
                }

                for (int i = 0; i < len; i++)
                {
                    for (int j = 0; j < len2; j++)
                    {
                        switch (map[i, j])
                        {
                            case '>':
                                nextMap[i, j] = map[i, j];
                                break;
                            case 'v':
                                var nexti = i + 1;
                                var nextj = j;
                                if (!map.InBound(nexti, nextj))
                                {
                                    nexti = 0;
                                }

                                if (map[nexti, nextj] == '.')
                                {
                                    nextMap[nexti, nextj] = map[i, j];
                                    nextMap[i, j] = '.';
                                    needChange = true;
                                }
                                else
                                {
                                    nextMap[i, j] = map[i, j];
                                }
                                break;
                            case '.':
                                if (nextMap[i, j] != 'v')
                                {
                                    nextMap[i, j] = map[i, j];
                                }
                                break;
                            default:
                                throw new Exception();
                        }
                    }
                }
                map = nextMap;

                if (shouldVisualise)
                {
                    var str = new StringBuilder();
                    str.Append("Step: ");
                    str.AppendLine(step.ToString());
                    for (int i = 0; i < len; ++i)
                    {
                        for (int j = 0; j < len2; ++j)
                        {
                            str.Append(map[i, j]);
                        }
                        str.AppendLine();
                    }
                    yield return str.ToString();
                }

                step++;
            }

            if (!shouldVisualise) yield return $"Step: {step}";
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            yield return 0.ToString();
        }
    }
}
