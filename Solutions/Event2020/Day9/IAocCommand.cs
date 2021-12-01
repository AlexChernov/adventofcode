namespace AdventOfCode.Solutions.Event2020.Day9
{
    public interface IAocCommand
    {
        void Run(Context context);

        IAocCommand SwitchCommand();
    }
}