using System;
using System.Collections.Generic;
using AdventOfCode.Solutions.Common;

namespace AdventOfCode.Solutions.Event2016.Day23
{
    public class Day23 : IAdventOfCodeDay
    {
        public bool HaveVisualization()
        {
            return false;
        }

        public IEnumerable<string> Run1(string input, bool shouldVisualise)
        {
            return SimplifiedProgram(input, 7);
        }

        public IEnumerable<string> Run2(string input, bool shouldVisualise)
        {
            return SimplifiedProgram(input, 12);
        }

        private IEnumerable<string> SimplifiedProgram(string program, int input)
        {
            var comp = new PrototypeComputer.PrototypeComputer(program);

            if (!Int32.TryParse(comp.state.Instructions[19].Args[0], out var left) ||
                !Int32.TryParse(comp.state.Instructions[20].Args[0], out var right))
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
