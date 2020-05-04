namespace AdventOfCode.Solutions.Event2016.Day24
{
    /// <summary>
    /// Incapsulates data of map node.
    /// </summary>
    public class MapNode
    {
        /// <summary>
        /// Gets or sets the location of node.
        /// </summary>
        public char? Location { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether can move on this node.
        /// </summary>
        public bool CanMove { get; set; }
    }
}
