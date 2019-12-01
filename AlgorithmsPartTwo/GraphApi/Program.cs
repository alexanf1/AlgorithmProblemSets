using GraphApi.UndirectedGraph;
using System;

namespace GraphApi
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "C:\\AlgorithmProblemSets\\GraphWeighted.txt";

            EdgeWeightedGraph g = EdgeWeightedGraph.CreateEdgeWeightGraphFromFile(filePath);

            Console.WriteLine("EagerPrimMST");
            EagerPrimMST mst = new EagerPrimMST(g);
            foreach (Edge e in mst.GetEdges())
                Console.WriteLine($"{e}");

            Console.WriteLine("\nLazyPrimMST");
            LazyPrimMST _mst = new LazyPrimMST(g);
            foreach (Edge e in _mst.GetEdges())
                Console.WriteLine($"{e}");
        }
    }
}
