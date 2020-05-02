using System;
using System.Linq;

namespace AdventOfCode.Solutions.Task2016_20
{
    public class Task2016_20
    {
        public static string Run1(string input)
        {
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            var ranges = lines
                .Select(l =>
                {
                    var separatorIndex = l.IndexOf('-');

                    if (separatorIndex == -1)
                    {
                        return null;
                    }

                    var left = l.Substring(0, separatorIndex);
                    var right = l.Substring(separatorIndex + 1);
                    long leftValue, rightValue;
                    if (!ValuesValid(left, right, out leftValue, out rightValue))
                    {
                        return null;
                    }

                    if (leftValue < rightValue)
                    {
                        return new Tuple<long, long>(leftValue, rightValue);
                    }
                    else
                    {
                        return new Tuple<long, long>(rightValue, leftValue);
                    }
                })
                .Where(t => t != null)
                .OrderBy(t => t.Item1)
                .ThenBy(t => t.Item2);

            long min = 0;

            foreach (var range in ranges)
            {
                if (range.Item1 > min)
                {
                    break;
                }
                min = range.Item2 + 1;
            }

            return min.ToString() + " is minimum allowerd IP.";
        }

        public static string Run2(string input)
        {
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            var ranges = lines
                .Select(l =>
                {
                    var separatorIndex = l.IndexOf('-');

                    if (separatorIndex == -1)
                    {
                        return null;
                    }

                    var left = l.Substring(0, separatorIndex);
                    var right = l.Substring(separatorIndex + 1);
                    long leftValue, rightValue;
                    if (!ValuesValid(left, right, out leftValue, out rightValue))
                    {
                        return null;
                    }

                    if (leftValue < rightValue)
                    {
                        return new Tuple<long, long>(leftValue, rightValue);
                    }
                    else
                    {
                        return new Tuple<long, long>(rightValue, leftValue);
                    }
                })
                .Where(t => t != null)
                .OrderBy(t => t.Item1)
                .ThenBy(t => t.Item2);

            long lastValid = 0;
            long count = 0;

            foreach (var range in ranges)
            {
                if (range.Item1 > lastValid)
                {
                    count += range.Item1 - lastValid;
                }
                if (range.Item2 >= lastValid)
                {
                    lastValid = range.Item2 + 1;
                }
            }
            count += 4294967295 - lastValid + 1;
            return count.ToString() + " are allowed by the blacklist.";
        }

        private static bool ValuesValid(string left, string right, out long leftValue, out long rightValue)
        {
            var leftIsValid = Int64.TryParse(left, out leftValue);
            var rightIsValid = Int64.TryParse(right, out rightValue);

            return leftIsValid && rightIsValid;
        }
    }
}
