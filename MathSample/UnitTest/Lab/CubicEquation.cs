using System;
using System.Linq;
using static System.Math;

namespace UnitTest.Lab
{
    public static class CubicEquation
    {
        public static bool IsCloseTo(this double x, double y) => Round(x - y, 12) == 0;
        public static bool IsAlmostInteger(this double x) => x.IsCloseTo(Round(x));
        public static double RoundAlmost(this double x)
        {
            var n = Round(x);
            return x.IsCloseTo(n) ? n : x;
        }

        public static Func<double, double> CreateFunction(double b, double c, double d) =>
            x => x * x * x + b * x * x + c * x + d;
        public static Func<double, double> CreateFunction(double c, double d) =>
            x => x * x * x + c * x + d;
        public static Func<double, double> CreateDerivative(double c, double d) =>
            x => 3 * x * x + c;

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
            return x.RoundAlmost();
        }

        // f(x) = ax^3 + bx^2 + cx + d = 0
        public static double[] Solve(double a, double b, double c, double d) =>
            a != 0 ? Solve(b / a, c / a, d / a) : throw new ArgumentException("The value must not be 0.", nameof(a));

        // f(x) = x^3 + bx^2 + cx + d = 0
        public static double[] Solve(double b, double c, double d) =>
            Solve((c - b * b / 3).RoundAlmost(), (d - b * c / 3 + 2 * b * b * b / 27).RoundAlmost())
                .Select(x => (x - b / 3).RoundAlmost())
                .ToArray();

        // f  (x) = x^3 + cx + d = 0
        // f' (x) = 3x^2 + c
        // f''(x) = 6x
        public static double[] Solve(double c, double d)
        {
            // Center: (0, d)
            var f = CreateFunction(c, d);
            var f1 = CreateDerivative(c, d);

            // 実数解を 1 つ求めます。
            var x1 = d == 0 ? 0 : SolveByNewtonMethod(f, f1, -Sign(d) * ((c < 0 ? Sqrt(-c / 3) : 0) + 1));

            // f(x) = (x - x_1) (x^2 + x_1 x + q)
            // q = x_1^2 + c;
            var det = (-3 * x1 * x1 - 4 * c).RoundAlmost();
            if (det < 0) return new[] { x1 };

            var x2 = ((-x1 - Sqrt(det)) / 2).RoundAlmost();
            var x3 = ((-x1 + Sqrt(det)) / 2).RoundAlmost();
            return new[] { x1, x2, x3 }.Distinct().OrderBy(x => x).ToArray();
        }

        public static double[] Solve0(double c, double d)
        {
            // Center: (0, d)
            var f = CreateFunction(c, d);
            var f1 = CreateDerivative(c, d);

            // f(x) が極値を持たない場合
            // 3重解 (c = d = 0) を含む
            if (c >= 0) return new[] { d == 0 ? 0.0 : SolveByNewtonMethod(f, f1, -Sign(d)) };

            // 以下、f(x) が極値を持つ場合
            var m_x = Sqrt(-c / 3).RoundAlmost();
            var M = (x: -m_x, y: f(-m_x).RoundAlmost());
            var m = (x: m_x, y: f(m_x).RoundAlmost());

            if (M.y < 0) return new[] { SolveByNewtonMethod(f, f1, m.x + 1) };
            if (m.y > 0) return new[] { SolveByNewtonMethod(f, f1, M.x - 1) };
            // 重解
            if (M.y == 0) return new[] { M.x, -2 * M.x };
            if (m.y == 0) return new[] { -2 * m.x, m.x };

            // 以下、解を 3 つ持つ場合
            if (d == 0) return new[] { -Sqrt(-c), 0.0, Sqrt(-c) };

            var x0 = -Sign(d) * (m_x + 1);
            var x1 = SolveByNewtonMethod(f, f1, x0);

            // f(x) = (x - x_1) (x^2 + x_1 x + q)
            // q = x_1^2 + c;
            var sqrt_det = Sqrt(-3 * x1 * x1 - 4 * c);
            var x2 = ((-x1 - sqrt_det) / 2).RoundAlmost();
            var x3 = ((-x1 + sqrt_det) / 2).RoundAlmost();
            return x1 < 0 ? new[] { x1, x2, x3 } : new[] { x2, x3, x1 };
        }

        public static double[] Solve2(double c, double d)
        {
            // Center: (0, d)
            var f = CreateFunction(c, d);
            var f1 = CreateDerivative(c, d);

            // 3重解 (c = d = 0) を含む
            if (d == 0) return c >= 0 ? new[] { 0.0 } : new[] { -Sqrt(-c).RoundAlmost(), 0.0, Sqrt(-c).RoundAlmost() };

            // f(x) が極値を持たない場合
            if (c >= 0) return new[] { SolveByNewtonMethod(f, f1, -Sign(d)) };

            // 以下、f(x) が極値を持つ場合
            var m_x = Sqrt(-c / 3).RoundAlmost();
            var det_cd = (-4 * c * c * c - 27 * d * d).RoundAlmost();
            if (det_cd < 0) return new[] { SolveByNewtonMethod(f, f1, -Sign(d) * (m_x + 1)) };
            if (det_cd == 0) return d > 0 ? new[] { -2 * m_x, m_x } : new[] { -m_x, 2 * m_x };

            // x1 を最も外側の解とすると、x1 が重解となることはない
            // f(x) = (x - x_1) (x^2 + x_1 x + q)
            // q = x_1^2 + c;
            var x1 = SolveByNewtonMethod(f, f1, -Sign(d) * (m_x + 1));
            var sqrt_det = Sqrt(-3 * x1 * x1 - 4 * c);
            var x2 = ((-x1 - sqrt_det) / 2).RoundAlmost();
            var x3 = ((-x1 + sqrt_det) / 2).RoundAlmost();
            return x1 < 0 ? new[] { x1, x2, x3 } : new[] { x2, x3, x1 };
        }
    }
}
