using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructureApi.PriorityQueue.Sorting
{
    /// <summary>
    /// Demonstrates a heap sort algorithm. See 'MaxPriorityQueue' for more comments.
    /// Performance:
    ///     IN-PLACE
    ///     NOT STABLE (duplicate keys may not always be the same order)
    ///     O (N LOG N) runtime GUARANTEE
    /// *Makes poor use of cache memory (refererences can be all over the place in a huge array)
    ///     and you may have to do long distance swaps in memory as well.
    /// *Inner loop takes longer than quicksort's
    /// *Not stable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class BinaryHeap<T> where T : IComparable<T>
    {
        public static void Sort(T[] arr)
        {
            // Begin the construction of a min/max binary heap IN-PLACE
            // Start at the bottom most node with children and work your way up to the root.
            // This will make the array heap-ordered with the largest key at the root.
            int N = arr.Length;
            for(int k = N/2; k >= 1; k--)
            {
                DemoteKey(arr, k, N);
            }

            // In order to sort we begin "fake" deleting the largest/smallest remaining item
            // To do this, we first swap the largest/smallest key with the bottom most node and
            // then we re-sorting using the Demote method on the root. Repeat all the way down the tree
            while (N > 1)
            {
                ExchangeKeys(arr, 1, N);
                DemoteKey(arr, 1, --N);
            }
        }

        public static void DemoteKey(T[] arr, int k, int N)
        {
            while (2*k <= N)
            {
                int j = 2 * k;
                if (j < N && IsNodeKeyLargerThanParent(arr, j + 1, j))
                {
                    j++;
                }

                if (!IsNodeKeyLargerThanParent(arr, j, k))
                {
                    break;
                }

                ExchangeKeys(arr, k, j);
                k = j;
            }
        }

        private static bool IsNodeKeyLargerThanParent(T[] arr, int x, int y)
        {
            return 0 < arr[x-1].CompareTo(arr[y-1]);
        }

        private static void ExchangeKeys(T[] arr, int x, int y)
        {
            T temp = arr[x-1];
            arr[x-1] = arr[y-1];
            arr[y-1] = temp;
        }
    }
}
