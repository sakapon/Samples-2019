using System;
using System.Linq;

class C
{
    static void Main()
    {
        Console.ReadLine();
        var a = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        var b = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

        var x = 0L;
        for (var i = 0; i < b.Length; i++)
        {
            var m = Math.Min(a[i], b[i]);
            x += m;
            b[i] -= m;
            var n = Math.Min(a[i + 1], b[i]);
            x += n;
            a[i + 1] -= n;
        }
        Console.WriteLine(x);
    }
}
