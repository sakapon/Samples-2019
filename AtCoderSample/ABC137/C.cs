using System;
using System.Linq;

class C
{
	static void Main()
	{
		Console.ReadLine();
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
	}
}
