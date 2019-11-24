using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataStructureApi.UnionFind
{
    /// <summary>
    /// The following implementation is based off a quick-find algorithm.
    /// For every M union operations, we will have to perform N access lookups. N*M
    /// </summary>
    internal class QuickFind
    {
        private int _objects;
        private int[] _id;

        public int Count => _objects;

        /// <summary>
        /// Createa union-find data strucutre with n objects
        /// </summary>
        /// <param name="n"></param>
        public QuickFind(int n)
        {
            _objects = n;
            _id = new int[n];

            for (int i = 0; i < _id.Length; i++)
            {
                _id[i] = i;
            }
        }

        public static QuickFind CreateUnionFindFromFile(string fileName)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(fileName))
                {
                    // Read the stream to a string line by line
                    // First line is the number of objects
                    int objects = int.Parse(sr.ReadLine());

                    QuickFind qf = new QuickFind(objects);

                    while (!sr.EndOfStream)
                    {
                        string[] input = sr.ReadLine().Split(" ");
                        int p = int.Parse(input[0]);
                        int q = int.Parse(input[1]);

                        if(!qf.IsConnected(p, q))
                        {
                            qf.AddUnion(p, q);
                        }
                    }

                    return qf;
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
            // anything that has the id of p now must be q 's id
            int prevId = Find(p);
            int newId = Find(q);

            // prevents adding a connection that already exists.
            if (prevId == newId)
                return;

            for (int i = 0; i < _id.Length; i++)
            {
                if(_id[i] == prevId)
                {
                    _id[i] = newId;
                }
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
            return _id[p] == _id[q];
        }

        /// <summary>
        /// Returns the component identifier for p
        /// </summary>
        /// <returns></returns>
        private int Find(int p)
        {
            return _id[p];
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
