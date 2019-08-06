using System;
using System.Linq;

class D
{
	static void Main()
	{
		var r = Enumerable.Range(0, 13);
		var d = Enumerable.Range(0, 10).Select(i =>
		{
			var m = new int[13, 13];
			foreach (var j in r) m[(10 * j + i) % 13, j] = 1;
			return new { i, m };
		})
		.ToDictionary(_ => _.i.ToString()[0], _ => _.m);

		var m_ = new int[13, 13];
		foreach (var p in r.SelectMany(i => r.Select(j => new { i, j }))) m_[p.i, p.j] = d.Values.Sum(m => m[p.i, p.j]);
		d['?'] = m_;

		var x = new long[13];
		x[0] = 1;
		x = Console.ReadLine().Aggregate(x, (v, c) => r.Select(i => r.Sum(j => d[c][i, j] * v[j]) % 1000000007).ToArray());
		Console.WriteLine(x[5]);
	}
}
