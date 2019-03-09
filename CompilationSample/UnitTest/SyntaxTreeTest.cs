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
        public void Diagnostics()
        {
            var expression = SyntaxFactory.ParseExpression("x+y");
            Assert.AreEqual(0, expression.GetDiagnostics().Count());

            var expressionTree = CSharpSyntaxTree.ParseText("x+y");
            Assert.IsTrue(expressionTree.GetDiagnostics().Any());

            var methodTree = CSharpSyntaxTree.ParseText("void Action_0() => x + y;");
            Assert.AreEqual(0, methodTree.GetDiagnostics().Count());

            var methodComp = CSharpCompilation.Create("CompilationSample", new[] { methodTree }, new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) }, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            Assert.IsTrue(methodComp.GetDiagnostics().Any());

            var source = @"
public static class Numerics
{
public static double Add(int x, double y) => x + y;
}";
            var tree = CSharpSyntaxTree.ParseText(source);
            var compilation = CSharpCompilation.Create("CompilationSample", new[] { tree }, new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) }, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            Assert.AreEqual(0, tree.GetDiagnostics().Count());
        }

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
            var compilation = CSharpCompilation.Create("CompilationSample", new[] { tree }, new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) }, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

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

        [TestMethod]
        public void Add_Script()
        {
            var source = "Item1 + Item2";
            var globals = Tuple.Create(1, 1.3);

            var tree = SyntaxFactory.ParseSyntaxTree(source, CSharpParseOptions.Default.CommonWithKind(SourceCodeKind.Script));
            var compilation = CSharpCompilation.CreateScriptCompilation("CompilationSample", tree, new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) }, globalsType: globals.GetType());

            // コンパイル前にエラーの存在を確認できます。
            var diagnostics = compilation.GetDiagnostics();
            if (!diagnostics.IsEmpty) throw new FormatException();

            using (var memory = new MemoryStream())
            {
                var result = compilation.Emit(memory);
                if (!result.Success) throw new FormatException();
                var assembly = Assembly.Load(memory.ToArray());
            }
        }
    }
}
