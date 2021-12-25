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
    public class Day19 : IAdventOfCodeDayRunner
    {

        /// <inheritdoc/>
        public bool HasVisualization() => true;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var scanners = input.Split(new[] { "--- scanner" }, StringSplitOptions.RemoveEmptyEntries).Select(GetScanner).ToArray();

            var map = new Dictionary<
                int,
                List<(
                    int scnr,
                    (int beaconId, int otherBeaconId, List<long> hashes) intersect)>>();

            for (int i = 0; i < scanners.Length; i++)
            {
                for (int j = i + 1; j < scanners.Length; j++)
                {
                    if (scanners[i].TryFindIntersect(scanners[j], out var intersect))
                    {
                        var nextForI = map.GetValueOrDefault(i, new List<(int scnr, (int beaconId, int otherBeaconId, List<long> hashes) intersect)>());
                        nextForI.Add((j, intersect));
                        if (nextForI.Count == 1) map.Add(i, nextForI);
                        var nextForJ = map.GetValueOrDefault(j, new List<(int scnr, (int beaconId, int otherBeaconId, List<long> hashes) intersect)>());
                        nextForJ.Add((i, (intersect.otherBeaconId, intersect.beaconId, intersect.hashes)));
                        if (nextForJ.Count == 1) map.Add(j, nextForJ);
                    }
                }
            }

            var open = new HashSet<int>();
            var close = new HashSet<int>();
            open.Add(0);
            scanners[0].Transform = (v) => v;
            var all = new HashSet<(int, int, int)>();

            while (open.Count > 0)
            {
                var nextOpen = new HashSet<int>();
                foreach (var current in open)
                {
                    var currentScnr = scanners[current];
                    for (var i = 0; i < currentScnr.Beacons.Count; i++)
                    {
                        var t = currentScnr.Transform(currentScnr.Beacons[i]);
                        currentScnr.Beacons[i] = t;
                        all.Add(t);
                    }
                    //if (current != 0) Validate(currentScnr, map[current].Select(x => scanners[x.scnr]).Where(x => x.Transform != null).ToArray());

                    foreach (var next in map[current])
                    {
                        var nextScnrId = next.scnr;
                        if (close.Contains(nextScnrId) || open.Contains(nextScnrId)) continue;

                        var intersect = next.intersect;
                        var nextScnr = scanners[nextScnrId];
                        var x0 = currentScnr.Beacons[intersect.beaconId];
                        var x1Id = currentScnr.RelativeHashToIdMap[intersect.beaconId][intersect.hashes[0]];
                        var x1 = currentScnr.Beacons[x1Id];

                        nextScnr.CalcTranform(intersect.otherBeaconId, intersect.hashes[0], x0, x1);

                        var newValues = intersect.hashes
                            .Select(h => nextScnr.Beacons[nextScnr.RelativeHashToIdMap[intersect.otherBeaconId][h]])
                            .Select(nextScnr.Transform)
                            .ToArray();
                        var intr = currentScnr.Beacons.Intersect(newValues).ToArray();
                        if (intr.Length == 11)
                        {
                            nextOpen.Add(nextScnrId);
                        }
                        else
                        {
                            nextScnr.Transform = null;
                        }
                    }
                    close.Add(current);
                }
                open = nextOpen;
            }

            yield return all.Count.ToString();
        }

        private void Validate(Scanner currentScnr, Scanner[] scanners)
        {
            if (scanners.Length == 0) throw new Exception();
            foreach (var scnr in scanners)
            {
                if (currentScnr.Beacons.Intersect(scnr.Beacons).Count() != 12)
                {
                    throw new Exception();
                }
            }
        }

        private Scanner GetScanner(string src)
        {
            var values = src.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var beacons = new List<(int x, int y, int z)>();
            foreach (var value in values.Skip(1))
            {
                var coords = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                beacons.Add((coords[0], coords[1], coords[2]));
            }
            return new Scanner(beacons);
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var scanners = input.Split(new[] { "--- scanner" }, StringSplitOptions.RemoveEmptyEntries).Select(GetScanner).ToArray();

            var map = new Dictionary<
                int,
                List<(
                    int scnr,
                    (int beaconId, int otherBeaconId, List<long> hashes) intersect)>>();

            for (int i = 0; i < scanners.Length; i++)
            {
                for (int j = i + 1; j < scanners.Length; j++)
                {
                    if (scanners[i].TryFindIntersect(scanners[j], out var intersect))
                    {
                        var nextForI = map.GetValueOrDefault(i, new List<(int scnr, (int beaconId, int otherBeaconId, List<long> hashes) intersect)>());
                        nextForI.Add((j, intersect));
                        if (nextForI.Count == 1) map.Add(i, nextForI);
                        var nextForJ = map.GetValueOrDefault(j, new List<(int scnr, (int beaconId, int otherBeaconId, List<long> hashes) intersect)>());
                        nextForJ.Add((i, (intersect.otherBeaconId, intersect.beaconId, intersect.hashes)));
                        if (nextForJ.Count == 1) map.Add(j, nextForJ);
                    }
                }
            }

            var open = new HashSet<int>();
            var close = new HashSet<int>();
            open.Add(0);
            scanners[0].Transform = (v) => v;
            var all = new HashSet<(int, int, int)>();

            while (open.Count > 0)
            {
                var nextOpen = new HashSet<int>();
                foreach (var current in open)
                {
                    var currentScnr = scanners[current];
                    for (var i = 0; i < currentScnr.Beacons.Count; i++)
                    {
                        var t = currentScnr.Transform(currentScnr.Beacons[i]);
                        currentScnr.Beacons[i] = t;
                        all.Add(t);
                    }
                    //if (current != 0) Validate(currentScnr, map[current].Select(x => scanners[x.scnr]).Where(x => x.Transform != null).ToArray());

                    foreach (var next in map[current])
                    {
                        var nextScnrId = next.scnr;
                        if (close.Contains(nextScnrId) || open.Contains(nextScnrId)) continue;

                        var intersect = next.intersect;
                        var nextScnr = scanners[nextScnrId];
                        var x0 = currentScnr.Beacons[intersect.beaconId];
                        var x1Id = currentScnr.RelativeHashToIdMap[intersect.beaconId][intersect.hashes[0]];
                        var x1 = currentScnr.Beacons[x1Id];

                        nextScnr.CalcTranform(intersect.otherBeaconId, intersect.hashes[0], x0, x1);

                        var newValues = intersect.hashes
                            .Select(h => nextScnr.Beacons[nextScnr.RelativeHashToIdMap[intersect.otherBeaconId][h]])
                            .Select(nextScnr.Transform)
                            .ToArray();
                        var intr = currentScnr.Beacons.Intersect(newValues).ToArray();
                        if (intr.Length == 11)
                        {
                            nextOpen.Add(nextScnrId);
                        }
                        else
                        {
                            nextScnr.Transform = null;
                        }
                    }
                    close.Add(current);
                }
                open = nextOpen;
            }

            var scnr0Point = scanners.Select(s => s.Transform((0, 0, 0))).ToArray();

            var mh = 0;
            for (int i = 0; i < scnr0Point.Length; i++)
            {
                for (int j = i + 1; j < scnr0Point.Length; j++)
                {
                    var r = Math.Abs(scnr0Point[j].x - scnr0Point[i].x) +
                        Math.Abs(scnr0Point[j].y - scnr0Point[i].y) +
                        Math.Abs(scnr0Point[j].z - scnr0Point[i].z);
                    if (r > mh) mh = r;
                }
            }

            yield return mh.ToString();
        }

    }
}
