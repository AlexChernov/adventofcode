namespace AdventOfCode.Solutions.Event2016.PrototypeComputer
{
    using System;
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
        public IList<Instruction> Instructions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether can be computer run.
        /// </summary>
        public bool CanRun { get; set; }

        /// <summary>
        /// Gets or sets the output.
        /// </summary>
        public ICollection<int> Out { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is PrototypeComputerState other &&
                this.CurrentIndex == other.CurrentIndex &&
                this.RegistersEquals(other.Registers);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(this.CurrentIndex, this.Registers, this.Instructions, this.CanRun);
        }

        private bool RegistersEquals(Dictionary<string, long> registers)
        {
            foreach (var key in this.Registers.Keys)
            {
                if (this.Registers[key] != registers[key])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
