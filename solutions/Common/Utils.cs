using System.Collections.Generic;

namespace AdventOfCode.Solutions.Common
{
    public class Utils
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
    }
}
