namespace BenchmarkRunner
{
    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The entry point of program.
        /// </summary>
        public static void Main()
        {
            BenchmarkDotNet.Running.BenchmarkRunner.Run<BenchmarkRunner>();
        }
    }
}