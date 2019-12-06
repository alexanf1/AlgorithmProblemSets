﻿using System.Collections.Generic;
using System.IO;
using GraphApi.Interfaces;

namespace GraphApi.UndirectedGraph
{
    /// <summary>
    /// *Note that an you can maintain a list of edges as a Linked List or an Array.
    /// 
    /// List of Edges vs Adjacency-Matrix vs Adjacency-List representation
    /// [Performance - List of edges]
    ///     Space:.............................. E
    ///     Add Edge:........................... 1
    ///     Edge exists between v and w:........ E
    ///     Iterate over vertices adjacent to v: E
    /// [Performance - Adjacency-Matrix]
    ///     Space:.............................. V^2
    ///     Add Edge:........................... 1 (disallows parallel edges)
    ///     Edge exists between v and w:........ 1
    ///     Iterate over vertices adjacent to v: V
    /// [Performance - Adjacency-List]
    ///     Space:.............................. E + V
    ///     Add Edge:........................... 1
    ///     Edge exists between v and w:........ degree(V)
    ///     Iterate over vertices adjacent to v: degree(V)
    /// 
    /// *In practice most graphs are sparse (Large number of Vertices, Small amount of edges)
    /// *Therefore, it is wise to use an adjacency-list as an underlying representation of a graph
    /// </summary>
    internal class UndirectedGraph : IGraph
    {
        private int _vertices;
        private int _edges;
        private LinkedList<int>[] _adj;

        public static UndirectedGraph InitializeGraph(string fileName)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(fileName))
                {
                    // Read the stream to a string line by line
                    // First line is the number of vertices
                    int vertices = int.Parse(sr.ReadLine());
                    int edges = int.Parse(sr.ReadLine());

                    UndirectedGraph g = new UndirectedGraph(vertices);

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
        public UndirectedGraph(int vertices)
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

        public override string ToString()
        {
            string output = string.Empty;
            for(int v = 0; v < _adj.Length; v++)
            {
                foreach(int w in _adj[v])
                {
                    output += $"{v}-{w}\n";
                }
            }
            //Print each edge as "v-w"
            return output;
        }
    }
}
