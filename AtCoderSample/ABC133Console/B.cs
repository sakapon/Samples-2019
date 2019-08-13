using System;
using System.Linq;

class B
{
    static void Main()
    {
        var a = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        var ps = Enumerable.Range(0, a[0]).Select(i => Console.ReadLine().Split(' ').Select(int.Parse).ToArray()).ToArray();

        var c = Enumerable.Range(0, a[0]).SelectMany(i => Enumerable.Range(i + 1, a[0] - i - 1).Select(j => new { i, j }))
            .Count(_ =>
            {
                var d = Math.Sqrt(Enumerable.Range(0, a[1]).Sum(k => Math.Pow(ps[_.i][k] - ps[_.j][k], 2)));
                return d == (int)d;
            });
        Console.WriteLine(c);
    }
}
