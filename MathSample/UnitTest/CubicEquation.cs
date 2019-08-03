using System;
using System.Linq;

namespace UnitTest
{
    public static class CubicEquation
    {
        public static bool IsNearlyInteger(this double x) => Math.Round(x, 12) == Math.Round(x);
        public static double RoundNearlyInteger(this double x) => IsNearlyInteger(x) ? Math.Round(x) : x;

        public static Func<double, double> CreateFunction(double b, double c, double d) =>
            x => x * x * x + b * x * x + c * x + d;
        public static Func<double, double> CreateDerivative(double b, double c, double d) =>
            x => 3 * x * x + 2 * b * x + c;

        /// <summary>
        /// 方程式 f(x) = 0 を満たす近似解を Newton 法により求めます。
        /// </summary>
        /// <param name="f">元の関数。</param>
        /// <param name="f1">導関数。</param>
        /// <param name="x0">初期値。</param>
        /// <returns>方程式 f(x) = 0 の近似解。</returns>
        public static double SolveByNewtonMethod(Func<double, double> f, Func<double, double> f1, double x0)
        {
            var x = x0;
            for (var i = 0; i < 100; i++)
            {
                var temp = x - f(x) / f1(x);
                if (x == temp) break;
                x = temp;
            }
            return x.RoundNearlyInteger();
        }

        // f(x) = ax^3 + bx^2 + cx + d = 0
        public static double[] Solve(double a, double b, double c, double d) =>
            a != 0 ? Solve(b / a, c / a, d / a) : throw new ArgumentException("The value must not be 0.", nameof(a));

        // f(x) = x^3 + bx^2 + cx + d = 0
        public static double[] Solve(double b, double c, double d) =>
            Solve((c - b * b / 3).RoundNearlyInteger(), (d - b * c / 3 + 2 * b * b * b / 27).RoundNearlyInteger())
                .Select(x => (x - b / 3).RoundNearlyInteger())
                .ToArray();

        // f  (x) = x^3 + cx + d = 0
        // f' (x) = 3x^2 + c
        // f''(x) = 6x
        public static double[] Solve(double c, double d)
        {
            // Center: (0, d)
            var f = CreateFunction(0, c, d);
            var f1 = CreateDerivative(0, c, d);

            if (c > 0)
            {
                return new[] { SolveByNewtonMethod(f, f1, -Math.Sign(d)) };
            }
            else if (c == 0)
            {
                // 3重解
                if (d == 0) return new[] { 0.0 };
                return new[] { SolveByNewtonMethod(f, f1, -Math.Sign(d)) };
            }
            else
            {
                var m_x = Math.Sqrt(-c / 3).RoundNearlyInteger();
                var M = (x: -m_x, y: f(-m_x).RoundNearlyInteger());
                var m = (x: m_x, y: f(m_x).RoundNearlyInteger());

                if (M.y < 0) return new[] { SolveByNewtonMethod(f, f1, m.x + 1) };
                if (m.y > 0) return new[] { SolveByNewtonMethod(f, f1, M.x - 1) };
                // 重解
                if (M.y == 0) return new[] { M.x, -2 * M.x };
                if (m.y == 0) return new[] { -2 * m.x, m.x };

                throw new NotImplementedException();
            }
        }
    }
}
