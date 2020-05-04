namespace AdventOfCode.Solutions.Event2016.Day24
{
    using System;
    using System.Collections.Generic;
    using AdventOfCode.Solutions.Common;

    public partial class Day24
    {
        public class GraphNode
        {
            internal GraphNode Parent;
            internal char LastLocation;

            public int H { get; internal set; }

            public int F { get; internal set; }

            public int G
            {
                get => this.F + this.H;
                private set { }
            }

            public X_Y CurrentPos { get; internal set; }

            public string LocationsToVisit { get; internal set; }

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
}
