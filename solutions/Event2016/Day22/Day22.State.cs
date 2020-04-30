using System.Collections.Generic;

namespace AdventOfCode.Solutions.Event2016.Day22
{
    public partial class Day22
    {
        public class State
        {
            public Node[,] Nodes;
            public int MaxAvail;
            public ISet<GraphNode> Open;
            public HashSet<GraphNode> Close;
            public GraphNode LastClosed;
            public GraphNode Path;
            public int I;
        }
    }
}
