using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructureApi.PriorityQueue.Interfaces
{
    /// <summary>
    /// A interface representing the commands for a priority-queue data structure
    /// </summary>
    interface IMaxPriorityQueue<T>
    {
        /// <summary>
        /// Inserts a key into the priority queue
        /// </summary>
        /// <param name="key"></param>
        void Insert(T key);

        /// <summary>
        /// Removes and returns the largest key
        /// </summary>
        /// <returns></returns>
        T DeleteMax();

        /// <summary>
        /// Determines if the priority queue is empty
        /// </summary>
        /// <returns></returns>
        bool IsEmpty();
    }
}
