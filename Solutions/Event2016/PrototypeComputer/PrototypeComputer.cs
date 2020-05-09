namespace AdventOfCode.Solutions.Event2016.PrototypeComputer
{
    using System;
    using System.Collections.Generic;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates the prototype computer logic.
    /// </summary>
    internal class PrototypeComputer
    {
        private static readonly IDictionary<string, Action<PrototypeComputerState, string[]>> MethodsMapping = new Dictionary<string, Action<PrototypeComputerState, string[]>>()
        {
            {
                "cpy", (state, args) =>
                {
                    if (!state.Registers.ContainsKey(args[1]))
                    {
                        return;
                    }

                    state.Registers[args[1]] = GetValue(args[0], state);
                }
            },
            {
                "inc", (state, args) =>
                {
                    if (!state.Registers.ContainsKey(args[0]))
                    {
                        return;
                    }

                    // inc x increases the value of register x by one.
                    state.Registers[args[0]] += 1;
                }
            },
            {
                "dec", (state, args) =>
                {
                    if (!state.Registers.ContainsKey(args[0]))
                    {
                        return;
                    }

                    // dec x decreases the value of register x by one.
                    state.Registers[args[0]] -= 1;
                }
            },
            {
                "jnz", (state, args) =>
                {
                    // jnz X Y jumps with an offset
                    if (GetValue(args[0], state) != 0)
                    {
                        state.CurrentIndex += GetValue(args[1], state) - 1;
                    }
                }
            },
            {
                "tgl", (state, args) =>
                {
                    // tgl X toggles the instruction x away
                    var index = state.CurrentIndex + GetValue(args[0], state);
                    if (index < 0 || state.Instructions.Count <= index)
                    {
                        return;
                    }

                    var instruction = state.Instructions[index];

                    instruction.Method = ToggleMethod(instruction.Method);
                }
            },
            {
                "out", (state, args) =>
                {
                    state.Out.Add(GetValue(args[0], state));
                }
            },
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="PrototypeComputer"/> class.
        /// </summary>
        /// <param name="input">The prototype computer input.</param>
        public PrototypeComputer(string input)
        {
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            this.State = new PrototypeComputerState
            {
                CurrentIndex = 0,
                Registers = InitRegisters(),
                Instructions = this.InitCommands(lines),
                Out = new LinkedList<int>(),
                CanRun = true,
            };
        }

        /// <summary>
        /// Gets the State.
        /// </summary>
        internal PrototypeComputerState State { get; private set; }

        /// <summary>
        /// Sets the register's value.
        /// </summary>
        /// <param name="key">The key of register.</param>
        /// <param name="value">The value.</param>
        public void SetRegister(string key, int value)
        {
            this.State.Registers[key] = value;
        }

        /// <summary>
        /// Runs instructions.
        /// </summary>
        /// <returns>The last state of registers.</returns>
        public PrototypeComputerState Run()
        {
            while (this.State.CanRun)
            {
                this.RunNext();
            }

            return this.State;
        }

        /// <summary>
        /// Runs instructions.
        /// </summary>
        /// <returns>The last state of registers.</returns>
        public PrototypeComputerState RunNext()
        {
            if (!this.State.CanRun)
            {
                return this.State;
            }

            var instruction = this.State.Instructions[this.State.CurrentIndex];
            MethodsMapping[instruction.Method](this.State, instruction.Args);

            ++this.State.CurrentIndex;

            if (this.State.CurrentIndex < 0 || this.State.CurrentIndex >= this.State.Instructions.Count)
            {
                this.State.CanRun = false;
            }

            return this.State;
        }

        private static string ToggleMethod(string method)
        {
            switch (method)
            {
                // inc becomes dec, and all other one-argument instructions become inc.
                case "inc":
                    return "dec";
                case "dec":
                case "tgl":
                case "out":
                    return "inc";

                // jnz becomes cpy, and all other two-instructions become jnz.
                case "jnz":
                    return "cpy";
                case "cpy":
                    return "jnz";
                default:
                    throw new NotSupportedException(method);
            }
        }

        private static int GetValue(string input, PrototypeComputerState state)
        {
            if (int.TryParse(input, out var value))
            {
                return value;
            }

            return (int)state.Registers[input];
        }

        private static Dictionary<string, long> InitRegisters()
        {
            return new Dictionary<string, long>()
            {
                { "a", 0 },
                { "b", 0 },
                { "c", 0 },
                { "d", 0 },
                { "e", 0 },
                { "f", 0 },
                { "g", 0 },
                { "h", 0 },
            };
        }

        private IList<Instruction> InitCommands(string[] text)
        {
            var commands = new List<Instruction>(text.Length);
            foreach (var line in text)
            {
                var parameters = line.Split(' ');
                var command = new Instruction()
                {
                    Method = parameters[0],
                    Args = Utils.Subarray(parameters, 1, parameters.Length - 1),
                };

                commands.Add(command);
            }

            return commands;
        }
    }
}
