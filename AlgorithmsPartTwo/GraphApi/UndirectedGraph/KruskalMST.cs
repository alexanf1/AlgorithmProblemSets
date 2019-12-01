using System;
using System.Collections.Generic;
using System.Text;
using DataStructureApi.PriorityQueue;
using DataStructureApi.UnionFind;

namespace GraphApi.UndirectedGraph
{
    /// <summary>
    /// Computes the mst in time proportional to E log E where 'E' is the number of edges
    /// </summary>
    internal class KruskalMST
    {
        private LinkedList<Edge> _mst;
        private double _totalWeight;

        public KruskalMST(EdgeWeightedGraph g)
        {
            _mst = new LinkedList<Edge>();

            // Create queue to hold on edges in ascending order
            MinPriorityQueue<Edge> pq = new MinPriorityQueue<Edge>(g.GetNumberOfEdges());
            foreach(Edge e in g.GetAllEdges())
            {
                pq.insert(e);
            }

            // union find
            WeightedUnion u = new WeightedUnion(g.GetNumberOfVertices());
            while (!pq.IsEmpty() && _mst.Count < (g.GetNumberOfVertices() - 1))
            {
                Edge e = pq.DeleteMin();

                int p = e.GetEitherVertex();
                int q = e.GetOtherVertex(p);
                if(!u.IsConnected(p, q))
                {
                    u.AddUnion(p, q);
                    _mst.AddLast(e);
                    _totalWeight += e.Weight;
                }
            }
        }

        /// <summary>
        /// Returns all the edges og a given graph that are part of the MST
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
