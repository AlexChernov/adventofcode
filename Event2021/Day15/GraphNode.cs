using AdventOfCode.Solutions.Common;

namespace AdventOfCode.Solutions.Event2021
{
    internal class GraphNode
    {
        /// <summary>
        /// Gets or sets position.
        /// </summary>
        internal (int x, int y) pos { get; set; }

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
            return obj is GraphNode other && other.pos.Equals(this.pos);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return System.HashCode.Combine(this.pos);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{this.pos}-G:{G}-H:{H}-F:{F}";
        }
    }
}