using GraphApi.UndirectedGraph;
using System;
using GraphApi.DirectGraph.Weighted;
using GraphApi.DirectGraph.ShortestPath;

namespace GraphApi
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "C:\\AlgorithmProblemSets\\GraphWeighted2.txt";

            EdgeWeightedDigraph d = EdgeWeightedDigraph.CreateEdgeWeightGraphFromFile(filePath);
            //EdgeWeightedDigraph d = new EdgeWeightedDigraph(7);
            //d.AddEdge(new DirectedEdge(0, 2, 1));

            BellmanFordSp sp = new BellmanFordSp(d, 0);
            foreach(DirectedEdge e in sp.GetPathTo(3))
            {
                Console.WriteLine($"{e}");
            }
        }
    }
}
