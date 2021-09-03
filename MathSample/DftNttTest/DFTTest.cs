using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DftNttTest
{
	[TestClass]
	public class DFTTest
	{
		[TestMethod]
		public void Transform()
		{
			var n = 123;
			var f1 = Enumerable.Range(3, n).Select(v => (long)v).ToArray();

			// f^ は整数になるとは限りません。
			var f_ = DFT.Transform(n, f1.ToComplex(), false);
			var f2 = DFT.Transform(n, f_, true);
			CollectionAssert.AreEqual(f1, f2.ToLong());

			var n2 = 127;
			f_ = DFT.Transform(n2, f1.ToComplex(), false);
			f2 = DFT.Transform(n2, f_, true);
			Array.Resize(ref f2, n);
			CollectionAssert.AreEqual(f1, f2.ToLong());
		}
	}
}
