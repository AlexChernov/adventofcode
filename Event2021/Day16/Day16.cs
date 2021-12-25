namespace AdventOfCode.Solutions.Event2021
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 16 of event.
    /// </summary>
    public class Day16 : IAdventOfCodeDayRunner
    {

        /// <inheritdoc/>
        public bool HasVisualization() => true;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var barr = new List<int>(input.Length * 4);
            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '0': barr.AddRange(new[] { 0, 0, 0, 0 }); break;
                    case '1': barr.AddRange(new[] { 0, 0, 0, 1 }); break;
                    case '2': barr.AddRange(new[] { 0, 0, 1, 0 }); break;
                    case '3': barr.AddRange(new[] { 0, 0, 1, 1 }); break;
                    case '4': barr.AddRange(new[] { 0, 1, 0, 0 }); break;
                    case '5': barr.AddRange(new[] { 0, 1, 0, 1 }); break;
                    case '6': barr.AddRange(new[] { 0, 1, 1, 0 }); break;
                    case '7': barr.AddRange(new[] { 0, 1, 1, 1 }); break;
                    case '8': barr.AddRange(new[] { 1, 0, 0, 0 }); break;
                    case '9': barr.AddRange(new[] { 1, 0, 0, 1 }); break;
                    case 'A': barr.AddRange(new[] { 1, 0, 1, 0 }); break;
                    case 'B': barr.AddRange(new[] { 1, 0, 1, 1 }); break;
                    case 'C': barr.AddRange(new[] { 1, 1, 0, 0 }); break;
                    case 'D': barr.AddRange(new[] { 1, 1, 0, 1 }); break;
                    case 'E': barr.AddRange(new[] { 1, 1, 1, 0 }); break;
                    case 'F': barr.AddRange(new[] { 1, 1, 1, 1 }); break;
                    default:
                        throw new Exception();
                }
            }

            var (versions, len) = VersionsSum(barr, 0);
            yield return versions.ToString();
        }

        private (int versions, int len) VersionsSum(List<int> barr, int startIndex)
        {
            var currentIndex = startIndex;
            var version = barr[currentIndex++] * 4 + barr[currentIndex++] * 2 + barr[currentIndex++];
            var type = barr[currentIndex++] * 4 + barr[currentIndex++] * 2 + barr[currentIndex++];
            if (type == 4)
            {
                var (value, len) = GetLiteral(barr, currentIndex);
                return (version, currentIndex + len - startIndex);
            }
            else
            {
                var lenType = barr[currentIndex++];
                if (lenType == 0)
                {
                    var (versions, len) = ReadVersionsOfTotalLen(barr, currentIndex);
                    return (versions + version, currentIndex + len - startIndex);
                }
                else
                {
                    var (versions, len) = ReadVersionsOfNumberOfPackets(barr, currentIndex);
                    return (versions + version, currentIndex + len - startIndex);
                }
            }
        }

        private (int versions, int len) ReadVersionsOfNumberOfPackets(List<int> barr, int startIndex)
        {
            var numberOfPackets = 0;
            var versionsSum = 0;
            var currentIndex = startIndex;
            for (int i = 0; i < 11; ++i)
            {
                numberOfPackets *= 2;
                numberOfPackets += barr[currentIndex++];
            }

            for (int i = 0; i < numberOfPackets; ++i)
            {
                var (versions, len) = VersionsSum(barr, currentIndex);
                versionsSum += versions;
                currentIndex += len;
            }

            return (versionsSum, currentIndex - startIndex);
        }

        private (int versions, int len) ReadVersionsOfTotalLen(List<int> barr, int startIndex)
        {
            var totalLen = 0;
            var versionsSum = 0;
            var currentIndex = startIndex;
            for (int i = 0; i < 15; ++i)
            {
                totalLen *= 2;
                totalLen += barr[currentIndex++];
            }

            var targetIndex = currentIndex + totalLen;
            while (currentIndex < targetIndex)
            {
                var (versions, len) = VersionsSum(barr, currentIndex);
                versionsSum += versions;
                currentIndex += len;
            }

            if (currentIndex != targetIndex)
            {
                throw new Exception();
            }

            return (versionsSum, currentIndex - startIndex);
        }

        private (long value, int len) GetLiteral(List<int> barr, int startIndex)
        {
            var currentIndex = startIndex;
            var value = 0L;
            while (barr[currentIndex++] == 1)
            {
                value *= 16;
                value += barr[currentIndex++] * 8 + barr[currentIndex++] * 4 + barr[currentIndex++] * 2 + barr[currentIndex++];
            }
            value *= 16;
            value += barr[currentIndex++] * 8 + barr[currentIndex++] * 4 + barr[currentIndex++] * 2 + barr[currentIndex++];
            return (value, currentIndex - startIndex);
        }


        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var barr = new List<int>(input.Length * 4);
            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '0': barr.AddRange(new[] { 0, 0, 0, 0 }); break;
                    case '1': barr.AddRange(new[] { 0, 0, 0, 1 }); break;
                    case '2': barr.AddRange(new[] { 0, 0, 1, 0 }); break;
                    case '3': barr.AddRange(new[] { 0, 0, 1, 1 }); break;
                    case '4': barr.AddRange(new[] { 0, 1, 0, 0 }); break;
                    case '5': barr.AddRange(new[] { 0, 1, 0, 1 }); break;
                    case '6': barr.AddRange(new[] { 0, 1, 1, 0 }); break;
                    case '7': barr.AddRange(new[] { 0, 1, 1, 1 }); break;
                    case '8': barr.AddRange(new[] { 1, 0, 0, 0 }); break;
                    case '9': barr.AddRange(new[] { 1, 0, 0, 1 }); break;
                    case 'A': barr.AddRange(new[] { 1, 0, 1, 0 }); break;
                    case 'B': barr.AddRange(new[] { 1, 0, 1, 1 }); break;
                    case 'C': barr.AddRange(new[] { 1, 1, 0, 0 }); break;
                    case 'D': barr.AddRange(new[] { 1, 1, 0, 1 }); break;
                    case 'E': barr.AddRange(new[] { 1, 1, 1, 0 }); break;
                    case 'F': barr.AddRange(new[] { 1, 1, 1, 1 }); break;
                    default:
                        throw new Exception();
                }
            }

            var (value, len) = GetValue(barr, 0);
            yield return value.ToString();
        }

        private (long value, int len) GetValue(List<int> barr, int startIndex)
        {
            var currentIndex = startIndex;
            var version = barr[currentIndex++] * 4 + barr[currentIndex++] * 2 + barr[currentIndex++];
            var type = barr[currentIndex++] * 4 + barr[currentIndex++] * 2 + barr[currentIndex++];
            if (type == 4)
            {
                var (value, len) = GetLiteral(barr, currentIndex);
                return (value, currentIndex + len - startIndex);
            }
            else
            {
                var lenType = barr[currentIndex++];
                var len = 0;
                List<long> values = null;
                if (lenType == 0)
                {
                    (values, len) = GetValuesTotalLen(barr, currentIndex);
                }
                else
                {
                    (values, len) = GetValuesOfNumberOfPackets(barr, currentIndex);
                }
                var value = CalcValue(type, values);

                return (value, currentIndex + len - startIndex);
            }
        }

        private long CalcValue(int type, List<long> values)
        {
            switch (type)
            {
                case 0: return values.Sum(x=>x);
                case 1: return values.Aggregate(1L, (mult, x)=>mult * x);
                case 2: return values.Min(x=>x);
                case 3: return values.Max(x=>x);
                case 5:
                    {
                        if (values.Count != 2) throw new ArgumentException();
                        return values[0] > values[1] ? 1 : 0;
                    }
                case 6:
                    {
                        if (values.Count != 2) throw new ArgumentException();
                        return values[0] < values[1] ? 1 : 0;
                    }
                case 7:
                    {
                        if (values.Count != 2) throw new ArgumentException();
                        return values[0] == values[1] ? 1 : 0;
                    }
                default:
                    throw new ArgumentException();
            }
        }

        private (List<long> values, int len) GetValuesOfNumberOfPackets(List<int> barr, int startIndex)
        {
            var numberOfPackets = 0;
            var values = new List<long>();
            var currentIndex = startIndex;
            for (int i = 0; i < 11; ++i)
            {
                numberOfPackets *= 2;
                numberOfPackets += barr[currentIndex++];
            }

            for (int i = 0; i < numberOfPackets; ++i)
            {
                var (value, len) = GetValue(barr, currentIndex);
                values.Add(value);
                currentIndex += len;
            }

            return (values, currentIndex - startIndex);
        }

        private (List<long> values, int len) GetValuesTotalLen(List<int> barr, int startIndex)
        {
            var totalLen = 0;
            var values = new List<long>();
            var currentIndex = startIndex;
            for (int i = 0; i < 15; ++i)
            {
                totalLen *= 2;
                totalLen += barr[currentIndex++];
            }

            var targetIndex = currentIndex + totalLen;
            while (currentIndex < targetIndex)
            {
                var (value, len) = GetValue(barr, currentIndex);
                values.Add(value);
                currentIndex += len;
            }

            if (currentIndex != targetIndex)
            {
                throw new Exception();
            }

            return (values, currentIndex - startIndex);
        }
    }
}
