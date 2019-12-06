namespace GraphApi.UndirectedGraph
{
    /// <summary>
    /// Computing the connected components in a undirrected graph using one DFS in E + V linear time.
    /// Provides constant time verification of connected components
    /// Note: This isn't quite like Union-Find as this algorithm is in fact completed in linear time
    /// However, the biggest difference here is that we are essentially performing all of the unions we want to have
    /// and then only performing 'find' operations afterward. Union-Find allows you to mix between the two operations.
    /// 
    /// [Defintion]
    ///     - Two vertices are 'connected' if there is a path between v and w
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

            // Essentially, we are going to start with a given node and perform DFS
            // assigning each visited node with the same identifier.
            // Once we no longer can continue we increment the counter
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
