using System;
using System.Linq;

class C
{
	static void Main() => Console.WriteLine(Enumerable.Range(0, int.Parse(Console.ReadLine())).Select(i => new string(Console.ReadLine().OrderBy(s => s).ToArray())).GroupBy(s => s).Select(g => g.Count()).Where(c => c > 1).Sum(c => (long)c * (c - 1) / 2));
}
