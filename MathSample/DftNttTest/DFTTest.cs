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
			var c = Enumerable.Range(3, n).Select(v => (long)v).ToArray();

			// f^ は整数になるとは限りません。
			var r = DFT.Transform(n, c.ToComplex(), false);
			r = DFT.Transform(n, r, true);
			CollectionAssert.AreEqual(c, r.ToLong());
		}
	}
}
