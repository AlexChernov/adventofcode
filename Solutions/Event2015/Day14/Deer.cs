namespace AdventOfCode.Solutions.Event2015.Day14
{
    using System;

    internal class Deer
    {
        public string Name { get; private set; }
        public int Speed { get; private set; }
        public int FlightTime { get; private set; }
        public int RestTime { get; private set; }
        public int Score { get; set; }

        public Deer(string name, int speed, int flightTime, int restTime)
        {
            this.Name = name;
            this.Speed = speed;
            this.FlightTime = flightTime;
            this.RestTime = restTime;
        }

        public int CalcDistance(int time)
        {
            var cycle = this.RestTime + this.FlightTime;
            var cyclesCount = time / cycle;
            var flightTime = cyclesCount * this.FlightTime;
            flightTime += Math.Min(this.FlightTime, time - (cyclesCount * cycle));

            return flightTime * this.Speed;
        }

        public int TimeWhenDistance(int dist, bool strictlyGreater = false)
        {
            dist += strictlyGreater ? 1 : 0;
            var fullFlightTime = (int)Math.Ceiling(dist / (decimal)this.Speed);

            var cycle = this.RestTime + this.FlightTime;
            var cyclesCount = (fullFlightTime - 1) / this.FlightTime;
            var fullTime = (cyclesCount * cycle) + fullFlightTime - (cyclesCount * this.FlightTime);

            return fullTime;
        }

        public int NextSleep(int time)
        {
            var cycle = this.RestTime + this.FlightTime;
            var cyclesCount = time / cycle;

            return Math.Max(time, (cyclesCount * cycle) + this.FlightTime);
        }
    }
}