using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using AdventOfCode.Solutions.Common;
using AdventOfCode.Solutions.Event2016.Day24;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace BenchmarkRunner
{
    public class BenchmarkRunner
    {
        private readonly Dictionary<char, X_Y> locations;
        private readonly Day24.MapNode[,] map;
        private string text;

        public BenchmarkRunner()
        {
            text = System.IO.File.ReadAllText(@".\input.txt");
        }

        [Benchmark]
        public object DirectAStar() => new Day24().RunTask1(text, false).First();

        [Benchmark]
        public object BFSSImplifyPlusTSP() => new Day24().RunTask2(text, false).First();
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkDotNet.Running.BenchmarkRunner.Run<BenchmarkRunner>();
        }
    }
}