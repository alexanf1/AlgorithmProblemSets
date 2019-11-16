﻿using System;
using System.Collections.Generic;
using System.Text;
using Algorithms.GraphApi.Interfaces;

namespace Algorithms.GraphApi
{
    static class GraphProcessing
    {
        /// <summary>
        /// Computes the degree of vertex 'v' (the number of edges)
        /// </summary>
        /// <param name="graph">vertex v</param>
        /// <returns></returns>
        public static int degree(IGraph graph, int v)
        {
            return graph.GetAdjacentVertices(v).Count;
        }

        /// <summary>
        /// Computes the maximum degree for all vertices in the graph
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static int maxDegree(IGraph graph)
        {
            int maxDegree = 0;
            for(int v = 0; v < graph.GetNumberOfVertices(); v++)
            {
                maxDegree = Math.Max(maxDegree, graph.GetAdjacentVertices(v).Count);
            }

            return maxDegree;
        }

        /// <summary>
        /// Computes the average degree across all the vertices in a graph
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static double averageDegree(IGraph graph)
        {
            return 2.0 * graph.GetNumberOfEdges() / graph.GetNumberOfVertices();
        }

        /// <summary>
        /// Returns the number of self-loops in the graph
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static int numberOfSetLoops(IGraph graph)
        {
            int totalSelfLoops = 0;
            for (int v = 0; v < graph.GetNumberOfVertices(); v++)
            {
                foreach(int w in graph.GetAdjacentVertices(v))
                {
                    if(v == w)
                    {
                        totalSelfLoops++;
                    }
                }
            }

            // Note that each edge is counted twice so divide
            return totalSelfLoops / 2;
        }
    }
}
