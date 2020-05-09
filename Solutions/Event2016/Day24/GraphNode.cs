namespace AdventOfCode.Solutions.Event2016.Day24
{
    using System;
    using System.Collections.Generic;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates data of graph node.
    /// </summary>
    public class GraphNode
    {
        /// <summary>
        /// Gets or sets the parent of node.
        /// </summary>
        internal GraphNode Parent { get; set; }

        /// <summary>
        /// Gets or sets the last visited lcoation.
        /// </summary>
        internal char LastLocation { get; set; }

        /// <summary>
        /// Gets or sets distance from start to current node.
        /// </summary>
        internal int H { get; set; }

        /// <summary>
        /// Gets or sets heuristic distance from current node to target node.
        /// </summary>
        internal int F { get; set; }

        /// <summary>
        /// Gets heuristic distance from start to target node.
        /// </summary>
        internal int G
        {
            get => this.F + this.H;
            private set { }
        }

        /// <summary>
        /// Gets or sets the current position.
        /// </summary>
        internal X_Y CurrentPos { get; set; }

        /// <summary>
        /// Gets or sets the locations to visit.
        /// </summary>
        internal string LocationsToVisit { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is GraphNode node &&
                   EqualityComparer<X_Y>.Default.Equals(this.CurrentPos, node.CurrentPos) &&
                   this.LocationsToVisit == node.LocationsToVisit;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(this.CurrentPos, this.LocationsToVisit);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.CurrentPos.ToString() + "_" + this.LocationsToVisit;
        }
    }
}
