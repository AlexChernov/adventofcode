namespace AdventOfCode.Solutions.Event2015.Day15
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic of the Day 15.
    /// </summary>
    public class Day15 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HasVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            List<Ingredient> ingredients = InitIngredients(lines);

            var score = this.BestScore(ingredients, new int[ingredients.Count], 100, 0, 0);

            yield return score.ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            List<Ingredient> ingredients = InitIngredients(lines);

            var score = this.BestScore(ingredients, new int[ingredients.Count], 100, 0, 0, isPart2: true);

            yield return score.ToString();
        }

        private static List<Ingredient> InitIngredients(string[] lines)
        {
            var regexp = new Regex(@"(?<name>\w+): capacity (?<capacity>-?\d+), durability (?<durability>-?\d+), flavor (?<flavor>-?\d+), texture (?<texture>-?\d+), calories (?<calories>-?\d+)");

            var ingredients = new List<Ingredient>(lines.Length);
            foreach (var line in lines)
            {
                var match = regexp.Match(line);

                if (match.Success)
                {
                    var name = match.Groups["name"].Value;
                    var capacity = int.Parse(match.Groups["capacity"].Value);
                    var durability = int.Parse(match.Groups["durability"].Value);
                    var flavor = int.Parse(match.Groups["flavor"].Value);
                    var texture = int.Parse(match.Groups["texture"].Value);
                    var calories = int.Parse(match.Groups["calories"].Value);

                    ingredients.Add(new Ingredient()
                    {
                        Name = name,
                        Capacity = capacity,
                        Durability = durability,
                        Flavor = flavor,
                        Texture = texture,
                        Calories = calories,
                    });
                }
            }

            return ingredients;
        }

        private long BestScore(List<Ingredient> ingredients, int[] ingredientAmounts, int restAmount, int count, long res, bool isPart2 = false)
        {
            if (ingredients.Count == 0)
            {
                return 0;
            }

            if (count == ingredients.Count - 1)
            {
                ingredientAmounts[count] = restAmount;

                var capacity = 0;
                var durability = 0;
                var flavor = 0;
                var texture = 0;
                var calories = 0;
                for (var i = 0; i < ingredients.Count; ++i)
                {
                    var ingredient = ingredients[i];
                    var amount = ingredientAmounts[i];
                    capacity += ingredient.Capacity * amount;
                    durability += ingredient.Durability * amount;
                    flavor += ingredient.Flavor * amount;
                    texture += ingredient.Texture * amount;
                    calories += ingredient.Calories * amount;
                }

                capacity = Math.Max(0, capacity);
                durability = Math.Max(0, durability);
                flavor = Math.Max(0, flavor);
                texture = Math.Max(0, texture);

                if (isPart2)
                {
                    return calories == 500 ? Math.Max(res, capacity * durability * flavor * texture) : res;
                }

                return Math.Max(res, capacity * durability * flavor * texture);
            }

            for (int i = 0; i <= restAmount; ++i)
            {
                ingredientAmounts[count] = i;
                res = this.BestScore(ingredients, ingredientAmounts, restAmount - i, count + 1, res, isPart2);
            }

            return res;
        }
    }
}
