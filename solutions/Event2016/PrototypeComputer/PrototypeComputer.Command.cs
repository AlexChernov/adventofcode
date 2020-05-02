using System;
using System.Text;

namespace AdventOfCode.Solutions.Event2016.PrototypeComputer
{
    internal partial class PrototypeComputer
    {
        internal class Instruction
        {
            public string Method;
            public string[] Args;

            public override string ToString()
            {
                var str = new StringBuilder(Method);
                str.Append(' ');
                str.AppendJoin(' ', Args);
                return str.ToString();
            }
        }
    }
}
