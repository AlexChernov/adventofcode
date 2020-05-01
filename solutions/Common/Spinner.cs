using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Common
{
    public class Spinner
    {
        int counter = 0;
        public string State { get; private set; }

        public void Turn()
        {
            counter++;
            switch (counter % 4)
            {
                case 0:
                    State = "/";
                    counter = 0;
                    break;
                case 1:
                    State = "-";
                    break;
                case 2:
                    State = "\\";
                    break;
                case 3:
                    State = "|";
                    break;
            }
        }
    }
}
