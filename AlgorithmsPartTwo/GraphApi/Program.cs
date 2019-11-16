﻿using Algorithms.GraphApi;
using System;
using System.Collections.Generic;

namespace GraphApi
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "C:\\AlgorithmProblemSets\\GraphInput.txt";

            Graph g = Graph.InitializeGraph(filePath);

            Console.WriteLine(g.ToString());
            Console.WriteLine("Check connected paths");

            int sourceVertex = 0;
            DepthFirstPaths dfp = new DepthFirstPaths(g, sourceVertex);

            for(int destination = 0; destination < g.GetNumberOfVertices(); destination++)
            {
                IEnumerable<int> enumerable = dfp.PathTo(destination);

                if (enumerable == null) continue;

                string pathOutput = string.Empty;
                foreach (int vertex in enumerable)
                {
                    pathOutput += $"{vertex},";
                }
                Console.WriteLine($"src:{sourceVertex} to {destination}, path:{pathOutput}");
            }
            
        }
    }
}
