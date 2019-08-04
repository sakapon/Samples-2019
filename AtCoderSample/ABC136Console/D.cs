using System;
using System.Linq;

class D
{
    static void Main()
    {
        var s = Console.ReadLine();
        var a = s.Select((c, i) => s[i + (c == 'L' ? -1 : 1)] == c ? 0 : 1).ToArray();

        for (var i = 0; i < a.Length; i++)
        {
            if (a[i] != 0) continue;

            if (s[i] == 'L')
            {
                for (int j = i - 2; j >= 0; j -= 2)
                {
                    if (a[j] != 0)
                    {
                        a[j]++;
                        break;
                    }
                }
            }
            else
            {
                for (var j = i + 2; j < a.Length; j += 2)
                {
                    if (a[j] != 0)
                    {
                        a[j]++;
                        break;
                    }
                }
            }
        }
        Console.WriteLine(string.Join(" ", a));
    }
}
