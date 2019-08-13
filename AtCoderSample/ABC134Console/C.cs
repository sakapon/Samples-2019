using System;
using System.Linq;

class C
{
    static void Main()
    {
        var n = int.Parse(Console.ReadLine());
        var a = Enumerable.Range(1, n).Select(i => int.Parse(Console.ReadLine())).ToArray();
        var M = a.Max();
        var k = Array.IndexOf(a, M);
        for (var i = 0; i < n; i++) Console.WriteLine(i == k ? a.Where((x, j) => j != i).Max() : M);
    }
}
