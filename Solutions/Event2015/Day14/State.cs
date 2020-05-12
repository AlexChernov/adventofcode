namespace AdventOfCode.Solutions.Event2015.Day14
{
    using System.Collections.Generic;
    using System.Linq;

    internal class State
    {
        private IEnumerable<Deer> deers;
        private IEnumerable<IGrouping<bool, Deer>> groups;

        public State(IEnumerable<Deer> deers)
        {
            this.deers = deers;
            this.groups = this.deers.GroupBy(d => false);
        }

        public int Time { get; set; }

        public IEnumerable<Deer> Leaders
        {
            get => this.groups.FirstOrDefault(g => g.Key) ?? Enumerable.Empty<Deer>();
            private set { }
        }

        public IEnumerable<Deer> Persecutors
        {
            get => this.groups.FirstOrDefault(g => !g.Key);
            private set { }
        }

        public IEnumerable<Deer> GetEstimatedLeaders(int time)
        {
            var max = this.deers.Max(deer => deer.CalcDistance(time));

            return this.deers.Where(deer => deer.CalcDistance(time) == max);
        }

        private IEnumerable<IGrouping<bool, Deer>> GetGroupings(int time)
        {
            var max = this.deers.Max(deer => deer.CalcDistance(time));

            return this.deers.GroupBy(deer => deer.CalcDistance(time) == max);
        }

        public void NextState(int nextTime)
        {
            var score = nextTime - this.Time;
            foreach (var leader in this.Leaders)
            {
                leader.Score += score;
            }

            this.groups = this.GetGroupings(nextTime);
            this.Time = nextTime;
        }
    }
}