using System;
using System.Linq;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

		var b = new int[n];
		var m = n / 2;
		for (var i = m; i < n; i++)
			b[i] = a[i];
		for (var i = m - 1; i >= 0; i--)
			b[i] = (a[i] + Enumerable.Range(2, n / (i + 1) - 1).Sum(j => b[j * (i + 1) - 1])) % 2;

		var c = b.Select((x, i) => new { x, i }).Where(_ => _.x == 1).Select(_ => _.i + 1).ToArray();
		Console.WriteLine(c.Length);
		Console.WriteLine(string.Join(" ", c));
	}
}
