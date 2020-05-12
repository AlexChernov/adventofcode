namespace AdventOfCode.Solutions.Event2015.Day12
{
    public class WordFinder
    {
        private readonly string word;
        private int currentIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="WordFinder"/> class.
        /// </summary>
        /// <param name="word"></param>
        public WordFinder(string word)
        {
            this.word = word;
            this.currentIndex = 0;
        }

        public bool NextChar(char next)
        {
            if (next == this.word[this.currentIndex])
            {
                this.currentIndex++;
            }
            else
            {
                this.currentIndex = 0;
            }

            if (this.currentIndex == this.word.Length)
            {
                this.currentIndex = 0;
                return true;
            }

            return false;
        }
    }
}
