namespace AdventOfCode.Solutions.Event2015.Day16
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic of the Day 16.
    /// </summary>
    public class Day16 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HasVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            IEnumerable<Aunt> aunts = this.InitAunts(input);

            var target = new Aunt()
            {
                Children = 3,
                Cats = 7,
                Samoyeds = 2,
                Pomeranians = 3,
                Akitas = 0,
                Vizslas = 0,
                Goldfish = 5,
                Trees = 3,
                Cars = 2,
                Perfumes = 1,
            };

            aunts = aunts.Where(aunt =>
                (!aunt.Children.HasValue || aunt.Children == target.Children) &&
                (!aunt.Cats.HasValue || aunt.Cats == target.Cats) &&
                (!aunt.Samoyeds.HasValue || aunt.Samoyeds == target.Samoyeds) &&
                (!aunt.Pomeranians.HasValue || aunt.Pomeranians == target.Pomeranians) &&
                (!aunt.Akitas.HasValue || aunt.Akitas == target.Akitas) &&
                (!aunt.Vizslas.HasValue || aunt.Vizslas == target.Vizslas) &&
                (!aunt.Goldfish.HasValue || aunt.Goldfish == target.Goldfish) &&
                (!aunt.Trees.HasValue || aunt.Trees == target.Trees) &&
                (!aunt.Cars.HasValue || aunt.Cars == target.Cars) &&
                (!aunt.Perfumes.HasValue || aunt.Perfumes == target.Perfumes));

            yield return aunts.First().Name.ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            IEnumerable<Aunt> aunts = this.InitAunts(input);

            var target = new Aunt()
            {
                Children = 3,
                Cats = 7,
                Samoyeds = 2,
                Pomeranians = 3,
                Akitas = 0,
                Vizslas = 0,
                Goldfish = 5,
                Trees = 3,
                Cars = 2,
                Perfumes = 1,
            };

            aunts = aunts.Where(aunt =>
                (!aunt.Cats.HasValue || aunt.Cats.Value > target.Cats.Value) &&
                (!aunt.Trees.HasValue || aunt.Trees.Value > target.Trees.Value) &&
                (!aunt.Pomeranians.HasValue || aunt.Pomeranians < target.Pomeranians) &&
                (!aunt.Goldfish.HasValue || aunt.Goldfish < target.Goldfish) &&
                (!aunt.Children.HasValue || aunt.Children == target.Children) &&
                (!aunt.Samoyeds.HasValue || aunt.Samoyeds == target.Samoyeds) &&
                (!aunt.Akitas.HasValue || aunt.Akitas == target.Akitas) &&
                (!aunt.Vizslas.HasValue || aunt.Vizslas == target.Vizslas) &&
                (!aunt.Cars.HasValue || aunt.Cars == target.Cars) &&
                (!aunt.Perfumes.HasValue || aunt.Perfumes == target.Perfumes));

            yield return aunts.First().Name.ToString();
        }

        private List<Aunt> InitAunts(string input)
        {
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var aunts = new List<Aunt>();
            var regexp = new Regex(@"Sue (?<name>\d+): ((children: (?<children>\d+))|(cats: (?<cats>\d+))|(samoyeds: (?<samoyeds>\d+))|(pomeranians: (?<pomeranians>\d+))|(akitas: (?<akitas>\d+))|(vizslas: (?<vizslas>\d+))|(goldfish: (?<goldfish>\d+))|(trees: (?<trees>\d+))|(cars: (?<cars>\d+))|(perfumes: (?<perfumes>\d+))|, )+");

            foreach (var line in lines)
            {
                var match = regexp.Match(line);

                if (match.Success)
                {
                    aunts.Add(new Aunt()
                    {
                        Name = match.Groups["name"].Value,
                        Children = match.Groups["children"].Success ? int.Parse(match.Groups["children"].Value) : default(int?),
                        Cats = match.Groups["cats"].Success ? int.Parse(match.Groups["cats"].Value) : default(int?),
                        Samoyeds = match.Groups["samoyeds"].Success ? int.Parse(match.Groups["samoyeds"].Value) : default(int?),
                        Pomeranians = match.Groups["pomeranians"].Success ? int.Parse(match.Groups["pomeranians"].Value) : default(int?),
                        Akitas = match.Groups["akitas"].Success ? int.Parse(match.Groups["akitas"].Value) : default(int?),
                        Vizslas = match.Groups["vizslas"].Success ? int.Parse(match.Groups["vizslas"].Value) : default(int?),
                        Goldfish = match.Groups["goldfish"].Success ? int.Parse(match.Groups["goldfish"].Value) : default(int?),
                        Trees = match.Groups["trees"].Success ? int.Parse(match.Groups["trees"].Value) : default(int?),
                        Cars = match.Groups["cars"].Success ? int.Parse(match.Groups["cars"].Value) : default(int?),
                        Perfumes = match.Groups["perfumes"].Success ? int.Parse(match.Groups["perfumes"].Value) : default(int?),
                    });
                }
            }

            return aunts;
        }
    }
}
