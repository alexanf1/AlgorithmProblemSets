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

            DijkstraSP dsp = new DijkstraSP(d, 0);
            Console.WriteLine("Dijkstra's SP");
            foreach (DirectedEdge e in dsp.GetPathTo(6))
            {
                Console.WriteLine($"{e}");
            }

            BellmanFordSp sp = new BellmanFordSp(d, 0);
            Console.WriteLine("\nBellmanFord's SP");
            Console.WriteLine($"has negative cycle : {sp.HasNegativeCycle()}");
            foreach (DirectedEdge e in sp.GetPathTo(6))
            {
                Console.WriteLine($"{e}");
            }

            EdgeWeightedDigraph d2 = new EdgeWeightedDigraph(7);
            d2.AddEdge(new DirectedEdge(0, 2, 1));
            d2.AddEdge(new DirectedEdge(2, 6, 5));
            d2.AddEdge(new DirectedEdge(6, 5, -7));
            d2.AddEdge(new DirectedEdge(5, 3, -15));
            d2.AddEdge(new DirectedEdge(5, 4, 4));
            d2.AddEdge(new DirectedEdge(3, 6, -10));
            d2.AddEdge(new DirectedEdge(3, 4, 3));

            BellmanFordSp sp2 = new BellmanFordSp(d2, 0);
            Console.WriteLine("\nBellmanFord's SP 2");
            Console.WriteLine($"has negative cycle : {sp2.HasNegativeCycle()}");
            foreach (DirectedEdge e in sp2.GetNegativeCycle())
            {
                Console.WriteLine($"{e}");
            }
        }
    }
}
