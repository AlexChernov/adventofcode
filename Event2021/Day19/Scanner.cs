using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Event2021
{
    internal class Scanner
    {
        public Scanner(List<(int x, int y, int z)> beacons)
        {
            Beacons = beacons;

            RelativeHashes = new List<List<long>>(Beacons.Count);
            RelativeHashToIdMap = new List<Dictionary<long, int>>(Beacons.Count);
            for (var i = 0; i < beacons.Count; i++)
            {
                RelativeHashToIdMap.Add(new Dictionary<long, int>());
                var hashes = new List<long>();
                for (var j = 0; j < beacons.Count; j++)
                {
                    if (i == j) continue;
                    var dx = beacons[i].x - beacons[j].x;
                    var dy = beacons[i].y - beacons[j].y;
                    var dz = beacons[i].z - beacons[j].z;
                    long t = new[] { dx, dy, dz }
                        .Select(Math.Abs)
                        .OrderBy(x => x)
                        .Aggregate(
                            0L,
                            (hash, value) =>
                            {
                                return hash * 2000 + value;
                            });
                    hashes.Add(t);
                    RelativeHashToIdMap[i][t] = j;
                }
                hashes = hashes.OrderBy(x => x).ToList();
                RelativeHashes.Add(hashes);
            }
        }

        internal bool TryFindIntersect(Scanner other, out (int beaconId, int otherBeaconId, List<long> hashes) intersect)
        {
            for (var i = 0; i < RelativeHashes.Count; i++)
            {
                for (var j = 0; j < other.RelativeHashes.Count; j++)
                {
                    var leftSrc = RelativeHashes[i];
                    var rightSrc = other.RelativeHashes[j];

                    if (PossibleMatch(leftSrc, rightSrc, out var intersectList))
                    {
                        intersect = (i, j, intersectList);
                        return true;
                    }
                }
            }
            intersect = (0, 0, null);
            return false;
        }

        private static bool PossibleMatch(List<long> leftSrc, List<long> rightSrc, out List<long> intersectList)
        {
            var leftIndex = 0;
            var rightIndex = 0;

            var intesectCount = 0;
            intersectList = new List<long>();
            while (leftIndex < leftSrc.Count && rightIndex < rightSrc.Count)
            {
                var left = leftSrc[leftIndex];
                var right = rightSrc[rightIndex];
                if (left == right)
                {
                    intersectList.Add(left);
                    ++intesectCount;
                    if (intesectCount > 10)
                    {
                        return true;
                    }
                    else
                    {
                        ++leftIndex;
                        ++rightIndex;
                        continue;
                    }
                }
                if (left > right)
                {
                    rightIndex++;
                }
                else
                {
                    leftIndex++;
                }
            }
            return false;
        }

        internal void CalcTranform(int beaconId, long hash, (int x, int y, int z) a0, (int x, int y, int z) a1)
        {
            var b1id = RelativeHashToIdMap[beaconId][hash];
            var b0 = Beacons[beaconId];
            var b1 = Beacons[b1id];

            var dxa = a1.x - a0.x;
            var dya = a1.y - a0.y;
            var dza = a1.z - a0.z;

            if (Math.Abs(dxa) == Math.Abs(dya) || Math.Abs(dza) == Math.Abs(dya) || Math.Abs(dxa) == Math.Abs(dza)) throw new Exception();

            var dxb = b1.x - b0.x;
            var dyb = b1.y - b0.y;
            var dzb = b1.z - b0.z;

            var (r00, r01, r02) = Rotate(dxb, dyb, dzb, dxa);
            var (r10, r11, r12) = Rotate(dxb, dyb, dzb, dya);
            var (r20, r21, r22) = Rotate(dxb, dyb, dzb, dza);

            var Bx = r00 * b0.x + r01 * b0.y + r02 * b0.z;
            var By = r10 * b0.x + r11 * b0.y + r12 * b0.z;
            var Bz = r20 * b0.x + r21 * b0.y + r22 * b0.z;

            var ox = a0.x - Bx;
            var oy = a0.y - By;
            var oz = a0.z - Bz;

            Transform = (v) =>
            {
                var Bx = r00 * v.x + r01 * v.y + r02 * v.z;
                var By = r10 * v.x + r11 * v.y + r12 * v.z;
                var Bz = r20 * v.x + r21 * v.y + r22 * v.z;

                return (Bx + ox, By + oy, Bz + oz);
            };
        }

        private (int, int, int) Rotate(int dxa, int dya, int dza, int db)
        {
            if (dxa == db) return (1, 0, 0);
            if (-dxa == db) return (-1, 0, 0);
            if (dya == db) return (0, 1, 0);
            if (-dya == db) return (0, -1, 0);
            if (dza == db) return (0, 0, 1);
            if (-dza == db) return (0, 0, -1);
            throw new Exception();
        }

        public List<(int x, int y, int z)> Beacons { get; }
        public List<List<long>> RelativeHashes { get; private set; }
        public List<Dictionary<long, int>> RelativeHashToIdMap { get; private set; }
        public Func<(int x, int y, int z), (int x, int y, int z)> Transform { get; set; }
    }
}