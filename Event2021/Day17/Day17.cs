namespace AdventOfCode.Solutions.Event2021
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 14 of event.
    /// </summary>
    public class Day17 : IAdventOfCodeDayRunner
    {

        /// <inheritdoc/>
        public bool HasVisualization() => true;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var regexp = new Regex(@"target area: x=(?<xmin>\S+)\.\.(?<xmax>\S+), y=(?<ymin>\S+)..(?<ymax>\S+)");

            var match = regexp.Match(values[0]);

            var xmin = int.Parse(match.Groups["xmin"].Value);
            var xmax = int.Parse(match.Groups["xmax"].Value);
            var ymin = int.Parse(match.Groups["ymin"].Value);
            var ymax = int.Parse(match.Groups["ymax"].Value);

            yield return 0.ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var regexp = new Regex(@"target area: x=(?<xmin>\S+)\.\.(?<xmax>\S+), y=(?<ymin>\S+)\.\.(?<ymax>\S+)");

            var match = regexp.Match(values[0]);

            var xmin = int.Parse(match.Groups["xmin"].Value);
            var xmax = int.Parse(match.Groups["xmax"].Value);
            var ymin = int.Parse(match.Groups["ymin"].Value);
            var ymax = int.Parse(match.Groups["ymax"].Value);

            var starts = new HashSet<(int, int)>();

            var corner = Init(xmin, xmax);

            foreach (var (step, ys) in GetY(ymin, ymax))
            {
                var xs = GetX(xmin, xmax, step, corner);

                foreach (var y in ys)
                {
                    foreach (var x in xs)
                    {
                        starts.Add((x, y));
                    }
                }
            }
            yield return starts.Count.ToString();
        }

        private (int, List<int>) Init(int xmin, int xmax)
        {
            var t = 0;
            var i = 0;
            var xs = new List<int>();

            while (t < xmax)
            {
                if (t > xmin) xs.Add(i);
                t += ++i;
            }
            return (i, xs);
        }

        private List<int> GetX(int xmin, int xmax, int step, (int step, List<int> xs) corner)
        {
            if (step >= corner.step - 1) return corner.xs;

            var target = (xmin / step) * step;
            var offset = step % 2 == 0 ? step / 2 : 0;
            target += offset;
            if (target < xmin) target += step;

            var lst = new List<int>();
            while (target <= xmax)
            {
                var v = target / step + (step - 1) / 2 + (step % 2 == 0 ? 1 : 0);
                target += step;
                if (v > 0)
                {
                    lst.Add(v);
                }
            }
            return lst;
        }

        private IEnumerable<(int step, List<int> lst)> GetY(int ymin, int ymax)
        {
            var maxUnderWater = 17; // TODO n*n-n+minY=0

            for (int step = 1; step < maxUnderWater; ++step)
            {
                var target = (ymin / step) * step;
                var offset = step % 2 == 0 ? step / 2 : 0;
                target += offset - step;
                if (ymin > target) target += step;

                var lst = new List<int>();
                while (target <= ymax)
                {
                    var v = target / step + (step - 1) / 2;
                    target += step;
                    if (v <= 0)
                    {
                        lst.Add(v);
                        if (v != 0) yield return (step: -v * 2 + step, new List<int>() { -v - 1 });
                    }
                }
                if (lst.Count > 0)
                {
                    yield return (step, lst);
                }
            }
        }
    }
}
