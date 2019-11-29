using System;
using DataStructureApi.PriorityQueue;

namespace DataStructureApi
{
    class Program
    {
        static void Main(string[] args)
        {
            MaxPriorityQueue<char> trial = new MaxPriorityQueue<char>(12);
            trial.insert('T');
            trial.insert('P');
            trial.insert('R');
            trial.insert('N');
            trial.insert('H');
            trial.insert('O');
            trial.insert('A');
            trial.insert('E');
            trial.insert('I');
            trial.insert('G');
            trial.insert('S');


            trial.DeleteMax();
            Console.WriteLine($"size:{trial.Size}");
            Console.WriteLine($"{trial}");
        }
    }
}
