﻿using GraphApi.UndirectedGraph;
using GraphApi.DirectGraph;
using System;
using System.Collections.Generic;
using GraphApi.Interfaces;
using GraphApi.Paths;

namespace GraphApi
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "C:\\AlgorithmProblemSets\\DirectedGraphInput.txt";

            DirectedGraph g = DirectedGraph.InitializeGraph(filePath);

            Console.WriteLine(g.ToString());
            Console.WriteLine("Check connected paths");

            //ConnectedComponents cc = new ConnectedComponents(g);
            //Console.WriteLine($"number of connected components: {cc.Count}");

            /*for (int v = 0; v < g.GetNumberOfVertices(); v++)
            {
                Console.WriteLine($"vertex:{v} is part of set:{cc.Id(v)}");
            }*/

            int sourceVertex = 0;
            IPath dfp = new DepthFirstPaths(g, sourceVertex);

            Console.WriteLine($"number of edges {g.GetNumberOfEdges()}");

            for(int destination = 0; destination < g.GetNumberOfVertices(); destination++)
            {
                IEnumerable<int> enumerable = dfp.PathTo(destination);

                if (enumerable == null)
                {
                    Console.WriteLine($"src:{sourceVertex} to {destination}, no path exists");
                    continue;
                }

                string pathOutput = string.Empty;
                foreach (int vertex in enumerable)
                {
                    pathOutput += $"{vertex},";
                }

                Console.WriteLine($"src:{sourceVertex} to {destination}, path:{pathOutput}");
            }

            DepthFirstOrder dfo = new DepthFirstOrder(g);
            string topologicalOrder = "[bottom]";
            foreach (int v in dfo.GetReversePostOrder())
            {
                topologicalOrder += $"{v},";
            }
            Console.WriteLine($"Topological order:{topologicalOrder}[top]");
        }
    }
}
