using System.IO;
using DataStructureApi.UnionFind.Interface;

namespace DataStructureApi.UnionFind
{
    /// <summary>
    /// The following implementation is based off a quick-find (eager) algorithm.
    /// For every M union operations, we will have to perform N access lookups. O(N * M)
    /// Note: It also benefical to think about the number or write vs read operations
    /// Performance:
    /// initialize - O(N)
    /// union - O(N)* many writes required
    /// connected - O(1)
    /// </summary>
    internal class QuickFind : IUnionFind
    {
        private int _objects;
        private int[] _id;

        public int Count => _objects;

        /// <summary>
        /// Createa union-find data strucutre with n objects
        /// Notice that each vertex is represented with a unique id
        /// </summary>
        /// <param name="n"></param>
        public QuickFind(int n)
        {
            _objects = n;
            _id = new int[n];

            for (int i = 0; i < _id.Length; i++)
            {
                _id[i] = i; // assign a unique identifier
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

        /// <inheritdoc/>
        public void AddUnion(int p, int q)
        {
            // anything that has the id of p now must be q 's id
            int prevId = Find(p);
            int newId = Find(q);

            // prevents adding a connection that already exists.
            if (prevId == newId)
                return;

            // loop through the entire array possible changing ids
            for (int i = 0; i < _id.Length; i++)
            {
                if(_id[i] == prevId)
                {
                    _id[i] = newId;
                }
            }
        }

        /// <inheritdoc/>
        public bool IsConnected(int p, int q)
        {
            // if p and q have the same id then they are connected
            return _id[p] == _id[q];
        }

        /// <summary>
        /// Returns the component identifier for the vertex x
        /// </summary>
        /// <returns></returns>
        private int Find(int x)
        {
            return _id[x];
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
