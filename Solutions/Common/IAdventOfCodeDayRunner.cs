namespace AdventOfCode.Solutions.Common
{
    using System.Collections.Generic;

    /// <summary>
    /// The interface for all tasks.
    /// </summary>
    public interface IAdventOfCodeDayRunner
    {
        /// <summary>
        /// Determines.
        /// </summary>
        /// <returns>True if the runner has visualization; otherwise False is returned.</returns>
        bool HasVisualization();

        /// <summary>
        /// Runs first task.
        /// </summary>
        /// <param name="input">The input for task.</param>
        /// <param name="shouldVisualise">Determines whether an task should be run with visualization.</param>
        /// <returns>The enumeration of output.</returns>
        IEnumerable<string> RunTask1(string input, bool shouldVisualise);

        /// <summary>
        /// Runs second task.
        /// </summary>
        /// <param name="input">The input for task.</param>
        /// <param name="shouldVisualise">Determines whether an task should be run with visualization.</param>
        /// <returns>The enumeration of output.</returns>
        IEnumerable<string> RunTask2(string input, bool shouldVisualise);
    }
}