namespace DataStructureApi.UnionFind.Interface
{
    /// <summary>
    /// A interface representing the commands for a union-find data structure
    /// </summary>
    interface IUnionFind
    {
        /// <summary>
        /// Add a connection between the vertex p and q
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        void AddUnion(int p, int q);

        /// <summary>
        /// Determines if the vertex p and q are in the same component
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        bool IsConnected(int p, int q);
    }
}
