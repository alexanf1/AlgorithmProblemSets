using System;
using System.Collections.Generic;
using System.Text;

namespace GraphApi.DirectGraph
{
    /// <summary>
    /// Computing the strongly connected components in a directed graph using two DFSs in E + V linear time.
    /// This is also known as the KosarajuSharir algorithm
    /// </summary>
    internal class StrongComponents
    {
        private bool[] _marked;
        private int?[] _ids;
        private int _count;

        public int Count => _count;
        public int Id(int v) => (int)_ids[v];
        public bool IsStronglyConnectedTo(int v, int w) => _ids[v] == _ids[w];

        public StrongComponents(DirectedGraph g)
        {
            _marked = new bool[g.GetNumberOfVertices()];
            _ids = new int?[g.GetNumberOfVertices()];

            DirectedGraph rg = g.GetReverse();
            DepthFirstOrder dfo = new DepthFirstOrder(rg);

            foreach(int v in dfo.GetReversePostOrder())
            {
                if (_ids[v] == null)
                {
                    DFS(g, v, _count);
                    _count++;
                }
            }
        }

        private void DFS(DirectedGraph g, int v, int id)
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
