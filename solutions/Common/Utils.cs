using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace AdventOfCode.Solutions.Common
{
    public static class Utils
    {
        public static int IndexOfFirstGreaterValue(IList<int> arr, int target)
        {
            int start = 0, end = arr.Count - 1;

            int ans = -1;
            while (start <= end)
            {
                int mid = (start + end) / 2;

                // Move to right side if target is 
                // greater. 
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

        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);

            return result;
        }

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
    }
}
