namespace AdventOfCode.Solutions.Event2015.Day13
{
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Incapsulates drawing logic.
    /// </summary>
    public class Drawing
    {
        private readonly int queueSize;
        private readonly Queue<GraphNode> queue;

        /// <summary>
        /// Initializes a new instance of the <see cref="Drawing"/> class.
        /// </summary>
        /// <param name="queueSize">The size of drawing queue.</param>
        public Drawing(int queueSize)
        {
            this.queueSize = queueSize;
            this.queue = new Queue<GraphNode>(this.queueSize + 1);
        }

        /// <summary>
        /// Updates drawing state.
        /// </summary>
        /// <param name="node">The graph node to add.</param>
        public void Update(GraphNode node)
        {
            this.queue.Enqueue(node);
            if (this.queue.Count > this.queueSize)
            {
                this.queue.Dequeue();
            }
        }

        /// <summary>
        /// Gets current drawing state.
        /// </summary>
        /// <returns>The string of current drawing state.</returns>
        public string GetStateStr()
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
                outstr.Append(currentPath.CurrentPerson);

                foreach (var n in path)
                {
                    outstr.Append(" - ");
                    outstr.Append(n.CurrentPerson);
                }

                outstr.AppendLine();
            }

            return outstr.ToString();
        }
    }
}
