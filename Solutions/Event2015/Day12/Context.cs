namespace AdventOfCode.Solutions.Event2015.Day12
{
    /// <summary>
    /// Incapsulates the logic of JSON context.
    /// </summary>
    internal class Context
    {
        /// <summary>
        /// Gets or sets the sum of integers in this context.
        /// </summary>
        public int Sum { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ths contex is array.
        /// </summary>
        public bool IsArray { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ths contex contains "red".
        /// </summary>
        public bool IsRed { get; set; }
    }
}
