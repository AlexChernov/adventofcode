namespace AdventOfCode.Solutions.Event2016.Day22
{
    using AdventOfCode.Solutions.Common;

    public partial class Day22
    {
        public class GraphNode
        {
            internal X_Y TargetNodePos { get; set; }

            internal X_Y EmptyNodePos { get; set; }

            internal int G
            {
                get => this.F + this.H;
                private set { }
            }

            internal int H { get; set; }

            internal int F { get; set; }

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
}
