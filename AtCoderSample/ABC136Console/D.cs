using System;
using System.Linq;

class D
{
    static void Main()
    {
        var s = Console.ReadLine();
        var b = s.Select((c, i) => s[i + (c == 'L' ? -1 : 1)] == c).ToArray();
        var a = Enumerable.Repeat(1, s.Length).ToArray();

        for (var i = 0; i < s.Length; i++)
        {
            if (s[i] == 'R' && b[i])
            {
                a[i + 2] += a[i];
                a[i] = 0;
            }
        }
        for (var i = s.Length - 1; i >= 0; i--)
        {
            if (s[i] == 'L' && b[i])
            {
                a[i - 2] += a[i];
                a[i] = 0;
            }
        }
        Console.WriteLine(string.Join(" ", a));
    }
}
