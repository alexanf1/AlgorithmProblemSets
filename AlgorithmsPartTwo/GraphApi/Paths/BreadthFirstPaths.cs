﻿using System.Collections.Generic;
using GraphApi.Interfaces;

namespace GraphApi.Paths
{
    internal class BreadthFirstPaths : IPath
    {
        private int[] _edgeTo;
        private int?[] _distTo;
        private int _source;

        public BreadthFirstPaths(IGraph g, int s)
        {
            _source = s;
            _edgeTo = new int[g.GetNumberOfVertices()];
            _distTo = new int?[g.GetNumberOfVertices()];

            BFS(g, s);
        }

        private void BFS(IGraph g, int v)
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(v);
            _distTo[v] = 0;

            while (queue.Count > 0)
            {
                int w = queue.Dequeue();
                foreach(int z in g.GetAdjacentVertices(w))
                {
                    if(_distTo[z] == null)
                    {
                        queue.Enqueue(z);
                        _edgeTo[z] = w;
                        _distTo[z] = _distTo[w] + 1;
                    }
                }
            }
        }

        public bool HasPathTo(int v)
        {
            return _distTo[v] > 0;
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

        /// <summary>
        /// Returns the fewest number of edges required to reach vertex v
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public int ShortestPathTo(int v)
        {
            return (int) _distTo[v];
        }
    }
}
