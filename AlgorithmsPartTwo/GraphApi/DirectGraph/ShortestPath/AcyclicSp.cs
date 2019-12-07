using GraphApi.DirectGraph.Weighted;
using System;
using System.Collections.Generic;

namespace GraphApi.DirectGraph.ShortestPath
{
    /// <summary>
    /// Unlike Dijkstra's algorithm, if we are dealing with a weighted DAG (no directed cycles)
    /// there's an easier way! The edge weights can be positive, negative, or zero.
    /// 
    /// [Performance]
    ///     - O (E + V) or linear time!
    /// [Space]
    ///     - O (V)
    /// 
    /// Note: If you need to find the longest path in an edge-weighted DAG, you could also 
    /// simply negate the edge weights and at them all up. The smallest number actually represents the
    /// shortest path.
    /// 
    /// Topological/Acyclic algorithm is well suited for Digraphs with no direct cycles
    /// </summary>
    internal class AcyclicSp
    {
        private DirectedEdge[] _edgeTo; // _edgeTo[v] is last edge on shortest path from s to v
        private double[] _distTo; // _distTo[v] is length of shortest path from s to v

        public AcyclicSp(EdgeWeightedDigraph g, int s)
        {
            _edgeTo = new DirectedEdge[g.GetNumberOfVertices()];
            _distTo = new double[g.GetNumberOfVertices()];

            // Similar to Prim's algorithm for setting up _distTo[]
            for (int v = 0; v < g.GetNumberOfVertices(); v++)
                _distTo[v] = double.MaxValue;

            // consider vertices in increasing order of distance from the source vertex
            // set initial source vertex to default
            _distTo[s] = 0.0;

            // topological sort, which is just the reverse post order of an acyclic direct graph
            DepthFirstOrder dfo = new DepthFirstOrder(g);

            // loop through each vertex and only look at it edges once!
            foreach(int v in dfo.GetReversePostOrder)
            {
                foreach(DirectedEdge e in g.GetAdjacentEdges(v))
                {
                    RelaxEdge(e);
                }
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

            if (_distTo[w] > _distTo[v] + e.Weight)
            {
                _distTo[w] = _distTo[v] + e.Weight;
                _edgeTo[w] = e;
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
            for (DirectedEdge e = _edgeTo[v]; e != null; e = _edgeTo[e.GetFrom()])
            {
                path.Push(e);
            }

            return path;
        }
    }
}
