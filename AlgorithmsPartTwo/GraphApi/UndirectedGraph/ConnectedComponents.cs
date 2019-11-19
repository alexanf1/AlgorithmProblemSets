namespace GraphApi.UndirectedGraph
{
    /// <summary>
    /// Computing the connected components in a undirrected graph using one DFS in E + V linear time.
    /// </summary>
    internal class ConnectedComponents
    {
        private bool[] _marked;
        private int?[] _ids;
        private int _count;

        public int GetNumberOfConnectedComponents => _count;
        public int GetConnectedComponentId(int v) => (int)_ids[v];

        public ConnectedComponents(UndirectedGraph g)
        {
            _marked = new bool[g.GetNumberOfVertices()];
            _ids = new int?[g.GetNumberOfVertices()];

            for(int v = 0; v < g.GetNumberOfVertices(); v++)
            {
                if(_ids[v] == null)
                {
                    DFS(g, v, _count);
                    _count++;
                }
            }
        }

        private void DFS(UndirectedGraph g, int v, int id)
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
