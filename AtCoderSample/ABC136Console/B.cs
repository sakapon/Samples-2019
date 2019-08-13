using System;
using System.Linq;

class B
{
    static void Main()
    {
        var n = int.Parse(Console.ReadLine());
        Console.WriteLine(Enumerable.Range(1, n).Count(i => i.ToString().Length % 2 == 1));
    }
}
