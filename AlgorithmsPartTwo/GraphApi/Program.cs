using GraphApi.UndirectedGraph;
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
            string filePath = "C:\\AlgorithmProblemSets\\GraphInput2.txt";

            IGraph g = DirectedGraph.InitializeGraph(filePath);

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

            for(int destination = 0; destination < g.GetNumberOfVertices(); destination++)
            {
                IEnumerable<int> enumerable = dfp.PathTo(destination);

                if (enumerable == null)
                {
                    Console.WriteLine($"src:{sourceVertex} to {destination}, no path exists\n");
                    continue;
                }

                string pathOutput = string.Empty;
                foreach (int vertex in enumerable)
                {
                    pathOutput += $"{vertex},";
                }

                Console.WriteLine($"src:{sourceVertex} to {destination}, path:{pathOutput}");
                //Console.WriteLine($"distance to {destination} from {sourceVertex}, {dfp.ShortestPathTo(destination)}\n");
            }
        }
    }
}
