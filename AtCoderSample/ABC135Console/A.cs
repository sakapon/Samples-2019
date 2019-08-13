using System;
using System.Linq;

class A
{
    static void Main()
    {
        var a = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        Console.WriteLine(a.Sum() % 2 == 0 ? a.Average().ToString() : "IMPOSSIBLE");
    }
}
