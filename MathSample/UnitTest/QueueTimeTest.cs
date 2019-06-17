using System;
using System.Collections.Generic;
using System.Linq;
using KLibrary.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class QueueTimeTest
    {
        static readonly Random random = new Random();

        /// <summary>
        /// ポアソン分布と指数分布の関連性。
        /// </summary>
        [TestMethod]
        public void Distributions()
        {
            var count = 100000;

            var arrivals = Enumerable.Range(0, count)
                .Select(i => count * random.NextDouble())
                .OrderBy(x => x)
                .ToArray();
            var intervals = arrivals
                .Between((x, y) => y - x)
                .OrderBy(x => x)
                .ToArray();
            Console.WriteLine($"Mean of Intervals: {intervals.Average()}");

            // Poisson distribution
            Console.WriteLine();
            arrivals
                .GroupBySequentially(x => (int)(x / 10))
                .GroupBy(g => g.Count())
                .OrderBy(g => g.Key)
                .Execute(g => Console.WriteLine($"{g.Key}: {g.Count()}"));

            // exponential distribution
            Console.WriteLine();
            intervals
                .GroupBySequentially(x => (int)x)
                .Execute(g => Console.WriteLine($"{g.Key}: {g.Count()}"));
            Console.WriteLine();
            intervals
                .GroupBySequentially(x => Math.Floor(x * 10) / 10)
                .Execute(g => Console.WriteLine($"{g.Key:F1}: {g.Count()}"));
        }
    }
}
