using System;
using System.Linq;

class C
{
    static void Main()
    {
        Console.ReadLine();
        var a = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

        for (var i = a.Length - 2; i >= 0; i--)
        {
            if (a[i] > a[i + 1] + 1)
            {
                Console.WriteLine("No");
                return;
            }
            else if (a[i] == a[i + 1] + 1)
            {
                a[i]--;
            }
        }
        Console.WriteLine("Yes");
    }
}
