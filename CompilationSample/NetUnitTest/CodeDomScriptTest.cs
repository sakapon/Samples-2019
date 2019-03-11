using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetUnitTest
{
    [TestClass]
    public class CodeDomScriptTest
    {
        [TestMethod]
        public void Add_1()
        {
            var expression = "z = x + y";
            var vars = new Dictionary<string, object>
            {
                { "x", 1.0 },
                { "y", 1.3 },
            };
            CodeDomScript.ExecuteExpression(expression, vars);
            Assert.AreEqual(2.3, vars["z"]);
        }

        [TestMethod]
        public void Add_2()
        {
            var expression = "z = (double)x + y";
            var vars = new Dictionary<string, object>
            {
                { "x", 1 },
                { "y", 1.3 },
            };
            CodeDomScript.ExecuteExpression(expression, vars);
            Assert.AreEqual(2.3, vars["z"]);
        }

        [TestMethod]
        public void Add_3()
        {
            var expression = "z += x + y";
            var vars = new Dictionary<string, object>
            {
                { "z", 3.1 },
                { "x", 1 },
                { "y", 1.3 },
            };
            CodeDomScript.ExecuteExpression(expression, vars);
            Assert.AreEqual(5.4, vars["z"]);
        }

        [TestMethod]
        public void Multiply_1()
        {
            var expression = "x*=7";
            var vars = new Dictionary<string, object>
            {
                { "x", 1.3 },
                { "y", 1 },
            };
            CodeDomScript.ExecuteExpression(expression, vars);
            Assert.AreEqual(9.1, vars["x"]);
        }

        [TestMethod]
        public void Mean_1()
        {
            var expression = "z = (x + y) / 2";
            var vars = new Dictionary<string, object>
            {
                { "x", 1.0 },
                { "y", 1.3 },
            };
            CodeDomScript.ExecuteExpression(expression, vars);
            Assert.AreEqual(1.15, vars["z"]);
        }

        [TestMethod]
        public void Mean_2()
        {
            var expression = "z = (x + y) / 2.0";
            var vars = new Dictionary<string, object>
            {
                { "x", 1 },
                { "y", 2 },
            };
            CodeDomScript.ExecuteExpression(expression, vars);
            Assert.AreEqual(1.5, vars["z"]);
        }

        [TestMethod]
        public void Sqrt_1()
        {
            var expression = "z = System.Math.Sqrt(10)";
            var vars = new Dictionary<string, object>
            {
                { "x", 1 },
            };
            CodeDomScript.ExecuteExpression(expression, vars);
            Assert.AreEqual(Math.Sqrt(10), vars["z"]);
        }
    }
}
