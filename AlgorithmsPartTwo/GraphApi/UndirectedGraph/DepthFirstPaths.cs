using System.Collections.Generic;
using GraphApi.Interfaces;

namespace GraphApi.UndirectedGraph
{
    internal class DepthFirstPaths : IPath
    {
        private bool[] _marked;
        private int[] _edgeTo;
        private int _source;

        /// <summary>
        /// Finds all vertices connected to a vertex 's' using DFS
        /// </summary>
        /// <param name="g">The graph</param>
        /// <param name="s">The source vertex</param>
        public DepthFirstPaths(Graph graph, int source)
        {
            _source = source;
            _marked = new bool[graph.GetNumberOfVertices()];
            _edgeTo = new int[graph.GetNumberOfVertices()];

            DFS(graph, source);
        }

        private void DFS(Graph g, int v)
        {
            _marked[v] = true;
            foreach(int w in g.GetAdjacentVertices(v))
            {
                if(!_marked[w])
                {
                    DFS(g, w);
                    _edgeTo[w] = v;
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
