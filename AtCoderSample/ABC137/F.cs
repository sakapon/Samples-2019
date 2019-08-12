using System;
using System.Linq;

class F
{
	static void Main()
	{
		var p = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		var c = Enumerable.Repeat(p - 1, p).ToArray();
		var b = new int[p];
		b[0] = a[0];
		for (var i = p - 1; i > 0; i--)
		{
			b[i] = Enumerable.Range(0, p).Sum(j => c[j] * a[j]) % p;
			for (var j = 0; j < p; j++) c[j] = j * c[j] % p;
		}
		Console.WriteLine(string.Join(" ", b));
	}
}
