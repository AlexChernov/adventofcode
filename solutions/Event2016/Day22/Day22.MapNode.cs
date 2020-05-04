namespace AdventOfCode.Solutions.Event2016.Day22
{
    public partial class Day22
    {
        private class MapNode
        {
            public int CloseCount;
            public int OpenCount;
            public bool CanMove;

            public override string ToString()
            {
                return this.CanMove ? "." + (this.OpenCount % 10).ToString() + "_" + (this.CloseCount % 10).ToString() : "####";
            }
        }
    }
}
