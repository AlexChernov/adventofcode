namespace AdventOfCode.Solutions.Event2015.Day14
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Incapsulates the logic of race state.
    /// </summary>
    internal class RaceState
    {
        private readonly IEnumerable<Deer> deers;
        private IEnumerable<IGrouping<bool, Deer>> groups;

        /// <summary>
        /// Initializes a new instance of the <see cref="RaceState"/> class.
        /// </summary>
        /// <param name="deers">The deers competitors of race.</param>
        public RaceState(IEnumerable<Deer> deers)
        {
            this.deers = deers;
            this.groups = this.deers.GroupBy(d => false);
        }

        /// <summary>
        /// Gets or sets current time.
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// Gets current leaders.
        /// </summary>
        public IEnumerable<Deer> Leaders
        {
            get => this.groups.FirstOrDefault(g => g.Key) ?? Enumerable.Empty<Deer>();
            private set { }
        }

        /// <summary>
        /// Gets current persecutors.
        /// </summary>
        public IEnumerable<Deer> Persecutors
        {
            get => this.groups.FirstOrDefault(g => !g.Key);
            private set { }
        }

        /// <summary>
        /// Calculates and gets estimated leaders by provided time in future.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>The enumeration of future leaders.</returns>
        public IEnumerable<Deer> GetEstimatedLeaders(int time)
        {
            var max = this.deers.Max(deer => deer.CalcDistance(time));

            return this.deers.Where(deer => deer.CalcDistance(time) == max);
        }

        /// <summary>
        /// Moves state to provided time.
        /// </summary>
        /// <param name="nextTime">The next time.</param>
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

        private IEnumerable<IGrouping<bool, Deer>> GetGroupings(int time)
        {
            var max = this.deers.Max(deer => deer.CalcDistance(time));

            return this.deers.GroupBy(deer => deer.CalcDistance(time) == max);
        }
    }
}