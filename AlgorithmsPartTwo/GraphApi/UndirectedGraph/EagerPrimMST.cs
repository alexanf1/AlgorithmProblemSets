using System;
using System.Collections.Generic;
using System.Text;
using DataStructureApi.PriorityQueue;

namespace GraphApi.UndirectedGraph
{
    /// <summary>
    /// Biggest difference here is the use of the IndexMinPriority Queue
    /// </summary>
    internal class EagerPrimMST
    {
        private bool[] _marked;
        private double[] _distTo;
        private Edge[] _edgeTo;
        private IndexMinPriorityQueue<double> _pq;

        public EagerPrimMST(EdgeWeightedGraph g)
        {
            _pq = new IndexMinPriorityQueue<double>(g.GetNumberOfVertices());
            _marked = new bool[g.GetNumberOfVertices()];
            _distTo = new double[g.GetNumberOfVertices()];
            _edgeTo = new Edge[g.GetNumberOfVertices()];

            // This is needed since we are trying to find the minimum edges
            for (int v = 0; v < g.GetNumberOfVertices(); v++)
                _distTo[v] = double.MaxValue;

            // This also takes into account minimal spanning forests...
            for (int v = 0; v < g.GetNumberOfVertices(); v++)
            {
                if(!_marked[v])
                {
                    prim(g, v);
                }
            }
        }

        private void prim(EdgeWeightedGraph g, int v)
        {
            _distTo[v] = 0.0;
            _pq.Insert(v, _distTo[v]);
            while(!_pq.IsEmpty())
            {
                int w = _pq.DeleteMin();
                scan(g, w);
            }
        }

        private void scan(EdgeWeightedGraph g, int v)
        {
            _marked[v] = true;
            foreach(Edge e in g.GetAdjacentEdges(v))
            {
                int w = e.GetOtherVertex(v);

                if (_marked[w])
                    continue;

                // Check if the edge weight is less than the current shortest recording weight
                if(e.Weight < _distTo[w])
                {
                    _distTo[w] = e.Weight;
                    _edgeTo[w] = e; // The end result, this array will result in holding all mst edges

                    // Add or update keys and their values to the priority queue
                    if (_pq.Contains(w))
                        _pq.ChangeKey(w, _distTo[w]);
                    else
                        _pq.Insert(w, _distTo[w]);
                }
            }
        }

        /// <summary>
        /// Returns all the edges og a given graph that are part of the MST
        /// </summary>
        /// <returns></returns>
        public ICollection<Edge> GetEdges()
        {
            LinkedList<Edge> mst = new LinkedList<Edge>();
            for(int v = 0; v < _edgeTo.Length; v++)
            {
                Edge e = _edgeTo[v];
                if(e != null)
                {
                    mst.AddLast(e);
                }
            }

            return mst;
        }
    }
}
