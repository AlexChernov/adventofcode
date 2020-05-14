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
        public bool HaveVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            IEnumerable<Aunt> aunts = this.InitAunts(input);

            var target = new Aunt()
            {
                children = 3,
                cats = 7,
                samoyeds = 2,
                pomeranians = 3,
                akitas = 0,
                vizslas = 0,
                goldfish = 5,
                trees = 3,
                cars = 2,
                perfumes = 1,
            };

            aunts = aunts.Where(aunt =>
                (!aunt.children.HasValue || aunt.children == target.children) &&
                (!aunt.cats.HasValue || aunt.cats == target.cats) &&
                (!aunt.samoyeds.HasValue || aunt.samoyeds == target.samoyeds) &&
                (!aunt.pomeranians.HasValue || aunt.pomeranians == target.pomeranians) &&
                (!aunt.akitas.HasValue || aunt.akitas == target.akitas) &&
                (!aunt.vizslas.HasValue || aunt.vizslas == target.vizslas) &&
                (!aunt.goldfish.HasValue || aunt.goldfish == target.goldfish) &&
                (!aunt.trees.HasValue || aunt.trees == target.trees) &&
                (!aunt.cars.HasValue || aunt.cars == target.cars) &&
                (!aunt.perfumes.HasValue || aunt.perfumes == target.perfumes));

            yield return aunts.First().name.ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            IEnumerable<Aunt> aunts = this.InitAunts(input);

            var target = new Aunt()
            {
                children = 3,
                cats = 7,
                samoyeds = 2,
                pomeranians = 3,
                akitas = 0,
                vizslas = 0,
                goldfish = 5,
                trees = 3,
                cars = 2,
                perfumes = 1,
            };

            aunts = aunts.Where(aunt =>
                (!aunt.cats.HasValue || aunt.cats.Value > target.cats.Value) &&
                (!aunt.trees.HasValue || aunt.trees.Value > target.trees.Value) &&
                (!aunt.pomeranians.HasValue || aunt.pomeranians < target.pomeranians) &&
                (!aunt.goldfish.HasValue || aunt.goldfish < target.goldfish) &&
                (!aunt.children.HasValue || aunt.children == target.children) &&
                (!aunt.samoyeds.HasValue || aunt.samoyeds == target.samoyeds) &&
                (!aunt.akitas.HasValue || aunt.akitas == target.akitas) &&
                (!aunt.vizslas.HasValue || aunt.vizslas == target.vizslas) &&
                (!aunt.cars.HasValue || aunt.cars == target.cars) &&
                (!aunt.perfumes.HasValue || aunt.perfumes == target.perfumes));

            yield return aunts.First().name.ToString();
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
                        name = match.Groups["name"].Value,
                        children = match.Groups["children"].Success ? int.Parse(match.Groups["children"].Value) : default(int?),
                        cats = match.Groups["cats"].Success ? int.Parse(match.Groups["cats"].Value) : default(int?),
                        samoyeds = match.Groups["samoyeds"].Success ? int.Parse(match.Groups["samoyeds"].Value) : default(int?),
                        pomeranians = match.Groups["pomeranians"].Success ? int.Parse(match.Groups["pomeranians"].Value) : default(int?),
                        akitas = match.Groups["akitas"].Success ? int.Parse(match.Groups["akitas"].Value) : default(int?),
                        vizslas = match.Groups["vizslas"].Success ? int.Parse(match.Groups["vizslas"].Value) : default(int?),
                        goldfish = match.Groups["goldfish"].Success ? int.Parse(match.Groups["goldfish"].Value) : default(int?),
                        trees = match.Groups["trees"].Success ? int.Parse(match.Groups["trees"].Value) : default(int?),
                        cars = match.Groups["cars"].Success ? int.Parse(match.Groups["cars"].Value) : default(int?),
                        perfumes = match.Groups["perfumes"].Success ? int.Parse(match.Groups["perfumes"].Value) : default(int?),
                    });
                }
            }

            return aunts;
        }
    }
}
