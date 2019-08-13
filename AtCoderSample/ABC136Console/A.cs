using System;
using System.Linq;

class A
{
    static void Main()
    {
        var a = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        Console.WriteLine(a[2] - Math.Min(a[0] - a[1], a[2]));
    }
}
