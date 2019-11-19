using System.Collections.Generic;
using System.IO;
using GraphApi.Interfaces;

namespace GraphApi.DirectGraph
{
    /// <summary>
    /// A data structure representation of a digraph (directed graph)
    /// </summary>
    internal class DirectedGraph : IGraph
    {
        private int _vertices;
        private int _edges;
        private LinkedList<int>[] _adj;

        public static DirectedGraph InitializeGraph(string fileName)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(fileName))
                {
                    // Read the stream to a string line by line
                    // First line is the number of vertices
                    int vertices = int.Parse(sr.ReadLine());
                    int edges = int.Parse(sr.ReadLine());

                    DirectedGraph g = new DirectedGraph(vertices);

                    while (!sr.EndOfStream)
                    {
                        string[] input = sr.ReadLine().Split(" ");
                        int v = int.Parse(input[0]);
                        int w = int.Parse(input[1]);

                        g.AddEdge(v, w);
                    }

                    return g;
                }
            }
            catch (IOException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Contructs a graph with a fixed number of vertices
        /// </summary>
        /// <param name="vertices">total number of vertices in the graph</param>
        public DirectedGraph(int vertices)
        {
            _vertices = vertices;

            _adj = new LinkedList<int>[_vertices];
            for (int v = 0; v < _adj.Length; v++)
            {
                _adj[v] = new LinkedList<int>();
            }
        }

        /// <inheritdoc/>
        public void AddEdge(int v, int w)
        {
            _adj[v].AddLast(w);
            _edges++;
        }

        /// <inheritdoc/>
        public ICollection<int> GetAdjacentVertices(int v)
        {
            return _adj[v];
        }

        /// <inheritdoc/>
        public int GetNumberOfEdges()
        {
            return _edges;
        }

        /// <inheritdoc/>
        public int GetNumberOfVertices()
        {
            return _vertices;
        }

        public DirectedGraph GetReverse()
        {
            DirectedGraph reversedGraph = new DirectedGraph(_vertices);
            for (int v = 0; v < _adj.Length; v++)
            {
                foreach (int w in _adj[v])
                {
                    reversedGraph.AddEdge(w, v);
                }
            }

            return reversedGraph;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            string output = string.Empty;
            for (int v = 0; v < _adj.Length; v++)
            {
                foreach (int w in _adj[v])
                {
                    output += $"{v}->{w}\n";
                }
            }
            return output;
        }
    }
}
