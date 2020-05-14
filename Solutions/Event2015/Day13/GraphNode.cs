namespace AdventOfCode.Solutions.Event2015.Day13
{
    /// <summary>
    /// Incapsulates logic of graph node.
    /// </summary>
    public class GraphNode
    {
        /// <summary>
        /// Gets or sets the cost of current node.
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// Gets or sets the parent node.
        /// </summary>
        public GraphNode Parent { get; set; }

        /// <summary>
        /// Gets or sets the corresponding person of current node.
        /// </summary>
        public string CurrentPerson { get; set; }
    }
}