using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructureApi.PriorityQueue
{
    internal class MaxPriorityQueue<T> where T : IComparable<T>
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
            _arr = new T[capacity];
        }

        /// <summary>
        /// Inserts a key into the priority queue
        /// </summary>
        public void insert(T key)
        {
            _entries++;
            _arr[_entries] = key;

            // check if parent exists and if it is smaller than the current inserted element
            int k = _entries;
            
            while ((k/2) > 0 && greater(k, k / 2))
            {
                // if true than swap
                swap(k, k / 2);

                // repeat but now examining the new parent
                k = (k / 2);
            }
        }

        /// <summary>
        /// Returns and removes the largest key
        /// </summary>
        /// <returns></returns>
        public T DeleteMax()
        {
            if (_entries < 0)
                return default;

            T max = _arr[1]; // holds the maximum value
            swap(1, _entries); // swap the maximum with the last entry
            _arr[_entries] = default; // delete the maximum
            _entries--;
            sink(1); // performing sink on the root

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
        private void sink(int k)
        {
            while (2*k <= _entries) // because of the balance property, if there is no left child then there is none at all)
            {
                // first discover which child is larger before comparing with root
                int j = 2*k;
                if (j < _entries && greater(j + 1, j)) // is the right child larger than the left
                {
                    j++;
                }

                // 'j' now represents the larger of the two children
                if (!greater(j, k))
                {
                    break;
                }

                swap(k, j);
                k = j;
            }
        }

        private void swap(int x, int y)
        {
            T temp = _arr[x];
            _arr[x] = _arr[y];
            _arr[y] = temp;
        }

        private bool greater(int x, int y)
        {
            return 0 < _arr[x].CompareTo(_arr[y]);
        }
        #endregion
    }
}
