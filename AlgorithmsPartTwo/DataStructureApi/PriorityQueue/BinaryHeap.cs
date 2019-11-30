using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructureApi.PriorityQueue
{
    internal static class BinaryHeap<T> where T : IComparable<T>
    {
        public static void Sort(T[] arr)
        {
            // NOTE: assume that the array given is at index 0 for the root
            
            // Begin the construction of the binary heap
            int N = arr.Length;
            for(int k = N/2; k >= 1; k--)
            {
                sink(arr, k, N);
            }

            // Begin to sort the binary heap
            while (N > 1)
            {
                swap(arr, 1, N);
                sink(arr, 1, --N);
            }
        }

        private static void sink(T[] arr, int k, int N)
        {
            while (2 * k <= N) // because of the balance property, if there is no left child then there is none at all)
            {
                // first discover which child is larger before comparing with root
                int j = 2 * k;
                if (j < N && greater(arr, j + 1, j)) // is the right child larger than the left
                {
                    j++;
                }

                // 'j' now represents the larger of the two children
                if (!greater(arr, j, k))
                {
                    break;
                }

                swap(arr, k, j);
                k = j;
            }
        }

        // NOTE: we are subtracting one when indexing to account for the offset mentioned above
        private static bool greater(T[] arr, int x, int y)
        {
            return 0 < arr[x-1].CompareTo(arr[y-1]);
        }

        private static void swap(T[] arr, int x, int y)
        {
            T temp = arr[x-1];
            arr[x-1] = arr[y-1];
            arr[y-1] = temp;
        }
    }
}
