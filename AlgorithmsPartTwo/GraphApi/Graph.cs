using System;
using System.Collections.Generic;
using System.IO;
using Algorithms.GraphApi.Interfaces;

namespace Algorithms.GraphApi
{
    internal class Graph : IGraph
    {
        public Graph(string fileName)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(fileName))
                {
                    // Read the stream to a string line by line
                    // First line is the number of vertices
                    string vertices = sr.ReadLine();
                    string edges = sr.ReadLine();
                    Console.WriteLine($"vertices:{vertices}, edges:{edges}");
                }
            }
            catch (IOException e)
            {
                throw e;
            }
        }

        public void AddEdge(int v, int w)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<int> GetAdjacentVertices(int v)
        {
            throw new System.NotImplementedException();
        }

        public int GetNumberOfEdges()
        {
            throw new System.NotImplementedException();
        }

        public int GetNumberOfVertices()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
