using System.Collections.Generic;

namespace GraphApi.Interfaces
{
    /// <summary>
    /// Calculates the paths in a given graph
    /// </summary>
    internal interface IPath
    {
        /// <summary>
        /// Determines if there is a connected path to v
        /// </summary>
        /// <param name="v">The destination vertex 'v'</param>
        /// <returns></returns>
        bool HasPathTo(int v);

        /// <summary>
        /// Returns a collection representing a path from s to v.
        /// </summary>
        /// <param name="v"></param>
        /// <returns>If null then no path exists</returns>
        IEnumerable<int> PathTo(int v);
    }
}
