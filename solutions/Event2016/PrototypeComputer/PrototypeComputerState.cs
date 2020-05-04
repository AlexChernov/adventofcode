namespace AdventOfCode.Solutions.Event2016.PrototypeComputer
{
    using System.Collections.Generic;

    /// <summary>
    /// Incapsulates the state of prototype computer.
    /// </summary>
    internal class PrototypeComputerState
    {
        /// <summary>
        /// Gets or sets the current instruction index.
        /// </summary>
        public int CurrentIndex { get; set; }

        /// <summary>
        /// Gets or sets the registers.
        /// </summary>
        public Dictionary<string, long> Registers { get; set; }

        /// <summary>
        /// Gets or sets the instructions.
        /// </summary>
        public IList<Instruction> Instructions { get; internal set; }
    }
}
