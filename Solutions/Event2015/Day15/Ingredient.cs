namespace AdventOfCode.Solutions.Event2015.Day15
{
    /// <summary>
    /// Incapsulates the logic of ingredient.
    /// </summary>
    internal class Ingredient
    {
        /// <summary>
        /// Gets or sets name of ingredient.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the capacity of ingredient.
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Gets or sets the durability of ingredient.
        /// </summary>
        public int Durability { get; set; }

        /// <summary>
        /// Gets or sets the flavor of ingredient.
        /// </summary>
        public int Flavor { get; set; }

        /// <summary>
        /// Gets or sets the texture of ingredient.
        /// </summary>
        public int Texture { get; set; }

        /// <summary>
        /// Gets or sets the calories of ingredient.
        /// </summary>
        public int Calories { get; set; }
    }
}