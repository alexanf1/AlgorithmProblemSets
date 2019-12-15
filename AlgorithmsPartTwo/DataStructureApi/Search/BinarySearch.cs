using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructureApi.Search
{
    public static class BinarySearch
    {
        /// <summary>
        /// Returns the index of the specified key in the provided array.
        /// </summary>
        /// <param name="arr">the array of integers, and must be sorted in ascending order</param>
        /// <param name="key">the key to look for in the provided array</param>
        /// <returns>index of the key in the array. returns -1 if no key present</returns>
        public static int GetIndexOf(int[] arr, int key)
        {
            int minIndx = 0;
            int maxIndx = arr.Length - 1;
            while(minIndx != maxIndx)
            {
                int midIndx = minIndx + (maxIndx - minIndx) / 2;

                if (key < arr[midIndx])
                    maxIndx = midIndx - 1;
                else if (key > arr[midIndx])
                    minIndx = midIndx + 1;
                else
                    return midIndx;
            }

            return -1;
        }
    }
}
