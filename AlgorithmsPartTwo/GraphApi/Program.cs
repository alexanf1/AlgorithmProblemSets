using Algorithms.GraphApi;
using Algorithms.GraphApi.Interfaces;

namespace GraphApi
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "C:\\AlgorithmProblemSets\\GraphInput.txt";

            IGraph g = Graph.InitializeGraph(filePath);
        }
    }
}
