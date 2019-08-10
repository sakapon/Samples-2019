using System;
using System.Linq;

class A
{
    static void Main()
    {
        var a = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        Console.WriteLine(Math.Min(a[0] * a[1], a[2]));
    }
}
