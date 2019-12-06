using System;
using System.Collections.Generic;
using System.Text;
using GraphApi.DirectGraph.Weighted;

namespace GraphApi.Interfaces
{
    /// <summary>
    /// Represents a signle-source shortest path algorithms
    /// </summary>
    interface ISingleSource
    {
        /// <summary>
        /// length of shortest path from s to v
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        double GetDistTo(int v);

        /// <summary>
        /// shortest path from s to v
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        IEnumerable<DirectedEdge> GetPathTo(int v);
    }
}
