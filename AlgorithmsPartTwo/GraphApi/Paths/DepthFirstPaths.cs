using System.Collections.Generic;
using GraphApi.Interfaces;

namespace GraphApi.Paths
{
    /// <summary>
    /// DFS is better to use if you plan on visiting all the nodes in the graph
    /// </summary>
    internal class DepthFirstPaths : IPath
    {
        private bool[] _marked; // marked[v] = true if v connected to s
        private int[] _edgeTo; // edgeTo[v] = previous vertex on path from s to v
        private int _source;

        /// <summary>
        /// Finds all vertices connected to a vertex 's' using DFS
        /// </summary>
        /// <param name="g">The graph</param>
        /// <param name="s">The source vertex</param>
        public DepthFirstPaths(IGraph graph, int source)
        {
            _source = source;
            _marked = new bool[graph.GetNumberOfVertices()];
            _edgeTo = new int[graph.GetNumberOfVertices()];

            DFS(graph, source);
        }

        // Notice the implicit use of the stack which happens with recursion
        private void DFS(IGraph g, int v)
        {
            _marked[v] = true;
            foreach(int w in g.GetAdjacentVertices(v))
            {
                if(!_marked[w])
                {
                    DFS(g, w);
                    _edgeTo[w] = v; // assigns each adjacent node of 'v', 'v' has the previous node
                }
            }
        }

        /// <inheritdoc/>
        public bool HasPathTo(int v)
        {
            return _marked[v];
        }

        /// <inheritdoc/>
        public IEnumerable<int> PathTo(int v)
        {
            if (!HasPathTo(v))
            {
                // returns null if no path exists
                return null;
            }

            Stack<int> path = new Stack<int>();
            for(int x = v; x != _source; x = _edgeTo[x])
            {
                path.Push(x);
            }
            path.Push(_source);

            return path;
        }
    }
}
