using System;
using System.Linq;

class B
{
    static void Main()
    {
        var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
        var m = Math.Max(a[1] - a[0] + 1, -1000000);
        var M = Math.Min(a[1] + a[0] - 1, 1000000);
        Console.WriteLine(string.Join(" ", Enumerable.Range(m, M - m + 1)));
    }
}
