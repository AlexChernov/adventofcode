namespace AdventOfCode.Solutions.Common
{
    /// <summary>
    /// The console spinner.
    /// </summary>
    public class Spinner
    {
        private int counter = 0;

        /// <summary>
        /// Gets the state of spinner.
        /// </summary>
        public string State { get; private set; }

        /// <summary>
        /// Modifies the state to imitate turn of spinner.
        /// </summary>
        public void Turn()
        {
            this.counter++;
            switch (this.counter % 4)
            {
                case 0:
                    this.State = "/";
                    this.counter = 0;
                    break;
                case 1:
                    this.State = "-";
                    break;
                case 2:
                    this.State = "\\";
                    break;
                case 3:
                    this.State = "|";
                    break;
            }
        }
    }
}
