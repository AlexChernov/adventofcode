namespace AdventOfCode.Solutions.Event2015.Day14
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic of the Day 14.
    /// </summary>
    public class Day14 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HaveVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var deers = this.InitDeers(input);
            var time = 2503;

            var max = deers.Max(deer => deer.CalcDistance(time));

            yield return max.ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var deers = this.InitDeers(input);

            var state = new State(deers);
            state.NextState(1);

            var maxTime = 2503;

            while (state.Time < maxTime)
            {
                var leaders = state.Leaders;
                var sleepTime = state.Leaders.Min(deer => deer.NextSleep(state.Time));

                if (sleepTime == state.Time || state.GetEstimatedLeaders(sleepTime).SequenceEqual(leaders))
                {
                    var dist = leaders.First().CalcDistance(sleepTime);
                    var nextTime2 = leaders.Min(deer => deer.TimeWhenDistance(dist, strictlyGreater: true));
                    var nextTime3 = state.Persecutors?.Min(deer => deer.TimeWhenDistance(dist)) ?? int.MaxValue;

                    var nextTime = Math.Min(Math.Min(nextTime2, nextTime3), maxTime + 1);

                    state.NextState(nextTime);
                }
                else
                {
                    var nextTime = state.Time + 1;
                    state.NextState(nextTime);
                }
            }

            var max1 = state.Leaders.Max(deer => deer.Score);
            var max2 = state.Persecutors?.Max(deer => deer.Score) ?? int.MinValue;

            yield return Math.Max(max1, max2).ToString();
        }

        private IEnumerable<Deer> InitDeers(string input)
        {
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var regex = new Regex(@"(?<name>\S+) can fly (?<speed>\d+) km/s for (?<flightTime>\d+) seconds, but then must rest for (?<restTIme>\d+) seconds.");
            var deers = new LinkedList<Deer>();
            foreach (var line in lines)
            {
                var match = regex.Match(line);
                if (match.Success)
                {
                    var name = match.Groups["name"].Value;
                    var speed = int.Parse(match.Groups["speed"].Value);
                    var flightTime = int.Parse(match.Groups["flightTime"].Value);
                    var restTIme = int.Parse(match.Groups["restTIme"].Value);

                    deers.AddLast(new Deer(name, speed, flightTime, restTIme));
                }
            }

            return deers;
        }
    }
}
