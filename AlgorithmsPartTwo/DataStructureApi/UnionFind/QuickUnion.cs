using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataStructureApi.UnionFind
{
    /// <summary>
    /// The following is based off of a quick-union algorithm.
    /// Although this algorithm may perform better on average, it is still not performant. N*M
    /// Think of the potential sizes of each tree and how large they can become.
    /// </summary>
    internal class QuickUnion
    {
        private int _objects;
        private int[] _id;

        public int Count => _objects;

        /// <summary>
        /// Createa union-find data strucutre with n objects
        /// </summary>
        /// <param name="n"></param>
        public QuickUnion(int n)
        {
            _objects = n;
            _id = new int[n];

            for (int i = 0; i < _id.Length; i++)
            {
                _id[i] = i;
            }
        }

        public static QuickUnion CreateQuickUnionFromFile(string fileName)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(fileName))
                {
                    // Read the stream to a string line by line
                    // First line is the number of objects
                    int objects = int.Parse(sr.ReadLine());

                    QuickUnion qu = new QuickUnion(objects);

                    while (!sr.EndOfStream)
                    {
                        string[] input = sr.ReadLine().Split(" ");
                        int p = int.Parse(input[0]);
                        int q = int.Parse(input[1]);

                        if (!qu.IsConnected(p, q))
                        {
                            qu.AddUnion(p, q);
                        }
                    }

                    return qu;
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

            _id[i] = j;
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
