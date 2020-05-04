namespace AdventOfCode.Solutions.Event2016.Day22
{
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates data of graph node.
    /// </summary>
    public class GraphNode
    {
        /// <summary>
        /// Gets or sets the target node position.
        /// </summary>
        internal X_Y TargetNodePos { get; set; }

        /// <summary>
        /// Gets or sets the empty node position.
        /// </summary>
        internal X_Y EmptyNodePos { get; set; }

        /// <summary>
        /// Gets heuristic distance from start to target node.
        /// </summary>
        internal int G
        {
            get => this.F + this.H;
            private set { }
        }

        /// <summary>
        /// Gets or sets distance from start to current node.
        /// </summary>
        internal int H { get; set; }

        /// <summary>
        /// Gets or sets heuristic distance from current node to target node.
        /// </summary>
        internal int F { get; set; }

        /// <summary>
        /// Gets or sets the parent node.
        /// </summary>
        internal GraphNode Parent { get; set; } = null;

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is GraphNode other &&
                this.TargetNodePos.Equals(other.TargetNodePos) &&
                this.EmptyNodePos.Equals(other.EmptyNodePos);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return System.HashCode.Combine(this.TargetNodePos, this.EmptyNodePos);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.TargetNodePos.ToString() + "-" + this.EmptyNodePos.ToString();
        }
    }
}
