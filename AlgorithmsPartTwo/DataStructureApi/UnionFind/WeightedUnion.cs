using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataStructureApi.UnionFind
{
    /// <summary>
    /// This demonstrates further improvements on the quick-union implementation using
    /// a weigth and path compression adjustments.
    /// The weight improvement alones makes this an N + M log N, and with path compression
    /// this almost makes it linear... but not quite.
    /// </summary>
    internal class WeightedUnion
    {
        private int _objects;
        private int[] _id;
        private int[] _sz;

        public int Count => _objects;

        /// <summary>
        /// Createa union-find data strucutre with n objects
        /// </summary>
        /// <param name="n"></param>
        public WeightedUnion(int n)
        {
            _objects = n;
            _id = new int[n];
            _sz = new int[n];

            for (int i = 0; i < _id.Length; i++)
            {
                _id[i] = i;
                _sz[i] = 0;
            }
        }

        public static WeightedUnion CreateQuickUnionFromFile(string fileName)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(fileName))
                {
                    // Read the stream to a string line by line
                    // First line is the number of objects
                    int objects = int.Parse(sr.ReadLine());

                    WeightedUnion wu = new WeightedUnion(objects);

                    while (!sr.EndOfStream)
                    {
                        string[] input = sr.ReadLine().Split(" ");
                        int p = int.Parse(input[0]);
                        int q = int.Parse(input[1]);

                        if (!wu.IsConnected(p, q))
                        {
                            wu.AddUnion(p, q);
                        }
                    }

                    return wu;
                }
            }
            catch (IOException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Add a connection between p and q
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        public void AddUnion(int p, int q)
        {
            int i = Find(p);
            int j = Find(q);

            // This is the portion of weighted improvement component
            // Note: This prevents trying to union two objects that are already in the same set.
            if (i == j)
                return;

            if(_sz[i] < _sz[j])
            {
                _id[i] = j;
                _sz[j] += _sz[i];
            }
            else
            {
                _id[j] = i;
                _sz[i] += _sz[j];
            }
        }

        /// <summary>
        /// Determine if p and q are in the same component
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public bool IsConnected(int p, int q)
        {
            return Find(p) == Find(q);
        }

        /// <summary>
        /// Returns the component identifier for p
        /// </summary>
        /// <returns></returns>
        private int Find(int p)
        {
            int root = p;
            while (_id[root] != root)
            {
                // This is the path compression component
                // make every node in path point to grandparent (halving path length search)
                // not the same as flattening but it's definitely a huge improvement
                _id[root] = _id[_id[root]];
                root = _id[root];
            }

            return root;
        }

        public override string ToString()
        {
            string output = string.Empty;
            for (int i = 0; i < _id.Length; i++)
            {
                output += $"{_id[i]},";
            }

            return output;
        }
    }
}
