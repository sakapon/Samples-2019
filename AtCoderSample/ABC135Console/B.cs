using System;
using System.Linq;

class B
{
	static void Main()
	{
		Console.ReadLine();
		var a = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
		var d = a.Where((pi, i) => pi != i + 1).Count();
		Console.WriteLine(d == 0 || d == 2 ? "YES" : "NO");
	}
}
