using System;
using System.Collections.Generic;
using System.Text;
using DataStructureApi.PriorityQueue;
using GraphApi.UndirectedGraph.Weighted;
using GraphApi.Interfaces;

namespace GraphApi.UndirectedGraph.MinimalSpanningTree
{
    /// <summary>
    /// Biggest difference here is the use of the IndexMinPriority Queue and the queue only having 
    /// at most ONE entry per VERTEX. If it already has the key, we update it.
    /// 
    /// [Performance]
    ///     *Binary Heap (as seen below)
    ///     - Insert:    Log V
    ///     - DeleteMin: Log V
    ///     - ChangeKey: Log V
    ///     - O(n) = E Log V
    /// </summary>
    internal class EagerPrimMST : IMinimumSpanningTree
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

        // This really where the algorithm begins...
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
                // Notice how this looks very similar to the relax portion of a shortest path algorithm
                int w = e.GetOtherVertex(v);

                if (_marked[w])
                    continue;

                // Check if the edge weight is less than the current shortest recording weight
                if(_distTo[w] > e.Weight)
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public double GetWeight()
        {
            double totalWeight = 0;
            for (int v = 0; v < _edgeTo.Length; v++)
            {
                Edge e = _edgeTo[v];
                if (e != null)
                {
                    totalWeight += e.Weight;
                }
            }
            return totalWeight;
        }
    }
}
