using System;
using System.Collections.Generic;
using System.Text;
using DataStructureApi.PriorityQueue.Interfaces;

namespace DataStructureApi.PriorityQueue
{
    /// <summary>
    /// **See notes from MaxPriorityQueue
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MinPriorityQueue<T> : IMinPriorityQueue<T> where T : IComparable<T>
    {
        private int _entries;
        private T[] _arr;

        /// <summary>
        /// The number of entries in the priority queue
        /// </summary>
        public int Size => _entries;

        /// <summary>
        /// Returns the smallest key
        /// </summary>
        public T Max => _arr[1];

        /// <summary>
        /// Creates a priority with a limit on capacity
        /// </summary>
        /// <param name="capacity">Total number of elements</param>
        public MinPriorityQueue(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException();

            // Adding one extra to the size in order to take into account the empty root
            _arr = new T[capacity + 1];
        }

        /// <summary>
        /// Inserts a key into the priority queue
        /// </summary>
        public void insert(T key)
        {
            _arr[++_entries] = key;
            PromoteKey(_entries);
        }

        /// <summary>
        /// Returns and removes the smallest key
        /// </summary>
        /// <returns></returns>
        public T DeleteMin()
        {
            if (_entries < 0)
                return default;

            T min = _arr[1]; // holds the minimum value
            ExchangeKeys(1, _entries--); // swap the minimum with the last entry
            _arr[_entries + 1] = default; // delete the minium
            DemoteKey(1); // performing sink on the root

            return min;
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
            for (int i = 0; i < _arr.Length; i++)
            {
                output += $"idx:{i}, val:{_arr[i]}\n";
            }

            return output;
        }

        #region Private Methods
        /// <summary>
        /// Exchange key in smaller child with key in parent
        /// </summary>
        /// <param name="k"></param>
        private void PromoteKey(int k)
        {
            // 'k/2' represents the parent node of node 'k'
            while (k > 1 && IsNodeKeySmallerThanParent(k, k / 2))
            {
                // if node's key is smaller than parent, swap keys
                ExchangeKeys(k, k / 2);

                // repeat this process until heap order is restored
                k = (k / 2);
            }
        }

        /// <summary>
        /// Exchange key in parent with key in smaller child
        /// </summary>
        /// <param name="k"></param>
        private void DemoteKey(int k)
        {
            // 2*k represents the child's node
            while (2 * k <= _entries)
            {
                // first discover which child is smaller before comparing with root
                int j = 2 * k; // represents left child
                if (j < _entries && IsNodeKeySmallerThanParent(j + 1, j)) // is the right child smaller than the left
                {
                    j++;
                }

                // 'j' now represents the smaller of the two children
                if (!IsNodeKeySmallerThanParent(j, k))
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
        /// Determines if a node has a smaller key than their parent/sibling
        /// </summary>
        /// <param name="x">node 'x'</param>
        /// <param name="y">parent/sibling node of 'x'</param>
        /// <returns></returns>
        private bool IsNodeKeySmallerThanParent(int x, int y)
        {
            return 0 > _arr[x].CompareTo(_arr[y]);
        }
        #endregion
    }
}
