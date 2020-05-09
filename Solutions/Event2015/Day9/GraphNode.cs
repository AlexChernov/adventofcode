namespace AdventOfCode.Solutions.Event2015.Day9
{
    using System;

    /// <summary>
    /// Incapsulates graph node.
    /// </summary>
    public class GraphNode
    {
        /// <summary>
        /// Gets or sets the current possition.
        /// </summary>
        public string CurrentPos { get; set; }

        /// <summary>
        /// Gets or sets locations to visit code.
        /// </summary>
        public int LocationsToVisit { get; set; }

        /// <summary>
        /// Gets or sets cost of path.
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// Gets or sets the parrent of node.
        /// </summary>
        public GraphNode Parent { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is GraphNode other &&
                   this.LocationsToVisit == other.LocationsToVisit &&
                   this.CurrentPos == other.CurrentPos;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(this.CurrentPos, this.LocationsToVisit);
        }
    }
}