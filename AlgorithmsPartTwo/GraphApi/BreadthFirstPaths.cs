using System.Collections.Generic;
using Algorithms.GraphApi.Interfaces;

namespace Algorithms.GraphApi
{
    internal class BreadthFirstPaths : IPath
    {
        private bool[] _marked;
        private int[] _edgeTo;
        private int _source;

        public BreadthFirstPaths(Graph g, int s)
        {
            _source = s;
            _marked = new bool[g.GetNumberOfVertices()];
            _edgeTo = new int[g.GetNumberOfVertices()];

            BFS(g, s);
        }

        private void BFS(Graph g, int v)
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(v);
            _marked[v] = true;

            while(queue.Count > 0)
            {
                int w = queue.Dequeue();
                foreach(int z in g.GetAdjacentVertices(w))
                {
                    if(!_marked[z])
                    {
                        _marked[z] = true;
                        queue.Enqueue(z);
                        _edgeTo[z] = w;
                    }
                }
            }
        }

        public bool HasPathTo(int v)
        {
            return _marked[v];
        }

        public IEnumerable<int> PathTo(int v)
        {
            if (!HasPathTo(v))
            {
                return null;
            }

            Stack<int> path = new Stack<int>();
            for (int x = v; x != _source; x = _edgeTo[x])
            {
                path.Push(x);
            }
            path.Push(_source);

            return path;
        }
    }
}
