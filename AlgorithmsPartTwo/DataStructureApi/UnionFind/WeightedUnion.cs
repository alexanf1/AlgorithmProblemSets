using System.IO;
using DataStructureApi.UnionFind.Interface;

namespace DataStructureApi.UnionFind
{
    /// <summary>
    /// This demonstrates further improvements on the quick-union implementation using weigth and path
    /// compression adjustments.
    /// The weight modification avoids having tall trees.
    /// The weight improvement alones makes this an O(N + M log N), and with path compression
    /// this almost makes it linear... but not quite.
    /// Performance:
    /// initialize - O(N)
    /// union - O(log N) * very few writes required
    /// connected - O(log N)
    /// weighted QU + path compression = O(N + M log* N)(practically linear)
    /// </summary>
    public class WeightedUnion : IUnionFind
    {
        private int _objects;
        private int[] _id;
        private int[] _sz; // used to keep track of the number of objects in each tree

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

        /// <inheritdoc/>>
        public void AddUnion(int p, int q)
        {
            int i = Find(p);
            int j = Find(q);

            // prevents adding a connection that already exists.
            if (i == j)
                return;

            // This is the portion involving checking heights of each tree
            // we use the roots to determine the height of each tree
            if (_sz[i] < _sz[j])
            {
                _id[i] = j; // assign the root as usual
                _sz[j] += _sz[i]; // since the root of i is now pointing to j, j has now grown. Add the objects
            }
            else
            {
                _id[j] = i;
                _sz[i] += _sz[j];
            }
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
            int root = x;
            while (_id[root] != root)
            {
                // This is the path compression component
                // make every node in path point to grandparent (halving path length search)
                // not the same as flattening completely but it's definitely a huge improvement
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
