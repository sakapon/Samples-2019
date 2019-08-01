using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class EquationsTest
    {
        static void AssertNearlyEqual(double expected, double actual) =>
            Assert.AreEqual(0.0, Math.Round(expected - actual, 12));

        [TestMethod]
        public void Solve_1()
        {
            Test(0, 1, 1);
            Test(1, 0, 1);
            Test(1, -1, 0);

            void Test(double b, double c, double d)
            {
                var f = CubicEquation.CreateFunction(b, c, d);
                foreach (var x in CubicEquation.Solve(b, c, d))
                    AssertNearlyEqual(0, f(x));
            }
        }
    }

    public static class CubicEquation
    {
        public static Func<double, double> CreateFunction(double b, double c, double d) =>
            x => x * x * x + b * x * x + c * x + d;
        public static Func<double, double> CreateDerivative(double b, double c, double d) =>
            x => 3 * x * x + 2 * b * x + c;

        // f(x) = ax^3 + bx^2 + cx + d = 0
        public static double[] Solve(double a, double b, double c, double d) =>
            a != 0 ? Solve(b / a, c / a, d / a) : throw new ArgumentException("The value must not be 0.", nameof(a));

        // f  (x) = x^3 + bx^2 + cx + d = 0
        // f' (x) = 3x^2 + 2bx + c
        // f''(x) = 6x + 2b
        public static double[] Solve(double b, double c, double d)
        {
            var f = CreateFunction(b, c, d);
            var f1 = CreateDerivative(b, c, d);

            var x_center = -b / 3;
            var y_center = f(x_center);
            var x0_sign = y_center <= 0 ? 1 : -1;
            var det_2 = b * b - 3 * c;

            var x0 = (det_2 <= 0 ? x_center : (-b + x0_sign * Math.Sqrt(det_2)) / 3) + x0_sign;
            var x1 = SolveByNewtonMethod(f, f1, x0);

            // f(x) = (x - x_1) (x^2 + px + q)
            var p = x1 + b;
            var q = x1 * p + c;
            var det = p * p - 4 * q;

            return det > 0 ? new[] { x1, (-p - Math.Sqrt(det)) / 2, (-p + Math.Sqrt(det)) / 2 } :
                det == 0 ? new[] { x1, -p / 2 } :
                new[] { x1 };
        }

        static double SolveByNewtonMethod(Func<double, double> f, Func<double, double> f1, double x0)
        {
            var r = x0;
            for (var i = 0; i < 100; i++)
            {
                var temp = r - f(r) / f1(r);
                if (r == temp) break;
                r = temp;
            }
            return r;
        }
    }
}
