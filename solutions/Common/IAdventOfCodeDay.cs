using System.Collections.Generic;

namespace AdventOfCode.Solutions.Common
{
    public interface IAdventOfCodeDay
    {
        IEnumerable<string> Run1(string input, bool shouldVisualise);
        IEnumerable<string> Run2(string input, bool shouldVisualise);
        bool HaveVisualization();
    }
}