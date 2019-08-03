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
            Test(new[] { 0.0 }, 0, 0, 0);
            Test(new[] { 2.0 }, 0, 0, -8);
            Test(new[] { 0.0 }, 1, 1, 0);
            Test(new[] { -2.0 }, 3, 4, 4);

            Test(new[] { -2.0 }, 1, -1, 2);
            Test(new[] { -3.0, 1.0 }, 1, -5, 3);
            Test(new[] { -3.0, 0.0, 3.0 }, 0, -9, 0);

            void Test(double[] expected, double b, double c, double d)
            {
                var actual = CubicEquation.Solve(b, c, d);
                CollectionAssert.AreEqual(expected, actual);

                var f = CubicEquation.CreateFunction(b, c, d);
                foreach (var x in actual)
                    AssertNearlyEqual(0, f(x));
            }
        }

        [TestMethod]
        public void Solve_2()
        {
            Test(3, 3, 3);
            Test(0, 1, 1);
            Test(1, 0, 1);
            Test(1, -1, 0);
            Test(3, -2, -6);
            Test(0, -15, -4);

            void Test(double b, double c, double d)
            {
                var actual = CubicEquation.Solve(b, c, d);

                var f = CubicEquation.CreateFunction(b, c, d);
                foreach (var x in actual)
                    AssertNearlyEqual(0, f(x));
            }
        }
    }

    public static class CubicEquation0
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

            var center_x = -b / 3;
            var center = (x: center_x, y: f(center_x).RoundNearlyInteger());
            var x0_sign = center.y <= 0 ? 1 : -1;
            var det_2 = (b * b - 3 * c).RoundNearlyInteger();

            if (det_2 > 0)
            {
                var M_x = ((-b - Math.Sqrt(det_2)) / 3).RoundNearlyInteger();
                var m_x = ((-b + Math.Sqrt(det_2)) / 3).RoundNearlyInteger();
                var M = (x: M_x, y: f(M_x).RoundNearlyInteger());
                var m = (x: m_x, y: f(m_x).RoundNearlyInteger());

                // 重解
                if (M.y == 0) return new[] { M.x, -2 * M.x - b };
                if (m.y == 0) return new[] { m.x, -2 * m.x - b };
                if (M.y < 0) return new[] { SolveByNewtonMethod(f, f1, m.x + 1) };
                if (m.y > 0) return new[] { SolveByNewtonMethod(f, f1, M.x - 1) };
            }
            else if (det_2 == 0)
            {
                // 3重解
                if (center.y == 0) return new[] { center.x };
                return new[] { SolveByNewtonMethod(f, f1, center.x + x0_sign) };
            }
            else
            {
                return new[] { SolveByNewtonMethod(f, f1, center.x + x0_sign) };
            }

            var x0 = (-b + x0_sign * Math.Sqrt(det_2)) / 3 + x0_sign;
            var x1 = SolveByNewtonMethod(f, f1, x0);

            // f(x) = (x - x_1) (x^2 + px + q)
            var p = x1 + b;
            var q = x1 * p + c;
            var det = p * p - 4 * q;

            return new[] { x1, ((-p - Math.Sqrt(det)) / 2).RoundNearlyInteger(), ((-p + Math.Sqrt(det)) / 2).RoundNearlyInteger() };
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
            return r.RoundNearlyInteger();
        }
    }
}
