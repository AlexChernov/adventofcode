namespace AdventOfCode.Solutions.Event2016.Day22
{
    /// <summary>
    /// Incapsulates the map node.
    /// </summary>
    internal class MapNode
    {
        /// <summary>
        /// Gets or sets the count of times this node closed.
        /// </summary>
        internal int ClosedCount { get; set; }

        /// <summary>
        /// Gets or sets the count of times this node open.
        /// </summary>
        internal int OpenCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether can move to this node.
        /// </summary>
        internal bool CanMove { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.CanMove ? "." + (this.OpenCount % 10).ToString() + "_" + (this.ClosedCount % 10).ToString() : "####";
        }
    }
}
