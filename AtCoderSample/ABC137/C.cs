using System;
using System.Linq;

class C
{
    static void Main()
    {
        var n = int.Parse(Console.ReadLine());
        var a = Enumerable.Range(0, n).Select(i => new string(Console.ReadLine().OrderBy(s => s).ToArray())).GroupBy(s => s).Where(g => g.Count() > 1).Select(g => g.Count()).Sum(c => (long)c * (c - 1) / 2);
        Console.WriteLine(a);
    }
}
