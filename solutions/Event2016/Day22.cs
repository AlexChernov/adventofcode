using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode.Solutions.Event2016
{
    public class Day22
    {
        class Node
        {
            public int Used;
            public int Available;
        }

        public static string Run1(string input)
        {
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var nodes = InitNodes(lines);

            if (nodes == null)
            {
                return "Wrong input!";
            }

            var listOfAvailabe = GetAvailable(nodes);

            var result = 0;
            foreach (var node in nodes)
            {
                if (node.Used <=0 )
                {
                    continue;
                }
                var index = IndexOfFirstGreaterValue(listOfAvailabe, node.Used);

                if (index < 0)
                {
                    continue;
                }

                var count = listOfAvailabe.Count - index;
                if (node.Available >= node.Used)
                {
                    count--;
                }
                result += count;
            }


            return result.ToString();
        }

        private static List<int> GetAvailable(Node[,] arr)
        {
            var list = new List<int>(arr.Length);
            foreach (var node in arr)
            {
                list.Add(node.Available);
            }
            list.Sort();
            return list;
        }

        private static int IndexOfFirstGreaterValue(List<int> arr, int target)
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

        private static Node[,] InitNodes(string[] lines)
        {
            Node[,] nodes;
            var last = lines.Last();
            var pattern = @"/dev.+x(?<X>\d+)-y(?<Y>\d+)\s+(?<size>\d+)T\s+(?<used>\d+)T\s+(?<Available>\d+)T\s+(?<UsedPerc>\d+)%";
            var regex = new Regex(pattern);
            var match = regex.Match(last);
            if (match.Success)
            {
                var x = Int32.Parse(match.Groups["X"].Value);
                var y = Int32.Parse(match.Groups["Y"].Value);
                nodes = new Node[x + 1, y + 1];
            }
            else
            {
                return null;
            }

            foreach (var line in lines)
            {
                match = regex.Match(line);
                if (match.Success)
                {
                    var x = Int32.Parse(match.Groups["X"].Value);
                    var y = Int32.Parse(match.Groups["Y"].Value);
                    var used = Int32.Parse(match.Groups["used"].Value);
                    var available = Int32.Parse(match.Groups["Available"].Value);
                    nodes[x, y] = new Node { Used = used, Available = available };
                }
            }

            return nodes;
        }

        public static string Run2(string input)
        {
            return "0";
        }
    }
}
