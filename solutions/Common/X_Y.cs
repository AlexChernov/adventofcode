namespace AdventOfCode.Solutions.Common
{
    using System;

    public class X_Y
    {
        public int X { get; set; }

        public int Y { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is X_Y other &&
                   this.X == other.X &&
                   this.Y == other.Y;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(this.X, this.Y);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.X.ToString() + "_" + this.Y.ToString();
        }

        public int CalcManhattanDistance(X_Y other)
        {
            return Math.Abs(other.X - this.X) + Math.Abs(other.Y - this.Y);
        }
    }
}
