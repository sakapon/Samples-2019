using System;
using System.Linq;

class A
{
    static void Main()
    {
        var a = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        var d = Math.Min(a[0] - a[1], a[2]);
        Console.WriteLine(a[2] - d);
    }
}
