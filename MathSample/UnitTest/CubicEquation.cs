using System;

namespace UnitTest
{
    public static class CubicEquation
    {
        public static bool IsMicro(this double x) => Math.Round(x, 12) == 0.0;
        public static double MicroToZero(this double x) => IsMicro(x) ? 0.0 : x;

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
            return x.MicroToZero();
        }
    }
}
