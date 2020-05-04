namespace AdventOfCode.Solutions.Event2016.Day23
{
    using System.Collections.Generic;
    using AdventOfCode.Solutions.Common;

    public class Day23 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HaveVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            return this.SimplifiedProgram(input, 7);
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            return this.SimplifiedProgram(input, 12);
        }

        private IEnumerable<string> SimplifiedProgram(string program, int input)
        {
            var comp = new PrototypeComputer.PrototypeComputer(program);

            if (!int.TryParse(comp.State.Instructions[19].Args[0], out var left) ||
                !int.TryParse(comp.State.Instructions[20].Args[0], out var right))
            {
                yield return "Wrong input!.\nThis solution abuses fact that program should calculate factorial of 7 and add multiply arguments of instructions #19 and #20.";
                yield break;
            }

            long value = left * right;
            value += Utils.Factorial(input);

            yield return value.ToString() + " should be sent to the safe.";
        }
    }
}
