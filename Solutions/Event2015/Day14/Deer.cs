namespace AdventOfCode.Solutions.Event2015.Day14
{
    using System;

    /// <summary>
    /// Incapsulates the logic of Deer.
    /// </summary>
    internal class Deer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Deer"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="speed">The speed of flight.</param>
        /// <param name="flightTime">The flight time before rest.</param>
        /// <param name="restTime">The time of rest.</param>
        public Deer(string name, int speed, int flightTime, int restTime)
        {
            this.Name = name;
            this.Speed = speed;
            this.FlightTime = flightTime;
            this.RestTime = restTime;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the speed.
        /// </summary>
        public int Speed { get; private set; }

        /// <summary>
        /// Gets the flight time before rest.
        /// </summary>
        public int FlightTime { get; private set; }

        /// <summary>
        /// Gets the time of rest.
        /// </summary>
        public int RestTime { get; private set; }

        /// <summary>
        /// Gets or sets the current score.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Calculates the distance flown by the time.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>The distance.</returns>
        public int CalcDistance(int time)
        {
            var cycle = this.RestTime + this.FlightTime;
            var cyclesCount = time / cycle;
            var flightTime = cyclesCount * this.FlightTime;
            flightTime += Math.Min(this.FlightTime, time - (cyclesCount * cycle));

            return flightTime * this.Speed;
        }

        /// <summary>
        /// Calculates time when distance is flown.
        /// </summary>
        /// <param name="distance">The distance.</param>
        /// <param name="strictlyGreater">Indicating whether ths distance should be strictly greater.</param>
        /// <returns>The time.</returns>
        public int TimeWhenDistance(int distance, bool strictlyGreater = false)
        {
            distance += strictlyGreater ? 1 : 0;
            var fullFlightTime = (int)Math.Ceiling(distance / (decimal)this.Speed);

            var cycle = this.RestTime + this.FlightTime;
            var cyclesCount = (fullFlightTime - 1) / this.FlightTime;
            var fullTime = (cyclesCount * cycle) + fullFlightTime - (cyclesCount * this.FlightTime);

            return fullTime;
        }

        /// <summary>
        /// Calculates the time of next sleep.
        /// </summary>
        /// <param name="time">The current time.</param>
        /// <returns>The time of next sleep.</returns>
        /// <remarks>If deer is sleeping at provided time returns provided time.</remarks>
        public int NextSleep(int time)
        {
            var cycle = this.RestTime + this.FlightTime;
            var cyclesCount = time / cycle;

            return Math.Max(time, (cyclesCount * cycle) + this.FlightTime);
        }
    }
}