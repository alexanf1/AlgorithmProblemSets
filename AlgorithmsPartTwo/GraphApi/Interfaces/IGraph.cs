using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.GraphApi.Interfaces
{
    /// <summary>
    /// Simply interface for a implementing a graph data structure
    /// </summary>
    internal interface IGraph
    {
        /// <summary>
        /// Connectes to two verices, v and w, by an edge
        /// </summary>
        /// <param name="v">The first vertex</param>
        /// <param name="w">The second vertex</param>
        void AddEdge(int v, int w);

        /// <summary>
        /// Returns an ICollection of all adjacent vertices of vertex v
        /// </summary>
        /// <param name="v">The vertex in question</param>
        /// <returns></returns>
        ICollection<int> GetAdjacentVertices(int v);

        /// <summary>
        /// Returns the number of vertices in the entire graph
        /// </summary>
        /// <returns></returns>
        int GetNumberOfVertices();

        /// <summary>
        /// Returns the number of edges in the entire graph
        /// </summary>
        /// <returns></returns>
        int GetNumberOfEdges();

        /// <summary>
        /// String representation of a graph
        /// </summary>
        /// <returns></returns>
        string ToString();
    }
}
