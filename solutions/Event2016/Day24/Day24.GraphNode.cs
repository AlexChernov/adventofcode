using AdventOfCode.Solutions.Common;
using System;
using System.Collections.Generic;

namespace AdventOfCode.Solutions.Event2016.Day24
{
    partial class Day24
    {
        public class GraphNode
        {
            internal GraphNode Parent;
            internal char LastLocation;

            public int H { get; internal set; }

            public int F { get; internal set; }
            public int G
            {
                get => F + H;
                private set { }
            }

            public X_Y CurrentPos { get; internal set; }
            public string LocationsToVisit { get; internal set; }

            public override bool Equals(object obj)
            {
                return obj is GraphNode node &&
                       EqualityComparer<X_Y>.Default.Equals(CurrentPos, node.CurrentPos) &&
                       LocationsToVisit == node.LocationsToVisit;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(CurrentPos, LocationsToVisit);
            }

            public override string ToString()
            {
                return CurrentPos.ToString() + "_" + LocationsToVisit;
            }
        }
    }
}
