using System.Collections.Generic;
using DataStructureApi.PriorityQueue;
using DataStructureApi.UnionFind;
using GraphApi.UndirectedGraph.Weighted;
using GraphApi.Interfaces;

namespace GraphApi.UndirectedGraph.MinimalSpanningTree
{
    /// <summary>
    /// This class represents an implementation of Kruskal's MST Algorithm.
    /// Considers Edges in ascending order of weight, add to MST if it doesn't create a cycle
    /// [Performance]
    ///     - Build PQ:   1 * (E Log E)
    ///     - Delete Min: E * Log E
    ///     - Union:      V * Log* V
    ///     - Connected:  E * Log* V
    ///     - O(E Log E)
    /// 
    /// Computes the MST in time proportional to E log E where 'E' is the number of edges
    /// </summary>
    internal class KruskalMST : IMinimumSpanningTree
    {
        private LinkedList<Edge> _mst;
        private double _totalWeight;

        public KruskalMST(EdgeWeightedGraph g)
        {
            _mst = new LinkedList<Edge>();

            // Create a queue to hold on edges in ascending order
            MinPriorityQueue<Edge> pq = new MinPriorityQueue<Edge>(g.GetNumberOfEdges());
            foreach(Edge e in g.GetAllEdges())
            {
                pq.insert(e);
            }

            // Create a Union Find Data Structure
            WeightedUnion u = new WeightedUnion(g.GetNumberOfVertices());

            // Examine the smallest edge and determine if it has been already
            // connected by checking our union find datastructure. If it is, we can't add it because
            // doing so would create a cycle. Else add it to the MST
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

        /// <inheritdoc/>
        public ICollection<Edge> GetEdges()
        {
            return _mst;
        }

        /// <inheritdoc/>
        public double GetWeight()
        {
            return _totalWeight;
        }
    }
}
