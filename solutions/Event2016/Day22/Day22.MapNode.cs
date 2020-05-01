namespace AdventOfCode.Solutions.Event2016.Day22
{
    public partial class Day22
    {
        private class MapNode
        {
            public int closeCount;
            public int openCount;
            public bool canMove;

            public override string ToString()
            {
                return canMove ? "." + (openCount % 10).ToString() + "_" + (closeCount % 10).ToString() : "####";
            }
        }
    }
}
