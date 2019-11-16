using System;
using System.Collections.Generic;
using System.IO;
using Algorithms.GraphApi.Interfaces;

namespace Algorithms.GraphApi
{
    internal class Graph : IGraph
    {
        private int _vertices;
        private int _edges;
        private LinkedList<int>[] _adj;

        public static Graph InitializeGraph(string fileName)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(fileName))
                {
                    // Read the stream to a string line by line
                    // First line is the number of vertices
                    int vertices = int.Parse(sr.ReadLine());
                    int edges = int.Parse(sr.ReadLine());

                    Graph g = new Graph(vertices);

                    while(!sr.EndOfStream)
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
        public Graph(int vertices)
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
            _adj[w].AddLast(v);
            _edges += 2;
        }

        /// <inheritdoc/>
        public ICollection<int> GetAdjacentVertices(int v)
        {
            return _adj[v];
        }

        /// <inheritdoc/>
        public int GetNumberOfEdges()
        {
            return _edges / 2;
        }

        /// <inheritdoc/>
        public int GetNumberOfVertices()
        {
            return _vertices;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            //Print each edge as "v-w"
            return base.ToString();
        }
    }
}
