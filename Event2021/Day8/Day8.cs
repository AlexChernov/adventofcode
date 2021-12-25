namespace AdventOfCode.Solutions.Event2021.Day8
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 8 of event.
    /// </summary>
    public class Day8 : IAdventOfCodeDayRunner
    {

        /// <inheritdoc/>
        public bool HasVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(SplitValues);
            var ret = values.SelectMany(values => values.Where(v => v.Length == 2 || v.Length == 3 || v.Length == 4 || v.Length == 7)).Count();

            yield return ret.ToString();
        }

        private string[] SplitValues(string value, int _)
        {
            return value.Substring(value.IndexOf('|')).Split(new char[] { ' ', }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(GetEntries);

            var sum = 0;
            foreach (var entry in values)
            {
                var map = InitMap(entry.values);
                var controlValue = Decode(entry.control, map);
                sum += controlValue;
            }

            yield return sum.ToString();
        }

        private int Decode(IEnumerable<HashSet<char>> control, Dictionary<string, int> map)
        {
            var value = 0;
            foreach (var entry in control)
            {
                var str = string.Concat(entry.OrderBy(ch => ch));
                value *= 10;
                value += map[str];
            }
            return value;
        }

        private Dictionary<string, int> InitMap(IEnumerable<HashSet<char>> values)
        {
            var numbers235 = values.Where(v => v.Count == 5).ToArray();
            var adg = numbers235.First();
            foreach (var value in numbers235)
            {
                adg = adg.Intersect(value).ToHashSet();
            }
            var number7acf = values.Single(v => v.Count == 3);
            var a = number7acf.Intersect(adg).Single();
            var number4bcdf = values.Single(v => v.Count == 4);
            var d = number4bcdf.Intersect(adg).Single();
            var g = adg.Single(c => c != a && c != d);
            var numbers069 = values.Where(v => v.Count == 6).ToArray();
            var abfg = numbers069.First();
            foreach (var value in numbers069)
            {
                abfg = abfg.Intersect(value).ToHashSet();
            }
            var f = number7acf.Intersect(abfg).Single(c => c != a);
            var c = number7acf.Single(c => c != a && c != f);
            var b = number4bcdf.Single(ch => ch != c && ch != d && ch != f);

            var allKnown = new[] { a, b, c, d, f, g }.ToHashSet();

            var e = "abcdefg".Single(ch => !allKnown.Contains(ch));

            var numberMap = new Dictionary<string, int>();
            var number0 = string.Concat(new[] { a, b, c, e, f, g }.OrderBy(ch => ch));
            var number1 = string.Concat(new[] { c, f }.OrderBy(ch => ch));
            var number2 = string.Concat(new[] { a, c, d, e, g }.OrderBy(ch => ch));
            var number3 = string.Concat(new[] { a, c, d, f, g }.OrderBy(ch => ch));
            var number4 = string.Concat(new[] { b, c, d, f }.OrderBy(ch => ch));
            var number5 = string.Concat(new[] { a, b, d, f, g }.OrderBy(ch => ch));
            var number6 = string.Concat(new[] { a, b, d, e, f, g }.OrderBy(ch => ch));
            var number7 = string.Concat(new[] { a, c, f }.OrderBy(ch => ch));
            var number8 = string.Concat(new[] { a, b, c, d, e, f, g }.OrderBy(ch => ch));
            var number9 = string.Concat(new[] { a, b, c, d, f, g }.OrderBy(ch => ch));

            numberMap.Add(number0, 0);
            numberMap.Add(number1, 1);
            numberMap.Add(number2, 2);
            numberMap.Add(number3, 3);
            numberMap.Add(number4, 4);
            numberMap.Add(number5, 5);
            numberMap.Add(number6, 6);
            numberMap.Add(number7, 7);
            numberMap.Add(number8, 8);
            numberMap.Add(number9, 9);

            return numberMap;
        }

        private (IEnumerable<HashSet<char>> values, IEnumerable<HashSet<char>> control) GetEntries(string input, int _)
        {
            var t = input.Split(new char[] { '|', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var value = t[0].Split(new char[] { ' ', }, StringSplitOptions.RemoveEmptyEntries).Select(str => str.ToHashSet()).ToArray();
            var control = t[1].Split(new char[] { ' ', }, StringSplitOptions.RemoveEmptyEntries).Select(str => str.ToHashSet()).ToArray();

            return (values: value, control: control);
        }
    }
}
