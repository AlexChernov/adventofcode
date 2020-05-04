namespace AdventOfCode.Solutions.Event2016.Day22
{
    using System.Collections.Generic;

    /// <summary>
    /// Incapsulates data of path finding state.
    /// </summary>
    public class State
    {
        /// <summary>
        /// Gets or sets the set of open nodes.
        /// </summary>
        public ISet<GraphNode> Open { get; set; }

        /// <summary>
        /// Gets or sets the set of closed nodes.
        /// </summary>
        public ISet<GraphNode> Closed { get; set; }

        /// <summary>
        /// Gets or sets the last closed node.
        /// </summary>
        public GraphNode LastClosed { get; set; }

        /// <summary>
        /// Gets or sets the path node.
        /// </summary>
        public GraphNode Path { get; set; }
    }
}
