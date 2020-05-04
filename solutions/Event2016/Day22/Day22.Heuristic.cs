namespace AdventOfCode.Solutions.Event2016.Day22
{
    using System;
    using System.Linq;
    using AdventOfCode.Solutions.Common;

    public partial class Day22
    {
        public static int CalcF(X_Y targetPos, X_Y emptyPos, X_Y endTargetNodePos)
        {
            var adjustedTargets = new X_Y[]
            {
                new X_Y() { X = targetPos.X - 1, Y = targetPos.Y },
                new X_Y() { X = targetPos.X + 1, Y = targetPos.Y },
                new X_Y() { X = targetPos.X, Y = targetPos.Y + 1 },
                new X_Y() { X = targetPos.X, Y = targetPos.Y - 1 },
            };

            var fs = adjustedTargets.Select(t =>
            {
                var distanceTargetY = Math.Abs(targetPos.Y - endTargetNodePos.Y);
                var distanceEmptyY = Math.Abs(t.Y - endTargetNodePos.Y);

                var distanceTargetX = Math.Abs(targetPos.X - endTargetNodePos.X);
                var distanceEmptyX = Math.Abs(t.X - endTargetNodePos.X);

                var f = CalcFAdjusted(distanceTargetX, distanceTargetY, distanceEmptyX, distanceEmptyY);

                return new { h = emptyPos.CalcManhattanDistance(t), f };
            })
                .OrderBy(x => x.h)
                .ThenBy(x => x.f);
            var target = fs.First();

            return target.h + target.f;
        }

        private static int CalcFAdjusted(int distanceTargetX, int distanceTargetY, int distanceEmptyX, int distanceEmptyY)
        {
            if (distanceTargetX == 0 && distanceTargetY == 0)
            {
                return 0;
            }

            if (distanceTargetX == 0)
            {
                return CalcFAdjustedLine(distanceTargetY, distanceEmptyY);
            }

            if (distanceTargetY == 0)
            {
                return CalcFAdjustedLine(distanceTargetX, distanceEmptyX);
            }

            var near = distanceEmptyX + distanceEmptyY < distanceTargetX + distanceTargetY;
            var diag = Math.Min(distanceTargetY, distanceTargetX);
            var count = 4;
            count += 6 * (diag - 1);
            count += near ? 0 : 2;

            if (distanceTargetY == distanceTargetX)
            {
                return count;
            }

            if (diag == distanceTargetX)
            {
                return count + 2 + CalcFAdjustedLine(distanceTargetY - diag, Math.Abs(distanceEmptyY - diag));
            }

            return count + 2 + CalcFAdjustedLine(distanceTargetX - diag, Math.Abs(distanceEmptyX - diag));
        }

        private static int CalcFAdjustedLine(int distanceTarget, int distanceEmpty)
        {
            if (distanceTarget == distanceEmpty)
            {
                return 3 + (5 * (distanceTarget - 1));
            }
            else
            {
                if (distanceTarget < distanceEmpty)
                {
                    // far.
                    return 5 * distanceTarget;
                }

                // near.
                return 1 + (5 * (distanceTarget - 1));
            }
        }
    }
}
