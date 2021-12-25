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
    public class Day22 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HasVisualization() => true;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var regexp = new Regex(@"(?<mode>\S{2,3}) x=(?<xmin>\S+)\.\.(?<xmax>\S+),y=(?<ymin>\S+)\.\.(?<ymax>\S+),z=(?<zmin>\S+)\.\.(?<zmax>\S+)");

            var rules = new List<(bool on, int xmin, int xmax, int ymin, int ymax, int zmin, int zmax)>();
            foreach (var value in values)
            {
                var match = regexp.Match(value);

                var xmin = int.Parse(match.Groups["xmin"].Value);
                var xmax = int.Parse(match.Groups["xmax"].Value);
                var ymin = int.Parse(match.Groups["ymin"].Value);
                var ymax = int.Parse(match.Groups["ymax"].Value);
                var zmin = int.Parse(match.Groups["zmin"].Value);
                var zmax = int.Parse(match.Groups["zmax"].Value);
                var on = match.Groups["mode"].Value == "on";
                rules.Add((on, xmin, xmax, ymin, ymax, zmin, zmax));
            }

            var activeCubes = new List<(int xmin, int xmax, int ymin, int ymax, int zmin, int zmax)>();
            foreach (var rule in rules)
            {
                (bool on, int xmin, int xmax, int ymin, int ymax, int zmin, int zmax) = rule;
                if (xmin < -50 || xmax > 50 || ymin < -50 || ymax > 50 || zmin < -50 || zmax > 50) continue;

                var nextActiveCubes = new List<(int xmin, int xmax, int ymin, int ymax, int zmin, int zmax)>();
                foreach (var activeCube in activeCubes)
                {
                    foreach (var cube in Intersect(activeCube, rule))
                    {
                        if (cube != null) nextActiveCubes.Add(cube.Value);
                    }
                }
                if (on) nextActiveCubes.Add((xmin, xmax, ymin, ymax, zmin, zmax));
                activeCubes = nextActiveCubes;
            }

            var sum = activeCubes.Sum(v => (v.xmax - v.xmin + 1) * (v.ymax - v.ymin + 1) * (v.zmax - v.zmin + 1));

            yield return sum.ToString();
        }

        private IEnumerable<(int xmin, int xmax, int ymin, int ymax, int zmin, int zmax)?> Intersect(
            (int xmin, int xmax, int ymin, int ymax, int zmin, int zmax) activeCube,
            (bool on, int xmin, int xmax, int ymin, int ymax, int zmin, int zmax) rule)
        {
            var xmin = Math.Max(activeCube.xmin, rule.xmin);
            var xmax = Math.Min(activeCube.xmax, rule.xmax);
            var ymin = Math.Max(activeCube.ymin, rule.ymin);
            var ymax = Math.Min(activeCube.ymax, rule.ymax);
            var zmin = Math.Max(activeCube.zmin, rule.zmin);
            var zmax = Math.Min(activeCube.zmax, rule.zmax);

            if (ValidCube(xmin, xmax, ymin, ymax, zmin, zmax) == null)
            {
                yield return activeCube;
                yield break;
            }

            yield return ValidCube(activeCube.xmin, activeCube.xmax, activeCube.ymin, activeCube.ymax, activeCube.zmin, zmin - 1);
            yield return ValidCube(activeCube.xmin, activeCube.xmax, activeCube.ymin, activeCube.ymax, zmax + 1, activeCube.zmax);

            yield return ValidCube(activeCube.xmin, xmin - 1, activeCube.ymin, activeCube.ymax, zmin, zmax);
            yield return ValidCube(xmax + 1, activeCube.xmax, activeCube.ymin, activeCube.ymax, zmin, zmax);
            yield return ValidCube(xmin, xmax, activeCube.ymin, ymin - 1, zmin, zmax);
            yield return ValidCube(xmin, xmax, ymax + 1, activeCube.ymax, zmin, zmax);
        }

        private (int xmin, int xmax, int ymin, int ymax, int zmin, int zmax)? ValidCube(int xmin, int xmax, int ymin, int ymax, int zmin, int zmax)
        {
            if (xmin <= xmax && ymin <= ymax && zmin <= zmax)
            {
                return (xmin, xmax, ymin, ymax, zmin, zmax);
            }
            return null;
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var regexp = new Regex(@"(?<mode>\S{2,3}) x=(?<xmin>\S+)\.\.(?<xmax>\S+),y=(?<ymin>\S+)\.\.(?<ymax>\S+),z=(?<zmin>\S+)\.\.(?<zmax>\S+)");

            var rules = new List<(bool on, int xmin, int xmax, int ymin, int ymax, int zmin, int zmax)>();
            foreach (var value in values)
            {
                var match = regexp.Match(value);

                var xmin = int.Parse(match.Groups["xmin"].Value);
                var xmax = int.Parse(match.Groups["xmax"].Value);
                var ymin = int.Parse(match.Groups["ymin"].Value);
                var ymax = int.Parse(match.Groups["ymax"].Value);
                var zmin = int.Parse(match.Groups["zmin"].Value);
                var zmax = int.Parse(match.Groups["zmax"].Value);
                var on = match.Groups["mode"].Value == "on";
                rules.Add((on, xmin, xmax, ymin, ymax, zmin, zmax));
            }

            var activeCubes = new List<(int xmin, int xmax, int ymin, int ymax, int zmin, int zmax)>();
            foreach (var rule in rules)
            {
                (bool on, int xmin, int xmax, int ymin, int ymax, int zmin, int zmax) = rule;

                var nextActiveCubes = new List<(int xmin, int xmax, int ymin, int ymax, int zmin, int zmax)>();
                foreach (var activeCube in activeCubes)
                {
                    foreach (var cube in Intersect(activeCube, rule))
                    {
                        if (cube != null) nextActiveCubes.Add(cube.Value);
                    }
                }
                if (on) nextActiveCubes.Add((xmin, xmax, ymin, ymax, zmin, zmax));
                activeCubes = nextActiveCubes;
            }

            long sum = activeCubes.Sum(v => (long)(v.xmax - v.xmin + 1) * (long)(v.ymax - v.ymin + 1) * (long)(v.zmax - v.zmin + 1));

            yield return sum.ToString();
        }
    }
}
