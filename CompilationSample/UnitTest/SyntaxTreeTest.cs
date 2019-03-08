using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class SyntaxTreeTest
    {
        [TestMethod]
        public void Add_1()
        {
            var source = @"
public static class Numerics
{
public static double Add(int x, double y) => x + y;
}";
            var assembly = Compile(source);
            var add = assembly.GetType("Numerics").GetMethod("Add");
            Assert.AreEqual(2.3, add.Invoke(null, new object[] { 1, 1.3 }));
        }

        public static Assembly Compile(string source)
        {
            var tree = CSharpSyntaxTree.ParseText(source);
            var compilation = CSharpCompilation.Create("CompilationSample", new[] { tree }, new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) });
            compilation = compilation.WithOptions(compilation.Options.WithOutputKind(OutputKind.DynamicallyLinkedLibrary));

            // コンパイル前にエラーの存在を確認できます。
            //var diagnostics = compilation.GetDiagnostics();
            //if (!diagnostics.IsEmpty) throw new FormatException();

            using (var memory = new MemoryStream())
            {
                var result = compilation.Emit(memory);
                if (!result.Success) throw new FormatException();
                return Assembly.Load(memory.ToArray());
            }
        }
    }
}
