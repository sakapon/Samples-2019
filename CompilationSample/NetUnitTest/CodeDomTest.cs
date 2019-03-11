using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetUnitTest
{
    [TestClass]
    public class CodeDomTest
    {
        [TestMethod]
        public void Add_1()
        {
            var source = @"
public static class Numerics
{
public static double Add(int x, double y) { return x + y; }
}";
            var options = new CompilerParameters()
            {
                GenerateInMemory = true,
            };
            var provider = CodeDomProvider.CreateProvider("CSharp");
            var results = provider.CompileAssemblyFromSource(options, source);
            if (results.Errors.HasErrors) throw new FormatException();

            var add = results.CompiledAssembly.GetType("Numerics").GetMethod("Add");
            Assert.AreEqual(2.3, add.Invoke(null, new object[] { 1, 1.3 }));
        }

        [TestMethod]
        public void Add_2()
        {
            var expression = "z += x + y";
            var vars = new Dictionary<string, object>
            {
                { "x", 1 },
                { "y", 1.3 },
                { "z", 3.1 },
            };

            var source = $@"
public static class Numerics
{{
public static void Add(ref int x, ref double y, ref double z)
{{
{expression};
}}
}}";
            // この場合は参照なしでもコンパイルできます。
            var options = new CompilerParameters(new[] { "System.dll", "System.Core.dll" })
            {
                GenerateInMemory = true,
            };
            var provider = CodeDomProvider.CreateProvider("CSharp");
            var results = provider.CompileAssemblyFromSource(options, source);
            if (results.Errors.HasErrors) throw new FormatException();

            var add = results.CompiledAssembly.GetType("Numerics").GetMethod("Add");
            var args = vars.Values.ToArray();
            add.Invoke(null, args);
            Assert.AreEqual(5.4, args[2]);
            add.Invoke(null, args);
            Assert.AreEqual(7.7, args[2]);
        }

        [TestMethod]
        public void Add_3()
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
    }
}
