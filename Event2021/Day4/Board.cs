using System;
using System.Collections.Generic;

namespace AdventOfCode.Solutions.Event2021.Day4
{
    internal class Board
    {
        private Dictionary<int, (int, int)> _boardMap;
        private int[] _unchecedRows;
        private int[] _unchecedCols;

        public int Score { get; private set; }
        public bool IsWon { get; private set; }

        public Board()
        {
            Score = 0;
            _boardMap = new Dictionary<int, (int, int)>();
            _unchecedRows = new int[5];
            _unchecedCols = new int[5];
        }

        internal void Init(int v, int row, int col)
        {
            Score += v;
            _boardMap[v] = (row, col);
            _unchecedCols[col] += 1;
            _unchecedRows[row] += 1;
        }

        internal bool CheckWin(int value)
        {
            if (!_boardMap.TryGetValue(value, out var rc))
            {
                return false;
            }

            Score -= value;
            var (row, col) = rc;
            IsWon = (--_unchecedCols[col] == 0) || (--_unchecedRows[row] == 0);
            return IsWon;
        }
    }
}