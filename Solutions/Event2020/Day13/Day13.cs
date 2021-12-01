namespace AdventOfCode.Solutions.Event2020.Day13
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode.Solutions.Common;
    using Microsoft.Win32.SafeHandles;

    /// <summary>
    /// Incapsulates logic for Day 11 of event.
    /// </summary>
    public class Day13 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HasVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var time = long.Parse(values[0]);
            var ids = values[1].Split(new char[] { ',', 'x' }, StringSplitOptions.RemoveEmptyEntries).Select(long.Parse);

            long minReminder = long.MaxValue;
            long idWithMin = 0;
            foreach (var id in ids)
            {
                var reminder = 0L;
                if (time % id != 0)
                {
                    var div = time / id;
                    reminder = ((div + 1) * id) - time;
                }

                if (reminder < minReminder)
                {
                    minReminder = reminder;
                    idWithMin = id;
                }
            }

            yield return (minReminder * idWithMin).ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var ids = GetIds(values).ToArray();

            var lcm = ids[0];

            for (long i = 1; i < ids.Length; ++i)
            {
                lcm = CalcLcm(lcm, ids[i]);
            }

            var count = lcm.offset;

            yield return count.ToString();
        }

        private (long id, long offset) CalcLcm((long id, long offset) lcm, (long id, long offset) next)
        {
            var found = false;
            long offset = 0; 

            for (long i = 0; i < next.id && !found; ++i)
            {
                offset = i * lcm.id + lcm.offset;
                if (offset % next.id == next.offset)
                {
                    found = true;
                }
            }

            if (!found)
            {
                throw new Exception();
            }

            return (Utils.LCM(lcm.id, next.id), offset);
        }

        private IEnumerable<(long id, long offset)> GetIds(string[] ids)
        {
            for (var i = 0; i < ids.Length; ++i)
            {
                var idStr = ids[i];
                if (idStr != "x")
                {
                    var id = long.Parse(idStr);
                    var offset = i % id;
                    offset = offset == 0 ? 0 : id - offset;

                    yield return (id, offset);
                }
            }
        }
    }
}
