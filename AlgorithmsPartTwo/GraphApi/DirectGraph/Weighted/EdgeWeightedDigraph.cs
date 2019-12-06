using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GraphApi.DirectGraph.Weighted
{
    /// <summary>
    /// Shortest Path Variants:
    ///     Single Source: from one vertex 's' to every other vertex
    ///     Source-link: from one vertex 's' to another 't'
    ///     All pairs: between all pairs of vertices
    /// Restrictions on Edge Weights:
    ///     Nonnegative weights
    ///     Euclidean weights
    ///     Arbitrary weights
    /// Cycles?    
    ///     No directed cycles
    ///     No "negative" cycles
    /// </summary>
    internal class EdgeWeightedDigraph
    {
        private int _vertices;
        private int _edges;
        private LinkedList<DirectedEdge>[] _adj;
        private LinkedList<DirectedEdge> _allEdges; // TODO: may be redudant 

        /// <summary>
        /// Creates an empty graph with v vertices
        /// </summary>
        /// <param name="v"></param>
        public EdgeWeightedDigraph(int v)
        {
            _allEdges = new LinkedList<DirectedEdge>();

            _vertices = v;

            _adj = new LinkedList<DirectedEdge>[_vertices];
            for (int i = 0; i < _adj.Length; i++)
            {
                _adj[i] = new LinkedList<DirectedEdge>();
            }
        }

        /// <summary>
        /// Creates a graph from a particular file. Particular formatting is required.
        /// </summary>
        /// <param name="fileName"></param>
        public static EdgeWeightedDigraph CreateEdgeWeightGraphFromFile(string fileName)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(fileName))
                {
                    // Read the stream to a string line by line
                    // First line is the number of vertices
                    int vertices = int.Parse(sr.ReadLine());
                    int edges = int.Parse(sr.ReadLine());

                    EdgeWeightedDigraph g = new EdgeWeightedDigraph(vertices);

                    while (!sr.EndOfStream)
                    {
                        string[] input = sr.ReadLine().Split(" ");
                        int v = int.Parse(input[0]);
                        int w = int.Parse(input[1]);
                        double weight = double.Parse(input[2]);

                        DirectedEdge edge = new DirectedEdge(v, w, weight);
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
        public void AddEdge(DirectedEdge e)
        {
            int v = e.GetFrom();
            _adj[v].AddLast(e);

            _allEdges.AddLast(e);

            _edges++;
        }

        /// <summary>
        /// Returns all adjacent edges from a given vertex
        /// </summary>
        /// <param name="v">The vertex in question</param>
        /// <returns></returns>
        public ICollection<DirectedEdge> GetAdjacentEdges(int v)
        {
            return _adj[v];
        }

        /// <summary>
        /// Returns all edges in the graph
        /// </summary>
        /// <param name="v">The vertex in question</param>
        /// <returns></returns>
        public ICollection<DirectedEdge> GetAllEdges()
        {
            return _allEdges;
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
        public int GetNumberOfEdges()
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
                foreach (DirectedEdge e in _adj[v])
                {
                    output += $"vertex:{v}, {e}\n";
                }
            }

            return output;
        }
    }
}
