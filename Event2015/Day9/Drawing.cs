namespace AdventOfCode.Solutions.Event2015.Day9
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class Drawing
    {
        private int queueSize;
        private Queue<GraphNode> queue;

        public Drawing(int queueSize)
        {
            this.queueSize = queueSize;
            this.queue = new Queue<GraphNode>(this.queueSize + 1);
        }

        internal void Update(GraphNode node)
        {
            this.queue.Enqueue(node);
            if (this.queue.Count > this.queueSize)
            {
                this.queue.Dequeue();
            }
        }

        internal string GetStateStr()
        {
            var outstr = new StringBuilder();
            foreach (var p in this.queue)
            {
                var path = new LinkedList<GraphNode>();
                var currentPath = p;
                while (currentPath.Parent != null)
                {
                    path.AddFirst(currentPath);
                    currentPath = currentPath.Parent;
                }

                outstr.Append(p.Cost);
                outstr.Append(": ");
                outstr.Append(currentPath.CurrentPos);

                foreach (var n in path)
                {
                    outstr.Append(" - ");
                    outstr.Append(n.CurrentPos);
                }

                outstr.AppendLine();
            }

            return outstr.ToString();
        }
    }
}
