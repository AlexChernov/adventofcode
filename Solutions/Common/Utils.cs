namespace AdventOfCode.Solutions.Common
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Utils.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Gets the index of first element greater than specified target value.
        /// </summary>
        /// <param name="arr">The array.</param>
        /// <param name="target">The target value.</param>
        /// <returns>The index of first element greater than specified target value.</returns>
        public static int IndexOfFirstGreaterValue(IList<int> arr, int target)
        {
            int start = 0, end = arr.Count - 1;

            int ans = -1;
            while (start <= end)
            {
                int mid = (start + end) / 2;

                // Move to right side if target is greater.
                if (arr[mid] < target)
                {
                    start = mid + 1;
                }

                // Move left side.
                else
                {
                    ans = mid;
                    end = mid - 1;
                }
            }

            return ans;
        }

        /// <summary>
        /// Gets the subarray of specified array starting from specified index with specified length.
        /// </summary>
        /// <typeparam name="T">The type of data.</typeparam>
        /// <param name="data">The array.</param>
        /// <param name="index">The starting index.</param>
        /// <param name="length">The length of subarray.</param>
        /// <returns>The subarray.</returns>
        public static T[] Subarray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);

            return result;
        }

        /// <summary>
        /// Determines whether an positin in bound of data.
        /// </summary>
        /// <typeparam name="T">The type of data.</typeparam>
        /// <param name="data">The 2D array of data.</param>
        /// <param name="xy">The instance of <see cref="X_Y"/> with position to check.</param>
        /// <returns>True if the positon in bound; otherwise False is returned.</returns>
        public static bool InBound<T>(this T[,] data, X_Y xy)
        {
            return xy.X >= 0 && xy.X < data.GetLength(0) && xy.Y >= 0 && xy.Y < data.GetLength(1);
        }

        /// <summary>
        /// Determines whether an positin in bound of data.
        /// </summary>
        /// <typeparam name="T">The type of data.</typeparam>
        /// <param name="data">The 2D array of data.</param>
        /// <param name="x">The x coordinate of position.</param>
        /// <param name="y">The y coordinate of position.</param>
        /// <returns>True if the positon in bound; otherwise False is returned.</returns>
        public static bool InBound<T>(this T[,] data, int x, int y)
        {
            return x >= 0 && x < data.GetLength(0) && y >= 0 && y < data.GetLength(1);
        }

        /// <summary>
        /// Calculates factorial of specified number.
        /// </summary>
        /// <param name="n">The number.</param>
        /// <returns>The factorial of specified number.</returns>
        public static long Factorial(long n)
        {
            long ret = 1;

            while (n > 1)
            {
                ret *= n;
                n--;
            }

            return ret;
        }

        /// <summary>
        /// Calculates modulo of value by base.
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="base">The base.</param>
        /// <returns>The modulo.</returns>
        public static int Mod(int value, int @base)
        {
            return ((value % @base) + @base) % @base;
        }

        /// <summary>
        /// Calculates GCD.
        /// </summary>
        /// <param name="first">First value.</param>
        /// <param name="second">Second value.</param>
        /// <returns>The GCD of two values.</returns>
        public static long GCD(long first, long second)
        {
            if (first == 0)
            {
                return second;
            }

            return GCD(second % first, first);
        }

        /// <summary>
        /// Calculates LCM.
        /// </summary>
        /// <param name="a">First value.</param>
        /// <param name="b">Second value.</param>
        /// <returns>The LCM of two values.</returns>
        public static long LCM(long a, long b)
        {
            return (a * b) / GCD(a, b);
        }
    }
}
