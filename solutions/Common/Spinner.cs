namespace AdventOfCode.Solutions.Common
{
    public class Spinner
    {
        private int counter = 0;

        public string State { get; private set; }

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
