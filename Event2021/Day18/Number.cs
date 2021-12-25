using System;

namespace AdventOfCode.Solutions.Event2021
{
    internal class Number
    {
        private Number _left;
        private Number _right;
        private int? _value;

        public Number(Number left, Number right)
        {
            _left = left;
            _right = right;
            _value = null;
        }

        public Number(int value)
        {
            _left = null;
            _right = null;
            _value = value;
        }

        public static Number Parse(string str)
        {
            if (int.TryParse(str, out var num))
            {
                return new Number(num);
            }
            var innerLevel = 0;
            int comma = 0;
            for (int i = 1; i < str.Length && comma == 0; i++)
            {
                switch (str[i])
                {
                    case '[': innerLevel++; break;
                    case ']': innerLevel--; break;
                    case ',': if (innerLevel == 0) comma = i; break;
                    default: break;
                }
            }
            var leftStr = str.Substring(1, comma - 1);
            var rightStr = str.Substring(comma + 1, str.Length - comma - 2);
            var left = Number.Parse(leftStr);
            var right = Number.Parse(rightStr);
            return new Number(left, right);
        }

        internal int Magnitude()
        {
            if (_value != null) return _value.Value;
            return 3*_left.Magnitude() + 2 * _right.Magnitude();
        }

        internal void Reduce()
        {
            var needToReduce = true;

            while (needToReduce)
            {
                (_, _, needToReduce) = Explode(1);
                if (needToReduce) continue;
                needToReduce = SplitHigh(out var _);
            }
        }

        private bool SplitHigh(out Number afterChange)
        {
            afterChange = this;
            if (_value.HasValue)
            {
                if (_value.Value < 10) return false;

                var l = (int)Math.Floor(_value.Value / 2.0);
                afterChange = new Number(new Number(l), new Number(_value.Value - l));
                return true;
            }

            if (_left.SplitHigh(out var left))
            {
                _left = left;
                return true;
            }
            if (_right.SplitHigh(out var right))
            {
                _right = right;
                return true;
            }
            return false;

        }

        private (int left, int right, bool needToReduce) Explode(int level)
        {
            if (_value.HasValue) return (0, 0, false);

            if (level == 5)
            {
                var leftValue = _left._value ?? throw new Exception();
                var rightValue = _right._value ?? throw new Exception();
                return (leftValue, rightValue, true);
            }

            var (left, right, needToReduce) = _left.Explode(level+1);
            if (needToReduce)
            {
                if (level == 4) _left = new Number(0);
                _right.IncreaseLeft(right);
                return (left, 0, needToReduce);
            }
            (left, right, needToReduce) = _right.Explode(level+1);
            if (needToReduce)
            {
                if (level == 4) _right = new Number(0);
                _left.IncreaseRight(left);
                return (0, right, needToReduce);
            }
            return (left, right, needToReduce);

        }

        private void IncreaseRight(int value)
        {
            if (value == 0)
            {
                return;
            }
            if (_value.HasValue) _value += value;
            else _right.IncreaseRight(value);
        }

        private void IncreaseLeft(int value)
        {
            if (value == 0)
            {
                return;
            }
            if (_value.HasValue) _value += value;
            else _left.IncreaseLeft(value);
        }

        public override string ToString()
        {
            if (_value != null)
            {
                return _value.ToString();
            }

            return $"[{_left.ToString()},{_right.ToString()}]";
        }
    }
}