namespace AdventOfCode.Solutions.Event2016.PrototypeComputer
{
    using System.Text;

    internal partial class PrototypeComputer
    {
        internal class Instruction
        {
            public string Method;
            public string[] Args;

            /// <inheritdoc/>
            public override string ToString()
            {
                var str = new StringBuilder(this.Method);
                str.Append(' ');
                str.AppendJoin(' ', this.Args);
                return str.ToString();
            }
        }
    }
}
