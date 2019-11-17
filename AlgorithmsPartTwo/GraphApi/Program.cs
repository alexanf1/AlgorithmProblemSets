using Algorithms.GraphApi;
using System;
using System.Collections.Generic;
using Algorithms.GraphApi.Interfaces;

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
            BreadthFirstPaths dfp = new BreadthFirstPaths(g, sourceVertex);

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
                Console.WriteLine($"distance to {destination} from {sourceVertex}, {dfp.ShortestPathTo(destination)}\n");
            }
            
        }
    }
}
