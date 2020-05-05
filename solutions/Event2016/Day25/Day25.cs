namespace AdventOfCode.Solutions.Event2016.Day25
{
    using System.Collections.Generic;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates day 25 logic.
    /// </summary>
    public class Day25 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HaveVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var title = " is the lowest positive integer that can be used to initialize register a";
            if (!shouldVisualise)
            {
                yield return this.FastLoop(input).ToString() + title;
                yield break;
            }

            var found = false;
            var value = 1;
            var spinner = new Spinner();
            while (!found)
            {
                var compRabbit = new PrototypeComputer.PrototypeComputer(input);
                var compTurtle = new PrototypeComputer.PrototypeComputer(input);

                compRabbit.SetRegister("a", value);
                compTurtle.SetRegister("a", value);

                while (compRabbit.State.CanRun && compTurtle.State.CanRun)
                {
                    compRabbit.RunNext();
                    compRabbit.RunNext();
                    compTurtle.RunNext();

                    if (compRabbit.State.Equals(compTurtle.State))
                    {
                        // Infinite loop detected.
                        if (this.IsSignal(compRabbit.State.Out))
                        {
                            yield return "Calculating.. " + spinner.State + " " + value.ToString();
                            found = true;
                        }

                        break;
                    }
                }

                spinner.Turn();
                ++value;
            }

            yield return value.ToString() + title;
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            if (!shouldVisualise)
            {
                yield return "Congratulations! You complete this event.";
                yield break;
            }

            var drawing = new Drawing();
            string[] picture = drawing.GetDefaultPicture();
            string[] train = drawing.GetTrainAnimation();
            string[] signal = drawing.GetSignalAnimation();

            var i = 0;
            var framesCount = Utils.LCM(train.Length, signal.Length);
            var repeat = 300 / framesCount;
            framesCount = repeat == 0 ? framesCount : framesCount * repeat;

            while (i < framesCount)
            {
                this.UpdatePicture(picture, train, signal, i);
                yield return string.Join('\n', picture);
                ++i;
            }
        }

        private int FastLoop(string input)
        {
            var comp = new PrototypeComputer.PrototypeComputer(input);
            var baseValue = int.Parse(comp.State.Instructions[1].Args[0]) * int.Parse(comp.State.Instructions[2].Args[0]);

            var value = 0;

            while (value < baseValue)
            {
                value <<= 1;
                ++value;
                value <<= 1;
            }

            return value - baseValue;
        }

        private void UpdatePicture(string[] picture, string[] train, string[] signal, int i)
        {
            var signalIndex = 0;
            var trainTopIndex = 21;
            picture[signalIndex] = signal[Utils.Mod(i, signal.Length)];
            picture[trainTopIndex] = train[Utils.Mod(i, train.Length / 2) * 2];
            picture[trainTopIndex + 1] = train[(Utils.Mod(i, train.Length / 2) * 2) + 1];
        }

        private bool IsSignal(ICollection<int> output)
        {
            int last = 1;
            foreach (var value in output)
            {
                if ((last == 0 && value != 1) || (last == 1 && value != 0))
                {
                    return false;
                }

                last = last == 1 ? 0 : 1;
            }

            return true;
        }
    }
}
