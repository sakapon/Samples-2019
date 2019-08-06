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
			var d = a[i] - a[i + 1];
			if (d > 1)
			{
				Console.WriteLine("No");
				return;
			}
			if (d == 1) a[i]--;
		}
		Console.WriteLine("Yes");
	}
}
