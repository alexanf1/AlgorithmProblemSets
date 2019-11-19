using System;
using System.Collections.Generic;
using System.Text;

namespace GraphApi.DirectGraph
{
    /// <summary>
    /// Computes the reverse post order of a graph. This can also be considered the topological sort order if 
    /// the graph also happens to be a Directed Acyclic Graph (DAG).
    /// </summary>
    internal class DepthFirstOrder
    {
        private bool[] _marked;
        private Stack<int> _reversePostOrder;

        /// <summary>
        /// The reverse post order of an acyclic digraph is also the topological order
        /// </summary>
        public IEnumerable<int> GetReversePostOrder => _reversePostOrder;

        public DepthFirstOrder(DirectedGraph g)
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

        private void DFS(DirectedGraph g, int v)
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
