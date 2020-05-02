using AdventOfCode.Solutions.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Event2016.Day24
{
    partial class Day24 : IAdventOfCodeDayRunner
    {
        public bool HaveVisualization() => true;

        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            InitNodes(lines, out var locations, out var map);

            if (!locations.TryGetValue('0', out var startPos))
            {
                yield return "Wrong input!";
                yield break;
            }

            GraphNode pathNode = null;
            var spinner = new Lazy<Spinner>(() => new Spinner());
            var calcMap = new Lazy<char[,]>(() => InitPrintMap(map));
            foreach (var state in FindPath(map, locations, startPos))
            {
                pathNode = state.Path;
                if (!shouldVisualise)
                {
                    continue;
                }

                UpdateCalcMap(calcMap.Value, state);
                spinner.Value.Turn();
                var titleCalc = "Calculating path... " + spinner.Value.State;

                yield return PrintPathFindingState(calcMap.Value, titleCalc);
                calcMap.Value[state.LastClosed.CurrentPos.X, state.LastClosed.CurrentPos.Y] = state.LastClosed.LastLocation;
            }

            var title = pathNode.H.ToString() + " is the fewest number of steps required to move your goal data to target node.";

            if (!shouldVisualise)
            {
                yield return title;
                yield break;
            }
        }

        private string PrintPathFindingState(char[,] printMap, string title)
        {
            var str = new StringBuilder(title);
            str.AppendLine();

            for (int i = 0; i < printMap.GetLength(0); ++i)
            {
                for (int j = 0; j < printMap.GetLength(1); ++j)
                {
                    str.Append(printMap[i,j]);
                }
                str.AppendLine();
            }

            return str.ToString();
        }

        private void UpdateCalcMap(char[,] calcMap, State state)
        {
            calcMap[state.LastClosed.CurrentPos.X, state.LastClosed.CurrentPos.Y] = '!';

            foreach (var open in state.LastOpen)
            {
                calcMap[open.CurrentPos.X, open.CurrentPos.Y] = '?';
            }
        }

        private char[,] InitPrintMap(MapNode[,] map)
        {
            var printMap = new char[map.GetLength(0), map.GetLength(1)];

            for (int i = 0; i < printMap.GetLength(0); ++i)
            {
                for (int j = 0; j < printMap.GetLength(1); ++j)
                {
                    printMap[i, j] = map[i, j].location.HasValue ? map[i, j].location.Value : (map[i, j].canMove ? '.' : '#');
                }
            }

            return printMap;
        }

        private void InitNodes(string[] lines, out Dictionary<char, X_Y> locations, out MapNode[,] map)
        {
            locations = new Dictionary<char, X_Y>();
            map = new MapNode[lines.Length, lines[0].Length];
            for (int i = 0; i < lines.Length; ++i)
            {
                for (int j = 0; j < lines[0].Length; ++j)
                {
                    map[i, j] = new MapNode { canMove = lines[i][j] != '#' };
                    if ('0' <= lines[i][j] && lines[i][j] <= '9')
                    {
                        map[i, j].location = lines[i][j];
                        locations.Add(lines[i][j], new X_Y { X = i, Y = j });
                    }
                }
            }
        }


        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            throw new NotImplementedException();
        }
    }
}
