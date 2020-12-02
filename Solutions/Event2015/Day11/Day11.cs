namespace AdventOfCode.Solutions.Event2015.Day11
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Security.Cryptography;
    using AdventOfCode.Solutions.Common;

    /// <summary>
    /// Incapsulates logic of the Day 11.
    /// </summary>
    public class Day11 : IAdventOfCodeDayRunner
    {
        /// <inheritdoc/>
        public bool HasVisualization() => false;

        /// <inheritdoc/>
        public IEnumerable<string> RunTask1(string input, bool shouldVisualise)
        {
            var chArr = input.ToCharArray();

            var restrictedChars = new HashSet<char>() { 'i', 'o', 'l' };

            chArr = this.GetNextPassword(chArr, restrictedChars);

            yield return new string(chArr);
        }

        /// <inheritdoc/>
        public IEnumerable<string> RunTask2(string input, bool shouldVisualise)
        {
            var chArr = input.ToCharArray();

            var restrictedChars = new HashSet<char>() { 'i', 'o', 'l' };

            chArr = this.GetNextPassword(chArr, restrictedChars);
            this.Increment(chArr, chArr.Length - 1);
            chArr = this.GetNextPassword(chArr, restrictedChars);

            yield return new string(chArr);
        }

        private char[] GetNextPassword(char[] chArr, HashSet<char> restrictedChars)
        {
            for (var i = 0; i < chArr.Length; ++i)
            {
                if (restrictedChars.Contains(chArr[i]))
                {
                    this.IncrementWithRounding(chArr, i);

                    return this.GetNextPassword(chArr, restrictedChars);
                }
            }

            var firstPairIndex = 0;
            var secondPairIndex = 0;
            var tripletIndex = 0;
            if (chArr[1] == chArr[0])
            {
                firstPairIndex = 1;
            }

            for (var i = 2; i < chArr.Length; ++i)
            {
                if (chArr[i] == chArr[i - 1])
                {
                    if (firstPairIndex == 0)
                    {
                        firstPairIndex = i;
                    }
                    else if (secondPairIndex == 0 && chArr[i] != chArr[firstPairIndex])
                    {
                        secondPairIndex = i;
                    }
                }

                if (tripletIndex == 0 && chArr[i] - 1 == chArr[i - 1] && chArr[i - 1] - 1 == chArr[i - 2])
                {
                    tripletIndex = i;
                }
            }

            if (tripletIndex > 0)
            {
                if (firstPairIndex > 0)
                {
                    if (secondPairIndex > 0)
                    {
                        // All good.
                        return chArr;
                    }
                    else
                    {
                        // Missing one pair.
                        if (chArr[^1] < chArr[^2])
                        {
                            chArr[^1] = chArr[^2];

                            return this.GetNextPassword(chArr, restrictedChars);
                        }
                        else
                        {
                            this.IncrementWithRounding(chArr, chArr.Length - 2);

                            return this.GetNextPassword(chArr, restrictedChars);
                        }
                    }
                }
                else
                {
                    // Missing two pairs.
                    if (chArr[^3] < chArr[^4])
                    {
                        chArr[^3] = chArr[^4];
                        chArr[^1] = 'a';
                        chArr[^2] = 'a';

                        return this.GetNextPassword(chArr, restrictedChars);
                    }
                    else
                    {
                        this.IncrementWithRounding(chArr, chArr.Length - 4);

                        return this.GetNextPassword(chArr, restrictedChars);
                    }
                }
            }
            else
            {
                if (firstPairIndex > 0)
                {
                    if (secondPairIndex > 0)
                    {
                        // Missing triplet.
                        if (chArr[^3] + 2 <= 'z' && this.CompareArrays(new char[] { chArr[^2], chArr[^1] }, new char[] { (char)(chArr[^3] + 1), (char)(chArr[^3] + 2) }) < 0)
                        {
                            chArr[^2] = (char)(chArr[^3] + 1);
                            chArr[^1] = (char)(chArr[^3] + 2);

                            return this.GetNextPassword(chArr, restrictedChars);
                        }
                        else
                        {
                            this.IncrementWithRounding(chArr, chArr.Length - 3);

                            return this.GetNextPassword(chArr, restrictedChars);
                        }
                    }
                    else
                    {
                        // Missing triplet and pair.
                        if (chArr[^4] + 2 <= 'z' && this.CompareArrays(new char[] { chArr[^3], chArr[^2], chArr[^1] }, new char[] { chArr[^4], (char)(chArr[^4] + 1), (char)(chArr[^4] + 2), }) < 0)
                        {
                            chArr[^3] = chArr[^4];
                            chArr[^2] = (char)(chArr[^4] + 1);
                            chArr[^1] = (char)(chArr[^4] + 2);

                            return this.GetNextPassword(chArr, restrictedChars);
                        }
                        else if (chArr[^4] + 2 <= 'z' && this.CompareArrays(new char[] { chArr[^3], chArr[^2], chArr[^1] }, new char[] { (char)(chArr[^4] + 1), (char)(chArr[^4] + 2), (char)(chArr[^4] + 2), }) < 0)
                        {
                            chArr[^3] = (char)(chArr[^4] + 1);
                            chArr[^2] = (char)(chArr[^4] + 2);
                            chArr[^1] = (char)(chArr[^4] + 2);

                            return this.GetNextPassword(chArr, restrictedChars);
                        }
                        else
                        {
                            this.IncrementWithRounding(chArr, chArr.Length - 4);

                            return this.GetNextPassword(chArr, restrictedChars);
                        }
                    }
                }
                else
                {
                    // Missing triplet and two pairs.
                    if (chArr[^5] + 2 <= 'z' && this.CompareArrays(new char[] { chArr[^4], chArr[^3], chArr[^2], chArr[^1] }, new char[] { chArr[^5], (char)(chArr[^5] + 1), (char)(chArr[^5] + 2), (char)(chArr[^5] + 2), }) < 0)
                    {
                        chArr[^4] = chArr[^5];
                        chArr[^3] = (char)(chArr[^5] + 1);
                        chArr[^2] = (char)(chArr[^5] + 2);
                        chArr[^1] = (char)(chArr[^5] + 2);

                        return this.GetNextPassword(chArr, restrictedChars);
                    }
                    else
                    {
                        this.IncrementWithRounding(chArr, chArr.Length - 5);

                        return this.GetNextPassword(chArr, restrictedChars);
                    }
                }
            }
        }

        private int CompareArrays(char[] left, char[] right)
        {
            return ((IStructuralComparable)left).CompareTo(right, StructuralComparisons.StructuralComparer);
        }

        private void IncrementWithRounding(char[] chArr, int i)
        {
            for (var j = i + 1; j < chArr.Length; ++j)
            {
                chArr[j] = 'a';
            }

            this.Increment(chArr, i);
        }

        private void Increment(char[] chArr, int i)
        {
            if (chArr[i] == 'z')
            {
                chArr[i] = 'a';
                this.Increment(chArr, i - 1);

                return;
            }

            chArr[i] = (char)(chArr[i] + 1);
        }
    }
}
