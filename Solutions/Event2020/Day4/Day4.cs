namespace AdventOfCode.Solutions.Event2020.Day4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic for Day 19 of event.
    /// </summary>
    public class Day4 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HasVisualization() => false;

        private Dictionary<string, Func<string, bool>> rules = new Dictionary<string, Func<string, bool>>()
        {
            { "byr", (value) =>
                {
                    if (int.TryParse(value, out int num) && 1920 <= num && num <= 2002)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            },
            { "iyr", (value) =>
                {
                    if (int.TryParse(value, out int num) && 2010 <= num && num <= 2020)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            },
            { "eyr", (value) =>
                {
                    if (int.TryParse(value, out int num) && 2020 <= num && num <= 2030)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            },
            { "hgt", (value) =>
                {
                    var regex = new Regex(@"^(?<value>\d+)((?<cm>cm)|(?<in>in))$");
                    var match = regex.Match(value);
                    if (!match.Success)
                    {
                        return false;
                    }
                    var num = int.Parse(match.Groups["value"].Value);
                    var isCm = match.Groups["cm"].Success;

                    return isCm ? ( 150 <= num && num <= 193 ) : ( 59 <= num && num <= 76 );
                }
            },
            { "hcl", (value) =>
                {
                    var regex = new Regex(@"^#(?<value>[0-9a-f]{6})$");
                    var match = regex.Match(value);

                    return match.Success;
                }
            },
            { "ecl", (value) =>
                {
                    var set = new HashSet<string>()
                    {
                        "amb", "blu", "brn", "gry", "grn", "hzl", "oth"
                    };

                    return set.Contains(value);
                }
            },
            { "pid", (value) =>
                {
                    var regex = new Regex(@"^(?<value>\d{9})$");
                    var match = regex.Match(value);

                    return match.Success;
                }
            },
        };

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var normalize = input.Replace("\n\n", "<separator>");
            normalize = normalize.Replace("\n", " ");
            normalize = normalize.Replace("<separator>", Environment.NewLine);
            var values = normalize.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            var regex = new Regex(@"(?<key>\w{3}):(?<value>.+)");
            var count = 0;
            foreach (var value in values)
            {
                var kvps = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var missingMandatory = new HashSet<string>()
                {
                    "byr",
                    "iyr",
                    "eyr",
                    "hgt",
                    "hcl",
                    "ecl",
                    "pid",
                };

                foreach (var kvp in kvps)
                {
                    var match = regex.Match(kvp);
                    if (match.Success)
                    {
                        var key = match.Groups["key"].Value;
                        var to = match.Groups["value"].Value;

                        if (missingMandatory.Contains(key))
                        {
                            missingMandatory.Remove(key);
                        }
                    }
                    else
                    {
                        throw new Exception("parsing error.");
                    }
                }

                if (!missingMandatory.Any())
                {
                    ++count;
                }
                else
                { }
            }

            yield return count.ToString();
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var normalize = input.Replace("\n\n", "<separator>");
            normalize = normalize.Replace("\n", " ");
            normalize = normalize.Replace("<separator>", Environment.NewLine);
            var values = normalize.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            var regex = new Regex(@"(?<key>\w{3}):(?<value>.+)");
            var count = 0;
            foreach (var entry in values)
            {
                var kvps = entry.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var missingMandatory = new HashSet<string>()
                {
                    "byr",
                    "iyr",
                    "eyr",
                    "hgt",
                    "hcl",
                    "ecl",
                    "pid",
                };

                foreach (var kvp in kvps)
                {
                    var match = regex.Match(kvp);
                    if (match.Success)
                    {
                        var key = match.Groups["key"].Value;
                        var value = match.Groups["value"].Value;

                        if (missingMandatory.Contains(key))
                        {
                            if (rules[key](value))
                            {
                                missingMandatory.Remove(key);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("parsing error.");
                    }
                }

                if (!missingMandatory.Any())
                {
                    ++count;
                }
            }

            yield return count.ToString();
        }

    }
}
