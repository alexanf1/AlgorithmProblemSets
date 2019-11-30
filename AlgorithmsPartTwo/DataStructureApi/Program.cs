using System;
using DataStructureApi.PriorityQueue;

namespace DataStructureApi
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] c_arr = new char[] { 'S', 'O', 'R', 'T', 'E', 'X', 'A', 'M', 'P', 'L', 'E' };
            BinaryHeap<char>.Sort(c_arr);
            Console.WriteLine(c_arr);
        }
    }
}
