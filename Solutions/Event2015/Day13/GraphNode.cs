namespace AdventOfCode.Solutions.Event2015.Day13
{
    public class GraphNode
    {
        public int Cost { get; internal set; }
        public GraphNode Parent { get; internal set; }
        public string CurrentPerson { get; internal set; }
    }
}