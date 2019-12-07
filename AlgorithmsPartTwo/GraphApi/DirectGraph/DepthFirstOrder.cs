using System.Collections.Generic;
using GraphApi.Interfaces;
using GraphApi.DirectGraph.Weighted;

namespace GraphApi.DirectGraph
{
    /// <summary>
    /// Computes the reverse DFS post order of a DAG (directed acyclic graph). 
    /// This can also be considered the topological sort order if 
    /// the graph also happens to be a Directed Acyclic Graph (DAG).
    /// Topological sort order can solve problems dealing with precedence contraints. Think of a DAG where
    /// all the directed edges point upward.
    /// 
    /// Note: Topological sort is impossible when there is a cycle!
    /// </summary>
    internal class DepthFirstOrder
    {
        private bool[] _marked;
        private Stack<int> _reversePostOrder; // A stack is required in order to return a collection in post order.

        /// <summary>
        /// The reverse DFS post order of an acyclic digraph is also the topological order
        /// </summary>
        public IEnumerable<int> GetReversePostOrder => _reversePostOrder;

        public DepthFirstOrder(IGraph g)
        {
            _marked = new bool[g.GetNumberOfVertices()];
            _reversePostOrder = new Stack<int>();

            for(int v = 0; v < g.GetNumberOfVertices(); v++)
            {
                if(!_marked[v])
                {
                    DFS(g, v);
                }
            }
        }

        public DepthFirstOrder(EdgeWeightedDigraph g)
        {
            _marked = new bool[g.GetNumberOfVertices()];
            _reversePostOrder = new Stack<int>();

            for (int v = 0; v < g.GetNumberOfVertices(); v++)
            {
                if (!_marked[v])
                {
                    DFS(g, v);
                }
            }
        }

        private void DFS(EdgeWeightedDigraph g, int v)
        {
            _marked[v] = true;
            foreach (DirectedEdge e in g.GetAdjacentEdges(v))
            {
                int w = e.GetTo();

                if (!_marked[w])
                {
                    DFS(g, w);
                }
            }

            _reversePostOrder.Push(v);
        }

        private void DFS(IGraph g, int v)
        {
            _marked[v] = true;
            foreach (int w in g.GetAdjacentVertices(v))
            {
                if (!_marked[w])
                {
                    DFS(g, w);
                }
            }
            _reversePostOrder.Push(v);
        }
    }
}
