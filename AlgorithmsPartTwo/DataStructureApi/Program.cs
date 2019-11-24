using System;
using DataStructureApi.UnionFind;

namespace DataStructureApi
{
    class Program
    {
        static void Main(string[] args)
        {
            /*UF uf = new UF(10);
            uf.AddUnion(4, 3);
            uf.AddUnion(3, 8);
            uf.AddUnion(6, 5);
            uf.AddUnion(9, 4);
            uf.AddUnion(2, 1);
            uf.AddUnion(5, 0);
            uf.AddUnion(7, 2);
            uf.AddUnion(6, 1);

            Console.WriteLine($"{uf}");*/

            WeightedUnion wu = new WeightedUnion(10);
            wu.AddUnion(4, 3);
            wu.AddUnion(3, 8);
            wu.AddUnion(6, 5);
            wu.AddUnion(9, 4);
            wu.AddUnion(2, 1);
            wu.AddUnion(5, 0);
            wu.AddUnion(7, 2);
            wu.AddUnion(6, 1);
            wu.AddUnion(7, 3);

            Console.WriteLine($"{wu}");
        }
    }
}
