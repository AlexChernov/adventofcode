using AdventOfCode.Solutions.Common;

namespace AdventOfCode.Solutions.Event2016.Day22
{
    public partial class Day22
    {
        public class GraphNode
        {
            public X_Y TargetNodePos;
            public X_Y EmptyNodePos;
            public int G
            {
                get
                {
                    return F + H;
                }
                private set { }
            }
            public int H;
            internal int F;
            public GraphNode Parent = null;

            public override bool Equals(object obj)
            {
                return obj is GraphNode other &&
                    this.TargetNodePos.Equals(other.TargetNodePos) &&
                    this.EmptyNodePos.Equals(other.EmptyNodePos);
            }

            public override int GetHashCode()
            {
                int hashCode = 151352842;
                hashCode = hashCode * -1521134295 + TargetNodePos.GetHashCode();
                hashCode = hashCode * -1521134295 + EmptyNodePos.GetHashCode();
                return hashCode;
            }

            public override string ToString()
            {
                return TargetNodePos.ToString() + "-" + EmptyNodePos.ToString();
            }
        }
    }
}
