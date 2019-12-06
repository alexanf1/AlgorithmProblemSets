using System;
using System.Collections.Generic;
using System.Text;
using DataStructureApi.PriorityQueue;
using GraphApi.Interfaces;
using GraphApi.DirectGraph.Weighted;

namespace GraphApi.DirectGraph.ShortestPath
{
    /// <summary>
    /// All shortest path algorithms are simply trying to apply edge relaxation
    /// and figure out which edge needs to be relaxed.
    /// 
    /// This is also built using two index arrays and an index priority queue
    /// 
    /// Note: This algorithm performs very similarily to prim's eager algorithm
    /// Prim's: Closest vertex to the tree (via an undirected edge)
    /// Dijkstra's: Closest vertex to the source (via a directed path)
    /// 
    /// Dijkstra's algorithm is well suited for Digraphs with Non-negative weights
    /// [Performance]
    ///     *Binary Heap (as seen below)
    ///     - Insert:    Log V
    ///     - DeleteMin: Log V
    ///     - ChangeKey: Log V
    ///     - O(n) = E Log V
    ///     
    /// Note: An unordered PQ implementation would be better suited if we were dealing with a dense graph
    /// aka lots of edges and few vertices.
    /// </summary>
    internal class DijkstraSP : ISingleSource
    {
        private DirectedEdge[] _edgeTo; // _edgeTo[v] is last edge on shortest path from s to v
        private double[] _distTo; // _distTo[v] is length of shortest path from s to v
        private IndexMinPriorityQueue<double> _pq;

        public DijkstraSP(EdgeWeightedDigraph g, int s)
        {
            _edgeTo = new DirectedEdge[g.GetNumberOfVertices()];
            _distTo = new double[g.GetNumberOfVertices()];
            _pq = new IndexMinPriorityQueue<double>(g.GetNumberOfVertices());

            // Similar to Prim's algorithm for setting up _distTo[]
            for (int v = 0; v < g.GetNumberOfVertices(); v++)
                _distTo[v] = double.MaxValue;

            // consider vertices in increasing order of distance from the source vertex
            // set initial source vertex to default
            _distTo[s] = 0.0; 
            _pq.Insert(s, 0.0);
            while(!_pq.IsEmpty())
            {
                int v = _pq.DeleteMin();
                foreach (DirectedEdge e in g.GetAdjacentEdges(v))
                    RelaxEdge(e);
            }
        }

        /// <summary>
        /// If e = v->w gives a shorter path to w through v,
        /// update both distTo[w] and edgeTo[w]. This is relaxing an edge and
        /// including it in the data structures.
        /// </summary>
        /// <param name="e"></param>
        private void RelaxEdge(DirectedEdge e)
        {
            int v = e.GetFrom();
            int w = e.GetTo();

            if(_distTo[w] > _distTo[v] + e.Weight)
            {
                _distTo[w] = _distTo[v] + e.Weight;
                _edgeTo[w] = e;

                if (_pq.Contains(w))
                    _pq.ChangeKey(w, _distTo[w]);
                else
                    _pq.Insert(w, _distTo[w]);
            }
        }

        public double GetDistTo(int v)
        {
            return _distTo[v];
        }

        // _edgeTo[] will hold the smallest wieghted edge from the source vertex
        public IEnumerable<DirectedEdge> GetPathTo(int v)
        {
            Stack<DirectedEdge> path = new Stack<DirectedEdge>();
            for(DirectedEdge e = _edgeTo[v]; e != null; e = _edgeTo[e.GetFrom()])
            {
                path.Push(e);
            }

            return path;
        }
    }
}
