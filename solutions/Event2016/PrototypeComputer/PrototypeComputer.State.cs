using System.Collections.Generic;

namespace AdventOfCode.Solutions.Event2016.PrototypeComputer
{
    internal partial class PrototypeComputer
    {
        internal class State
        {
            public int CurrentIndex { get; set; }
            public Dictionary<string, long> Registers { get; set; }
            public IList<Instruction> Instructions { get; internal set; }
        }
    }
}
