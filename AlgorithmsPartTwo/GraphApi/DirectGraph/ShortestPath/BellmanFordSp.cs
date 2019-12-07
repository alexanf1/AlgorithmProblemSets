using System;
using System.Collections.Generic;
using System.Text;
using GraphApi.DirectGraph.Weighted;
using DataStructureApi.PriorityQueue;

namespace GraphApi.DirectGraph.ShortestPath
{
    /// <summary>
    /// The Bellman Ford algorithm class is used for solving the
    /// single-source shortest paths problem in edge-weighted digraphs with
    /// no negative cycles. The edge weights can be positive, negative, or zero.
    /// 
    /// This class finds either a shortest path from the source vertex
    /// to every other vertex or a negative cycle reachable from the source vertex.
    ///  
    /// This implementation uses a queue-based implementation of
    /// the Bellman-Ford-Moore algorithm. You can also do two for loops
    /// doing multiple passes on v.
    /// 
    /// Note: this algorithm doesn't protect it self from negative cycles but it can
    /// and technically, the easier implementation is not using the queue and the algorithm
    /// will get stuck updating _distTo[v]. However, if you detect a change in phase 'v' when this is
    /// updated, you can assume that last edge updated it part of a cycle. 
    /// 
    /// [Performance]
    ///     - (without queue) E * V
    ///     - (with queue) E * V (but best case is E + V)
    /// [Space]
    ///     - (extra space) V
    /// </summary>
    internal class BellmanFordSp
    {
        private DirectedEdge[] _edgeTo; // _edgeTo[v] is last edge on shortest path from s to v
        private double[] _distTo; // _distTo[v] is length of shortest path from s to v
        private Queue<int> _queue;
        private bool[] _onQueue;

        public BellmanFordSp(EdgeWeightedDigraph g, int s)
        {
            _edgeTo = new DirectedEdge[g.GetNumberOfVertices()];
            _distTo = new double[g.GetNumberOfVertices()];
            _onQueue = new bool[g.GetNumberOfVertices()];
            _queue = new Queue<int>();

            for (int v = 0; v < g.GetNumberOfVertices(); v++)
                _distTo[v] = double.MaxValue;

            _distTo[s] = 0.0;
            _queue.Enqueue(s);
            _onQueue[s] = true;

            while(_queue.Count > 0)
            {
                int v = _queue.Dequeue();
                _onQueue[v] = false;
                foreach (DirectedEdge e in g.GetAdjacentEdges(v))
                {
                    RelaxEdge(g, e);
                }
            }
        }

        // relax vertex v and put other endpoints on queue if changed
        private void RelaxEdge(EdgeWeightedDigraph g, DirectedEdge e)
        {
            int v = e.GetFrom();
            int w = e.GetTo();

            if (_distTo[w] > _distTo[v] + e.Weight)
            {
                _distTo[w] = _distTo[v] + e.Weight;
                _edgeTo[w] = e;

                if(!_onQueue[w])
                {
                    _queue.Enqueue(w);
                    _onQueue[w] = true;
                }
            }
        }

        public IEnumerable<DirectedEdge> GetPathTo(int v)
        {
            Stack<DirectedEdge> path = new Stack<DirectedEdge>();
            for (DirectedEdge e = _edgeTo[v]; e != null; e = _edgeTo[e.GetFrom()])
            {
                path.Push(e);
            }

            return path;
        }

        public bool HasNegativeCycle()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DirectedEdge> GetNegativeCycle()
        {
            throw new NotImplementedException();
        }
    }
}
