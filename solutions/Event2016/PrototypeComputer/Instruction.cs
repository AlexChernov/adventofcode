namespace AdventOfCode.Solutions.Event2016.PrototypeComputer
{
    using System.Text;

    /// <summary>
    /// Incapsulates prototype compututer instruction.
    /// </summary>
    internal class Instruction
    {
        /// <summary>
        /// Gets or sets the method code.
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the arguments of instructon.
        /// </summary>
        public string[] Args { get; set; }

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
