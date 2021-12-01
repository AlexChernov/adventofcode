namespace AdventOfCode.Solutions.Event2020.Day9
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 9 of event.
    /// </summary>
    public class Day9 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HasVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var commands = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Select(v => CommandFactory.Create(v)).ToArray();
            Context context = Run(commands, out var _);

            var result = context.Value;

            yield return result.ToString();
        }

        private static Context Run(IAocCommand[] values, out HashSet<int> visited)
        {
            var context = new Context()
            {
                Index = 0,
                Value = 0
            };

            visited = new HashSet<int>();
            while (0 <= context.Index && context.Index < values.Length)
            {
                if (visited.Contains(context.Index))
                {
                    break;
                }
                visited.Add(context.Index);
                values[context.Index].Run(context);
            }

            return context;
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var commands = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Select(v => CommandFactory.Create(v)).ToArray();
            var context = Run(commands, out var firstRunVisited);

            foreach (var index in firstRunVisited)
            {
                var fixedCommand = commands[index].SwitchCommand();

                if (fixedCommand == null)
                {
                    continue;
                }

                commands[index] = fixedCommand;
                context = Run(commands, out var _);
                if (context.Index == commands.Length)
                {
                    break;
                }
                commands[index] = commands[index].SwitchCommand();
            }

            var result = context.Value;

            yield return result.ToString();
        }
    }
}
