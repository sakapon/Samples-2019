using System;
using System.Linq;

class B
{
	static void Main()
	{
		Console.ReadLine();
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
	}
}
