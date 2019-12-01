using System;
using DataStructureApi.PriorityQueue;

namespace DataStructureApi
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] c_arr = new char[] { 'A', 'S', 'O', 'R', 'T', 'I', 'N', 'G' };
            IndexMinPriorityQueue<char> test = new IndexMinPriorityQueue<char>(c_arr.Length);
            test.Insert(0, c_arr[0]);
            test.Insert(1, c_arr[1]);
            test.Insert(2, c_arr[2]);
            test.Insert(3, c_arr[3]);
            test.Insert(4, c_arr[4]);
            test.Insert(5, c_arr[5]);
            test.Insert(6, c_arr[6]);
            test.Insert(7, c_arr[7]);

            //char min = test.DeleteMin();
            //Console.WriteLine($"min=={min}");
            Console.WriteLine(test);
        }
    }
}
