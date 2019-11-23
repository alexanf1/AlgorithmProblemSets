using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GraphApi
{
    internal class EdgeWeightedGraph
    {
        private int _vertices;
        private int _edges;
        private LinkedList<Edge>[] _adj;

        /// <summary>
        /// Creates an empty graph with v vertices
        /// </summary>
        /// <param name="v"></param>
        public EdgeWeightedGraph(int v)
        {
            _vertices = v;

            _adj = new LinkedList<Edge>[_vertices];
            for (int i = 0; i < _adj.Length; i++)
            {
                _adj[i] = new LinkedList<Edge>();
            }
        }

        /// <summary>
        /// Creates a graph from a particular file. Particular formatting is required.
        /// </summary>
        /// <param name="fileName"></param>
        public static EdgeWeightedGraph CreateEdgeWeightGraphFromFile(string fileName)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(fileName))
                {
                    // Read the stream to a string line by line
                    // First line is the number of vertices
                    int vertices = int.Parse(sr.ReadLine());
                    int edges = int.Parse(sr.ReadLine());

                    EdgeWeightedGraph g = new EdgeWeightedGraph(vertices);

                    while (!sr.EndOfStream)
                    {
                        string[] input = sr.ReadLine().Split(" ");
                        int v = int.Parse(input[0]);
                        int w = int.Parse(input[1]);
                        double weight = double.Parse(input[2]);

                        Edge edge = new Edge(v, w, weight);
                        g.AddEdge(edge);
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
        /// Adds an edge to the graph
        /// </summary>
        /// <param name="v">The first vertex</param>
        /// <param name="w">The second vertex</param>
        public void AddEdge(Edge e)
        {
            int v = e.GetEitherEdge();
            int w = e.GetOtherEdge(v);

            _adj[v].AddLast(e);
            _adj[w].AddLast(e);

            _edges += 2;
        }

        /// <summary>
        /// Returns all adjacent edges from a given vertex
        /// </summary>
        /// <param name="v">The vertex in question</param>
        /// <returns></returns>
        ICollection<Edge> GetAdjacentEdges(int v)
        {
            return _adj[v];
        }

        /// <summary>
        /// Returns all edges in the graph
        /// </summary>
        /// <param name="v">The vertex in question</param>
        /// <returns></returns>
        ICollection<Edge> GetAllEdges()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the number of vertices in the entire graph
        /// </summary>
        /// <returns></returns>
        public int GetNumberOfVertices()
        {
            return _vertices;
        }

        /// <summary>
        /// Returns the number of edges in the entire graph
        /// </summary>
        /// <returns></returns>
        int GetNumberOfEdges()
        {
            return _edges;
        }

        /// <summary>
        /// String representation of a graph
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string output = string.Empty;
            for (int v = 0; v < _adj.Length; v++)
            {
                foreach(Edge e in _adj[v])
                {
                    output += $"vertex:{v}, {e}\n";
                }
            }

            return output;
        }
    }
}
