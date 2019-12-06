using System;

namespace DataStructureApi.PriorityQueue.Interfaces
{
    /// <summary>
    /// A interface representing the commands for a priority-queue data structure
    /// </summary>
    interface IMinPriorityQueue<T>
    {
        /// <summary>
        /// Inserts a key into the priority queue
        /// </summary>
        /// <param name="key"></param>
        void insert(T key);

        /// <summary>
        /// Removes and returns the smallest key
        /// </summary>
        /// <returns></returns>
        T DeleteMin();

        /// <summary>
        /// Determines if the priority queue is empty
        /// </summary>
        /// <returns></returns>
        bool IsEmpty();
    }
}
