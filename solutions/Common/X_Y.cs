namespace AdventOfCode.Solutions.Common
{
    using System;

    /// <summary>
    /// Incapsulates 2D position.
    /// </summary>
    public class X_Y
    {
        /// <summary>
        /// Gets or sets x coordinate.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets y coordinate.
        /// </summary>
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

        /// <summary>
        /// Calculates the Manhattan distance to other.
        /// </summary>
        /// <param name="other">The other position.</param>
        /// <returns>The Manhattan distance to other.</returns>
        public int CalcManhattanDistance(X_Y other)
        {
            return Math.Abs(other.X - this.X) + Math.Abs(other.Y - this.Y);
        }
    }
}
