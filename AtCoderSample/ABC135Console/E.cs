using System.Collections.Generic;
using System.Linq;
using static System.Console;
using static System.Math;

class E
{
	static void Main()
	{
		var k = int.Parse(ReadLine());
		var a = ReadLine().Split(' ').Select(int.Parse).ToArray();
		var q = new V(a[0], a[1]);

		if (k % 2 == 0 && q.Norm % 2 == 1)
		{
			WriteLine(-1);
			return;
		}
		var ps = new List<V> { new V() };
		var l = q.Norm % k == 0 ? 2 : q.Norm % 2 == 1 && q.Norm < k ? 4 : 3;
		if (l == 4) ps.Add(q.NextShot4(k));
		if (l >= 3) ps.Add(ps.Last() + (q - ps.Last()).NextShot3(k));
		while ((q - ps.Last()).Norm != 0) ps.Add(ps.Last() + (q - ps.Last()).NextShot2(k));
		WriteLine(ps.Count - 1);
		foreach (var p in ps.Skip(1)) WriteLine("{0} {1}", p.X, p.Y);
	}
}

struct V
{
	public int X;
	public int Y;
	public int Norm => Abs(X) + Abs(Y);

	public V(int x, int y) { X = x; Y = y; }
	public static V operator +(V v, V w) => new V(v.X + w.X, v.Y + w.Y);
	public static V operator -(V v, V w) => new V(v.X - w.X, v.Y - w.Y);

	public V NextShot4(int k) => Abs(X) >= Abs(Y) ? new V(-Sign(X) * (k - Norm), (Y > 0 ? -1 : 1) * Norm) : new V((X > 0 ? -1 : 1) * Norm, -Sign(Y) * (k - Norm));
	public V NextShot3(int k)
	{
		var w2 = k - Norm % k;
		if (w2 % 2 == 1) w2 += k;
		var w = Norm < k ? k - Norm / 2 : w2 / 2;
		return Abs(X) >= Abs(Y) ? new V(Sign(X) * (k - w), (Y > 0 ? -1 : 1) * w) : new V((X > 0 ? -1 : 1) * w, Sign(Y) * (k - w));
	}
	public V NextShot2(int k)
	{
		var d = Min(Abs(X), k);
		return new V(Sign(X) * d, Sign(Y) * (k - d));
	}
}
