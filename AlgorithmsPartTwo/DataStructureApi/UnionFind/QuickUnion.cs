using System.IO;
using DataStructureApi.UnionFind.Interface;

namespace DataStructureApi.UnionFind
{
    /// <summary>
    /// The following implementation is based off of a quick union (lazy) algorithm.
    /// Although this algorithm may perform better on average, it is still not performant. O(N * M)
    /// Think of the potential sizes of each tree and how large they can become.
    /// Note: It also benefical to think about the number or write vs read operations
    /// Performance:
    /// initialize - O(N)
    /// union - O(N) * very few writes required
    /// connected - O(N)
    /// </summary>
    internal class QuickUnion : IUnionFind
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

        /// <inheritdoc/>
        public void AddUnion(int p, int q)
        {
            // set the id of p's root to the id of q's root
            // requires a look up for each root
            int i = Find(p);
            int j = Find(q);

            // prevents adding a connection that already exists.
            if (i == j)
                return;

            _id[i] = j;
        }

        /// <inheritdoc/>
        public bool IsConnected(int p, int q)
        {
            return Find(p) == Find(q);
        }

        /// <summary>
        /// Returns the component identifier for vertex x
        /// </summary>
        /// <returns></returns>
        private int Find(int x)
        {
            // You can think of locating the root as id[id[id[....]]] until its equal to itself
            int root = x;
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
