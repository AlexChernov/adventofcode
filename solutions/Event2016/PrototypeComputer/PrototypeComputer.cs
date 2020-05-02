using System;
using System.Collections.Generic;
using System.Net;
using AdventOfCode.Solutions.Common;

namespace AdventOfCode.Solutions.Event2016.PrototypeComputer
{
    internal partial class PrototypeComputer
    {
        public State state;

        public PrototypeComputer(string input)
        {
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            state = new State
            {
                CurrentIndex = 0,
                Registers = InitRegisters(),
                Instructions = InitCommands(lines),
            };
        }

        public void SetRegister(string key, int value)
        {
            state.Registers[key] = value;
        }

        public Dictionary<string, long> Run()
        {
            while (state.CurrentIndex < state.Instructions.Count)
            {
                var instruction = state.Instructions[state.CurrentIndex];
                methodsMapping[instruction.Method](state, instruction.Args);

                ++state.CurrentIndex;
            }

            return state.Registers;
        }

        private static IDictionary<string, Action<State, string[]>> methodsMapping = new Dictionary<string, Action<State, string[]>>()
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
                    //jnz X Y jumps with an offset
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
            }
        };

        private static string ToggleMethod(string method)
        {
            switch (method)
            {
                //inc becomes dec, and all other one-argument instructions become inc.
                case "inc":
                    return "dec";
                case "dec":
                case "tgl":
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

        private static int GetValue(string input, State state)
        {
            if (Int32.TryParse(input, out var value))
            {
                return value;
            }

            return (int)state.Registers[input];
        }

        private static Dictionary<string, long> InitRegisters()
        {
            return new Dictionary<string, long>()
            {
                { "a", 0},
                { "b", 0},
                { "c", 0},
                { "d", 0},
                { "e", 0},
                { "f", 0},
                { "g", 0},
                { "h", 0},
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
                    Args = Utils.SubArray(parameters, 1, parameters.Length - 1),
                };

                commands.Add(command);
            }

            return commands;
        }
    }
}
