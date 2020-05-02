using AdventOfCode.Solutions.Common;
using System.Collections.Generic;

namespace AdventOfCode.Solutions.Event2016.Day24
{
    public partial class Day24
    {
        private class State
        {
            public IEnumerable<GraphNode> LastOpen { get; internal set; }
            public GraphNode LastClosed { get; internal set; }
            public GraphNode Path { get; internal set; }
        }
    }
}
