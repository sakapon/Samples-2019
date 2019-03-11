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
    }
}
