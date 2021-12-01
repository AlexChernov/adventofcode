namespace AdventOfCode.Solutions.Event2020.Day8
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 19 of event.
    /// </summary>
    public class Day8 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HasVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
            var prev = Init(values);

            var result = 0L;

            for (int i = 25; i < values.Length; ++i)
            {
                if (!CheckSum(prev, values[i]))
                {
                    result = values[i];
                    break;
                }
                UpdatePrev(prev, values[i - 25], values[i]);
            }

            yield return result.ToString();
        }

        private void UpdatePrev(Dictionary<long, int> prev, long toRemove, long toAdd)
        {
            AddOrIncrement(prev, toAdd);

            if (prev[toRemove] == 1)
            {
                prev.Remove(toRemove);
            }
            else
            {
                prev[toRemove] -= 1;
            }
        }

        private bool CheckSum(Dictionary<long, int> prev, long v)
        {
            foreach (var k in prev.Keys)
            {
                if (prev.ContainsKey(v - k))
                {
                    return true;
                }
            }

            return false;
        }

        private static Dictionary<long, int> Init(long[] values)
        {
            var prev = new Dictionary<long, int>();
            for (int i = 0; i < 25; ++i)
            {
                AddOrIncrement(prev, values[i]);
            }

            return prev;
        }

        private static void AddOrIncrement(Dictionary<long, int> prev, long value)
        {
            if (!prev.ContainsKey(value))
            {
                prev.Add(value, 0);
            }

            prev[value] += 1;
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
            var prev = Init(values);

            var result = 0L;

            for (int i = 25; i < values.Length; ++i)
            {
                if (!CheckSum(prev, values[i]))
                {
                    result = values[i];
                    break;
                }
                UpdatePrev(prev, values[i - 25], values[i]);
            }

            var low = 0;
            var high = 1;
            var sum = values[low] + values[high];

            while (high < values.Length && low < high)
            {
                if (sum == result)
                {
                    break;
                }

                if (sum < result)
                {
                    ++high;
                    sum += values[high];
                }
                else
                {
                    sum -= values[low];
                    ++low;
                }
            }

            var lowv = values[low];
            var highv = values[high];
            for (int i = low; i <= high; ++i)
            {
                if (values[i] < lowv)
                {
                    lowv = values[i];
                }
                if (highv < values[i])
                {
                    highv = values[i];
                }
            }

            yield return (lowv + highv).ToString();
        }
    }
}
