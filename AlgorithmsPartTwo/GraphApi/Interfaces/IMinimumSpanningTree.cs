using System;
using System.Collections.Generic;
using System.Text;
using GraphApi.UndirectedGraph.Weighted;

namespace GraphApi.Interfaces
{
    /// <summary>
    /// [Definition]
    ///     - A 'spanning tree' of G is a subgraph T that is both a 'tree' (connected and acyclic) and 
    ///     'spanning' (includes all of the vertices).
    ///     - A 'minimum spanning tree' includes all min weights
    /// </summary>
    interface IMinimumSpanningTree
    {
        /// <summary>
        /// Returns all the edges of a given graph that are part of the MST
        /// </summary>
        /// <returns></returns>
        ICollection<Edge> GetEdges();

        /// <summary>
        /// Returns the total weight of the MST
        /// </summary>
        /// <returns></returns>
        double GetWeight();
    }
}
