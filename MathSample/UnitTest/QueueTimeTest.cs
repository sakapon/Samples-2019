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

        [TestMethod]
        public void QueueTime_1()
        {
            var count = 200000;
            var waitings = GetWaitingTimes(1, 2, count)
                .Where(x => x != 0)
                .OrderBy(x => x)
                .ToArray();

            Console.WriteLine($"Mean of Waitings (non-zero): {waitings.Average()}");
            Console.WriteLine();
            Console.WriteLine($"No Waitings: {count - waitings.Length}");
            waitings
                .GroupBySequentially(x => (int)x)
                .Execute(g => Console.WriteLine($"{g.Key}: {g.Count()}"));
            Console.WriteLine();
            waitings
                .GroupBySequentially(x => Math.Floor(x * 10) / 10)
                .Execute(g => Console.WriteLine($"{g.Key:F1}: {g.Count()}"));
        }

        [TestMethod]
        public void QueueTime_2()
        {
            GetWaitingTimes(0.5, 1, 100000);
            GetWaitingTimes(5, 10, 100000);
            GetWaitingTimes(1, 3, 100000);
            GetWaitingTimes(0.8, 1, 100000);
            GetWaitingTimes(1.1, 1, 100000);
        }

        // λ: 到着率, μ: サービス率
        static double[] GetWaitingTimes(double λ, double μ, int count)
        {
            var ρ = λ / μ;
            Console.WriteLine($"λ = {λ}, μ = {μ}, ρ = {ρ}");
            Console.WriteLine($"ρ / (1-ρ)μ = {ρ / (1 - ρ) / μ}");

            var intervals = GetExponentialDistribution(1 / λ, count);
            var services = GetExponentialDistribution(1 / μ, count);
            Console.WriteLine($"Mean of Intervals: {intervals.Average()}");
            Console.WriteLine($"Mean of Services: {services.Average()}");

            var people = new (double arrival, double start)[count];
            people[0] = (intervals[0], intervals[0]);
            for (var i = 1; i < count; i++)
            {
                var arrival = people[i - 1].arrival + intervals[i];
                var start = Math.Max(arrival, people[i - 1].start + services[i - 1]);
                people[i] = (arrival, start);
            }

            var waitings = people
                .Select(_ => _.start - _.arrival)
                .ToArray();
            Console.WriteLine($"Mean of Waitings: {waitings.Average()}");
            Console.WriteLine();
            return waitings;
        }

        static double[] GetExponentialDistribution(double mean, int count)
        {
            return Enumerable.Range(0, count)
                .Select(i => -mean * Math.Log(1 - random.NextDouble()))
                .ToArray();
        }
    }
}
