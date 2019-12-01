using System;
using System.Collections.Generic;
using System.Text;
using DataStructureApi.PriorityQueue;

namespace GraphApi.UndirectedGraph
{
    /// <summary>
    /// Computes the MST in time proportional to E log E and extra space proportional to E
    /// Note the similarities between Dijkstra's algorithm (closest vertex to the source via directed path)
    /// and Prim's algorithm (closest vertex to the tree via undirected edge)
    /// </summary>
    internal class LazyPrimMST
    {
        private bool[] _marked;
        private MinPriorityQueue<Edge> _pq;
        private LinkedList<Edge> _mst;
        private double _totalWeight;

        public LazyPrimMST(EdgeWeightedGraph g)
        {
            _mst = new LinkedList<Edge>();
            _pq = new MinPriorityQueue<Edge>(g.GetNumberOfEdges());
            _marked = new bool[g.GetNumberOfVertices()];

            Visit(g, 0);

            while(!_pq.IsEmpty())
            {
                Edge e = _pq.DeleteMin();
                int v = e.GetEitherVertex();
                int w = e.GetOtherVertex(v);

                if (_marked[v] && _marked[w])
                    continue;

                _mst.AddLast(e);
                _totalWeight += e.Weight;

                if (!_marked[v])
                    Visit(g, v);

                if (!_marked[w])
                    Visit(g, w);
            }
        }

        private void Visit(EdgeWeightedGraph g, int v)
        {
            _marked[v] = true;
            foreach(Edge e in g.GetAdjacentEdges(v))
            {
                if(!_marked[e.GetOtherVertex(v)])
                {
                    _pq.insert(e);
                }
            }
        }

        /// <summary>
        /// Returns all the edges of a given graph that are part of the MST
        /// </summary>
        /// <returns></returns>
        public ICollection<Edge> GetEdges()
        {
            return _mst;
        }

        /// <summary>
        /// Returns the total weight of the MST
        /// </summary>
        /// <returns></returns>
        public double GetWeight()
        {
            return _totalWeight;
        }
    }
}
