using System.Collections.Generic;

namespace AdventOfCode.Solutions.Event2020.Day7
{
    public class Node
    {
        public List<Node> Parents { get; private set; }

        public List<(Node node, int count)> Children { get; private set; }
        public string Color { get; }

        public Node(string color)
        {
            Parents = new List<Node>();
            Children = new List<(Node node, int count)>();
            Color = color;
        }
    }
}