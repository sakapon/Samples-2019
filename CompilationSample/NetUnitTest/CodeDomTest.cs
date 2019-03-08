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
            var provider = CodeDomProvider.CreateProvider("CSharp");
            var results = provider.CompileAssemblyFromSource(new CompilerParameters { GenerateInMemory = true }, source);

            var add = results.CompiledAssembly.ExportedTypes.First().GetMethod("Add");
            Assert.AreEqual(2.3, add.Invoke(null, new object[] { 1, 1.3 }));
        }
    }
}
