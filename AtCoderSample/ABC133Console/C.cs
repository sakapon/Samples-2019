using System;
using System.Linq;

class C
{
    static void Main()
    {
        var a = Console.ReadLine().Split(' ').Select(long.Parse).ToArray();

        var m = long.MaxValue;
        for (var i = a[0]; i < a[1]; i++)
            for (var j = i + 1; j <= a[1]; j++)
            {
                var x = i * j % 2019;
                if (x == 0) { Console.WriteLine(0); return; }
                if (x < m) m = x;
            }
        Console.WriteLine(m);
    }
}
