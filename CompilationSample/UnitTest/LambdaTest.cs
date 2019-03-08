using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Linq.Expressions.Expression;

namespace UnitTest
{
    [TestClass]
    public class LambdaTest
    {
        [TestMethod]
        public void Sqrt_1()
        {
            Expression<Func<double, double, double>> Sqrt0Lambda = (x, a) => (x + a / x) / 2;
            var Sqrt0 = Sqrt0Lambda.Compile();
            Expression<Func<double, Func<double, double>>> GetSqrtALambda = a => x => Sqrt0(x, a);

            var SqrtA = GetSqrtALambda.Compile()(89.0);
            Console.WriteLine(SqrtA(9.5));
        }

        [TestMethod]
        public void Add_1()
        {
            var x = Variable(typeof(double), "x");
            var y = Variable(typeof(double), "y");

            var l = Lambda(Add(x, y), x, y);
            var add = (Func<double, double, double>)l.Compile();
            Assert.AreEqual(2.3, add(1, 1.3));
        }

        [TestMethod]
        public void Add_2()
        {
            var x = Variable(typeof(int), "x");
            var y = Variable(typeof(double), "y");

            var l = Lambda(Add(Convert(x, typeof(double)), y), x, y);
            var add = (Func<int, double, double>)l.Compile();
            Assert.AreEqual(2.3, add(1, 1.3));
        }

        [TestMethod]
        public void Mean_1()
        {
            var x = Variable(typeof(double), "x");
            var y = Variable(typeof(double), "y");

            var l = Lambda(Divide(Add(x, y), Constant(2.0)), x, y);
            var mean = (Func<double, double, double>)l.Compile();
            Assert.AreEqual(1.15, mean(1, 1.3));
        }

        [TestMethod]
        public void Mean_2()
        {
            var x = Variable(typeof(int), "x");
            var y = Variable(typeof(int), "y");

            var l = Lambda(Divide(Convert(Add(x, y), typeof(double)), Constant(2.0)), x, y);
            var mean = (Func<int, int, double>)l.Compile();
            Assert.AreEqual(1.5, mean(1, 2));
        }
    }
}
