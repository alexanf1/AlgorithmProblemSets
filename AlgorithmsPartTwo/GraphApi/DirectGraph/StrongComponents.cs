using System;
using System.Collections.Generic;
using System.Text;

namespace GraphApi.DirectGraph
{
    /// <summary>
    /// Computing the strong components in a directed graph using two DFSs in E + V linear time.
    /// This is also known as the Kosaraju-Sharir algorithm
    /// 
    /// [Definitions]
    ///     - A 'strong component' is a maximal subset of strongly-connected vertices
    ///     - Two vertices are 'strongly-connected' if there is a directed path from v to w and 
    ///         a directed path from w to v.
    /// </summary>
    internal class StrongComponents
    {
        private bool[] _marked;
        private int?[] _ids;
        private int _count;

        public int GetNumberOfStronglyConnectedComponents => _count;
        public int GetStronglyConnectedComponentId(int v) => (int)_ids[v];
        public bool IsStronglyConnectedTo(int v, int w) => _ids[v] == _ids[w];

        public StrongComponents(Digraph g)
        {
            _marked = new bool[g.GetNumberOfVertices()];
            _ids = new int?[g.GetNumberOfVertices()];

            // Phase 1: Compute the reverse postorder of a reversed graph
            Digraph reverseGraph = g.GetReverse();
            DepthFirstOrder dfo = new DepthFirstOrder(reverseGraph);

            // Phase 2: Perform DFS on G, visiting nodes based on the reverse postorder of the reversed graph
            foreach(int v in dfo.GetReversePostOrder)
            {
                if (_ids[v] == null)
                {
                    DFS(g, v, _count);
                    _count++;
                }
            }
        }

        private void DFS(Digraph g, int v, int id)
        {
            _marked[v] = true;
            _ids[v] = id;
            foreach (int w in g.GetAdjacentVertices(v))
            {
                if (!_marked[w])
                {
                    DFS(g, w, id);
                }
            }
        }
    }
}
