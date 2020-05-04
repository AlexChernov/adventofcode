namespace AdventOfCode.Solutions.Event2016.Day22
{
    using System.Collections.Generic;

    public partial class Day22
    {
        public class State
        {
            public ISet<GraphNode> Open;
            public HashSet<GraphNode> Close;
            public GraphNode LastClosed;
            public GraphNode Path;
        }
    }
}
