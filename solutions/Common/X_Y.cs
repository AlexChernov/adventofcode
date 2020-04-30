using System;

namespace AdventOfCode.Solutions.Common
{
    public class X_Y
    {
        public int X;
        public int Y;

        public override bool Equals(object obj)
        {
            return obj is X_Y y &&
                   X == y.X &&
                   Y == y.Y;
        }

        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return X.ToString() + "_" + Y.ToString();
        }

        public int CalcManhattanDistance(X_Y other)
        {
            return Math.Abs(other.X - X) + Math.Abs(other.Y - Y);
        }
    }

}
