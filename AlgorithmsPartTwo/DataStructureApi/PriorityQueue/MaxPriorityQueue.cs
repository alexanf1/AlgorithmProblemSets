using System;
using DataStructureApi.PriorityQueue.Interfaces;

namespace DataStructureApi.PriorityQueue
{
    /// <summary>
    /// *It worth noting what the performance would be if this was written as an elementary PQ
    /// *Depending on what you are trying to achieve, its possible a naive implementation is actually better
    /// [UNORDERED ARRAY]
    ///     Performance:
    ///         Insert - O(1)
    ///         Delete Max/Min - O(N)
    ///         Find Max/Min - O(N)       
    /// [ORDERED ARRAY]
    ///     Performance:
    ///         Insert - O(N)
    ///         Delete Max/Min - O(1)
    ///         Find Max/Min - O(1)
    /// [BINARY HEAP]
    ///     Performance:
    ///         Insert - O(log N)
    ///         Delete Max/Min - O(log N)
    ///         Find Max/Min - O(1)
    ///    
    /// [Binary Tree]
    ///     Empty or node with links to left and right binary trees
    /// [Complete Tree]    
    ///     Perfectly balanced (as all things should be), except for the bottom level.
    /// *The height of a complete binary tree with N nodes is Log N
    /// 
    /// [Heap-Ordered Binary Tree]
    ///     Keys in nodes. Parent's key no smaller than children's keys.
    /// 
    /// Below is an array implementation of a heap-ordered complete binary tree
    /// The array representation also is based on 1-indexing
    /// 
    /// Nodes are also in level order*
    /// Largest/Smallest key is at the root*
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MaxPriorityQueue<T> : IMaxPriorityQueue<T> where T : IComparable<T>
    {
        private int _entries;
        private T[] _arr;

        /// <summary>
        /// The number of entries in the priority queue
        /// </summary>
        public int Size => _entries;

        /// <summary>
        /// Returns the largest key
        /// </summary>
        public T Max => _arr[1];

        /// <summary>
        /// Creates a priority with a limit on capacity
        /// </summary>
        /// <param name="capacity">Total number of elements</param>
        public MaxPriorityQueue(int capacity)
        {
            // Adding one extra to the size in order to take into account the empty root
            _arr = new T[capacity + 1];
        }

        /// <summary>
        /// Inserts a key into the priority queue. At most Log N compares
        /// </summary>
        public void Insert(T key)
        {
            _arr[++_entries] = key;
            PromoteKey(_entries);
        }

        /// <summary>
        /// Returns and removes the largest key
        /// </summary>
        /// <returns></returns>
        public T DeleteMax()
        {
            if (_entries < 0)
                return default;

            T max = _arr[1]; // the root holds the largest key
            ExchangeKeys(1, _entries--); // swap the maximum with the last entry
            _arr[_entries + 1] = default; // delete the maximum
            DemoteKey(1); // perform demotion on the new root

            return max;
        }

        /// <summary>
        /// Checks if the priority queue is empty
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return _entries == 0;
        }

        public override string ToString()
        {
            string output = string.Empty;
            for(int i = 0; i < _arr.Length; i++)
            {
                output += $"idx:{i}, val:{_arr[i]}\n";
            }

            return output;
        }

        #region - Private Methods
        /// <summary>
        /// Exchange key in larger child with key in parent
        /// </summary>
        /// <param name="k"></param>
        private void PromoteKey(int k)
        {
            // 'k/2' represents the parent node of node 'k'
            while (k > 1 && IsNodeKeyLargerThanParent(k, k/2))
            {
                // if node's key is larger than parent, swap keys
                ExchangeKeys(k, k / 2);

                // repeat this process until heap order is restored
                k = (k / 2);
            }
        }

        /// <summary>
        /// Exchange key in parent with key in larger child
        /// </summary>
        /// <param name="k"></param>
        private void DemoteKey(int k)
        {
            // 2*k represents the child's node
            while (2*k <= _entries) 
            {
                // first discover which child is larger before comparing with root
                int j = 2*k; // represents left child
                if (j < _entries && IsNodeKeyLargerThanParent(j + 1, j)) // is the right child larger than the left
                {
                    j++;
                }

                // 'j' now represents the larger of the two children
                if (!IsNodeKeyLargerThanParent(j, k))
                {
                    break;
                }

                ExchangeKeys(k, j);

                // Repeat until heap order is restored
                k = j;
            }
        }

        /// <summary>
        /// Swap keys
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void ExchangeKeys(int x, int y)
        {
            T temp = _arr[x];
            _arr[x] = _arr[y];
            _arr[y] = temp;
        }

        /// <summary>
        /// Determines if a node has a larger key than their parent/sibling
        /// </summary>
        /// <param name="x">node 'x'</param>
        /// <param name="y">parent/sibling node of 'x'</param>
        /// <returns></returns>
        private bool IsNodeKeyLargerThanParent(int x, int y)
        {
            return 0 < _arr[x].CompareTo(_arr[y]);
        }

        #endregion
    }
}
