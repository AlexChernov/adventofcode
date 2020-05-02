using System.Collections.Generic;

namespace AdventOfCode.Solutions.Common
{
    public interface IAdventOfCodeDayRunner
    {
        bool HaveVisualization();
        IEnumerable<string> RunTask1(string input, bool shouldVisualise);
        IEnumerable<string> RunTask2(string input, bool shouldVisualise);
    }
}