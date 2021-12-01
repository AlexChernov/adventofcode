using System;

namespace AdventOfCode.Solutions.Event2020.Day9
{
    public class CommandFactory
    {
        public static IAocCommand Create(string value)
        {
            var split = value.Split(" ");

            int incrementValue = int.Parse(split[1].Replace("+", ""));
            switch (split[0])
            {
                case "nop":
                    return new Nop(incrementValue);
                case "acc":
                    return new Acc(incrementValue);
                case "jmp":
                    return new Jmp(incrementValue);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public class Nop : IAocCommand
    {
        private int incrementValue;

        public Nop(int incrementValue)
        {
            this.incrementValue = incrementValue;
        }

        public void Run(Context context)
        {
            context.Index += 1;
        }

        public IAocCommand SwitchCommand()
        {
            return new Jmp(incrementValue);
        }
    }

    public class Acc : IAocCommand
    {
        private readonly int incrementValue;

        public Acc(int incrementValue)
        {
            this.incrementValue = incrementValue;
        }

        public void Run(Context context)
        {
            context.Value += incrementValue;
            context.Index += 1;
        }

        public IAocCommand SwitchCommand()
        {
            return null;
        }
    }

    public class Jmp : IAocCommand
    {
        private readonly int jumpValue;

        public Jmp(int jumpValue)
        {
            this.jumpValue = jumpValue;
        }

        public void Run(Context context)
        {
            context.Index += jumpValue;
        }

        public IAocCommand SwitchCommand()
        {
            return new Nop(jumpValue);
        }
    }
}