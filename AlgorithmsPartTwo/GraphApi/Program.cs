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
            string filePath = "C:\\AlgorithmProblemSets\\GraphWeighted.txt";

            EdgeWeightedGraph g = EdgeWeightedGraph.CreateEdgeWeightGraphFromFile(filePath);

            Console.WriteLine($"{g.ToString()}");
        }
    }
}
