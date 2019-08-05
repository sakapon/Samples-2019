using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static void Main()
	{
		var k = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
		var q = new Vector(a[0], a[1]);

		if (k % 2 == 0 && q.Norm % 2 == 1)
		{
			Console.WriteLine(-1);
			return;
		}
		var ps = new List<Vector> { new Vector() };
		var l = q.Norm % k == 0 ? 2 : k % 2 == 1 && q.Norm % 2 == 1 && q.Norm < k ? 4 : 3;
		if (l == 4) ps.Add(q.GetShotFor4(k));
		if (l >= 3) ps.Add(ps.Last() + (q - ps.Last()).GetShotFor3(k));
		while ((q - ps.Last()).Norm != 0) ps.Add(ps.Last() + (q - ps.Last()).GetShotFor2(k));
		Console.WriteLine(ps.Count - 1);
		foreach (var p in ps.Skip(1)) Console.WriteLine("{0} {1}", p.X, p.Y);
	}
}

struct Vector
{
	public int X;
	public int Y;
	public int Norm => Math.Abs(X) + Math.Abs(Y);
	bool IsXLong => Math.Abs(X) >= Math.Abs(Y);
	public Vector(int x, int y) { X = x; Y = y; }
	public static Vector operator +(Vector v, Vector w) => new Vector(v.X + w.X, v.Y + w.Y);
	public static Vector operator -(Vector v, Vector w) => new Vector(v.X - w.X, v.Y - w.Y);
	public Vector GetShotFor4(int k) => IsXLong ? new Vector(-Math.Sign(X) * (k - Norm), (Y > 0 ? -1 : 1) * Norm) : new Vector((X > 0 ? -1 : 1) * Norm, -Math.Sign(Y) * (k - Norm));
	public Vector GetShotFor3(int k)
	{
		var w2 = (Norm / k + 1) * k - Norm;
		if (w2 % 2 == 1) w2 += k;
		var w = Norm < k ? k - Norm / 2 : w2 / 2;
		return IsXLong ? new Vector(Math.Sign(X) * (k - w), (Y > 0 ? -1 : 1) * w) : new Vector((X > 0 ? -1 : 1) * w, Math.Sign(Y) * (k - w));
	}
	public Vector GetShotFor2(int k)
	{
		var dx = Math.Min(Math.Abs(X), k);
		return new Vector(Math.Sign(X) * dx, Math.Sign(Y) * (k - dx));
	}
}
