using System;

namespace DataStructureApi.PriorityQueue
{
    /// <summary>
    /// A cleass representing an index min priority queue where the priority of a key can change
    /// 
    /// Associate an index between 0 and N - 1 with each key in a priority queue.
    /// Client can change the key by specifying the index
    ///     
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IndexMinPriorityQueue<T> where T : IComparable<T>
    {
        private int _entries;
        private T[] _keys; // _key[i] is the priority of i
        private int[] _pq; // _pq[i] is the index of the key in heap position i
        private int[] _qp; // _qp[i] is the heap position of the key with index i

        /// <summary>
        /// The number of entries in the priority queue
        /// </summary>
        public int Size => _entries;

        /// <summary>
        /// Returns the minimum key
        /// </summary>
        public T MinKey => _keys[_pq[1]];

        /// <summary>
        /// Returns the index of the minimum Key
        /// </summary>
        public int MinIndex => _pq[1];

        public IndexMinPriorityQueue(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException();

            // Note: we are implementing this indexMinPQ with 1-based indexing
            _keys = new T[capacity + 1];
            _pq = new int[capacity + 1];
            _qp = new int[capacity + 1];

            for (int i = 0; i <= capacity; i++)
            {
                _qp[i] = -1;
                _pq[i] = -1;
            }   
        }

        /// <summary>
        /// Inserts associate key with index 'i'
        /// </summary>
        /// <param name="key"></param>
        public void Insert(int i, T key)
        {
            _entries++;

            _keys[i] = key;
            _pq[_entries] = i; // given a heap position you want to return the key index 'i'
            _qp[i] = _entries; // given a key index 'i' you want to return the heap position

            // _entries represents the latest heap position
            swim(_entries);
        }

        /// <summary>
        /// Decrease the key associated with index 'i'
        /// </summary>
        /// <param name="key"></param>
        public void ChangeKey(int i, T key)
        {
            // Note: you need to keep this strict and simply only allow increases or decreases

            _keys[i] = key; // assign new key given the index position

            // use the given key index to find the heap position (with _qp) to begin sink operation
            // This only works when the key is smaller than the original
            swim(_qp[i]);
        }

        /// <summary>
        /// Removes and returns the minimal key's index
        /// </summary>
        /// <returns></returns>
        public int DeleteMin()
        {
            if (_entries <= 0)
                return default; // Should throw instead...

            int indexOfMinMaxKey = _pq[1]; // returns the key index of the min/max key

            swap(1, _entries); // swap heap position 1 with the last position

            _keys[indexOfMinMaxKey] = default;
            _qp[indexOfMinMaxKey] = -1; // min/am key index should no longer point to a position
            _pq[_entries] = -1; // last heap position should also no longer point to a key index

            _entries--;
            sink(1); // performing sink on the root

            return indexOfMinMaxKey;
        }

        /// <summary>
        /// Does the key index exist on the priority queue
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public bool Contains(int i)
        {
            return _qp[i] != -1;
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
            for (int i = 0; i < _keys.Length; i++)
            {
                output += $"[{i}] == {_keys[i]},{_pq[i]},{_qp[i]}\n";
            }

            return output;
        }

        #region Private Methods
        private void sink(int k)
        {
            while (2 * k <= _entries) // because of the balance property, if there is no left child then there is none at all)
            {
                // first discover which child is larger before comparing with root
                int j = 2 * k;
                if (j < _entries && lesser(j + 1, j)) // is the right child larger than the left
                {
                    j++;
                }

                // 'j' now represents the larger of the two children
                if (!lesser(j, k))
                {
                    break;
                }

                swap(k, j);
                k = j;
            }
        }

        private void swim(int k)
        {
            while ((k / 2) > 0 && lesser(k, k / 2))
            {
                swap(k, k / 2);
                k = (k / 2);
            }
        }

        private void swap(int x, int y)
        {
            int swap = _pq[x];
            _pq[x] = _pq[y];
            _pq[y] = swap;

            _qp[_pq[x]] = x;
            _qp[_pq[y]] = y;
        }

        /// <summary>
        /// 'x' and 'y' represent heap positions to compare
        /// </summary>
        /// <param name="x">heap position x</param>
        /// <param name="y">heap position y</param>
        /// <returns></returns>
        private bool lesser(int x, int y)
        {
            return _keys[_pq[x]].CompareTo(_keys[_pq[y]]) < 0;
        }
        #endregion
    }
}
