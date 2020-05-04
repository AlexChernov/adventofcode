namespace AdventOfCode.Solutions.Event2016.PrototypeComputer
{
    using System.Collections.Generic;

    internal partial class PrototypeComputer
    {
        internal class PrototypeComputerState
        {
            public int CurrentIndex { get; set; }

            public Dictionary<string, long> Registers { get; set; }

            public IList<Instruction> Instructions { get; internal set; }
        }
    }
}
