using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class SemanticModelTest
    {
        [TestMethod]
        public void Add_1()
        {
            var source = @"
public static class Numerics
{
public static double Add(int x, double y) => x + y;
public static double Mean(double x, double y) => (x + y) / 2;
}";
            var tree = CSharpSyntaxTree.ParseText(source);
            var compilation = CSharpCompilation.Create("CompilationSample", new[] { tree }, new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) }, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            var diagnostics = compilation.GetDiagnostics();
            if (!diagnostics.IsEmpty) throw new FormatException();
            var semantic = compilation.GetSemanticModel(tree);

            var root = tree.GetCompilationUnitRoot();
            var add = root.DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Single(m => m.Identifier.ValueText == "Add");
            // GetTypeInfo で式の型を取得できます。
            // ただし、型情報は GetSymbolInfo でも取得できます。
            var symbol = (IMethodSymbol)semantic.GetSymbolInfo(add.ExpressionBody.Expression).Symbol;

            // このシンボルは op_Addition(double, double) を表します。
            Assert.AreEqual(typeof(double), ToType(symbol.Parameters[0]));
            Assert.AreEqual(typeof(double), ToType(symbol.Parameters[1]));
        }

        [TestMethod]
        public void Add_Script()
        {
            var source = "Item1 + Item2";
            var globals = Tuple.Create(1, 1.3);

            var tree = SyntaxFactory.ParseSyntaxTree(source, CSharpParseOptions.Default.CommonWithKind(SourceCodeKind.Script));
            var compilation = CSharpCompilation.CreateScriptCompilation("CompilationSample", tree, new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) }, globalsType: globals.GetType());

            var diagnostics = compilation.GetDiagnostics();
            if (!diagnostics.IsEmpty) throw new FormatException();
            var semantic = compilation.GetSemanticModel(tree);

            var root = tree.GetCompilationUnitRoot();
            var expression = root.DescendantNodes().First(s => s is ExpressionSyntax);
            var symbol = (IMethodSymbol)semantic.GetSymbolInfo(expression).Symbol;

            // このシンボルは op_Addition(double, double) を表します。
            Assert.AreEqual(typeof(double), ToType(symbol.Parameters[0]));
            Assert.AreEqual(typeof(double), ToType(symbol.Parameters[1]));
        }

        static Type ToType(IParameterSymbol symbol)
        {
            var name = symbol.Type.SpecialType.ToString().Replace('_', '.');
            return Type.GetType(name);
        }
    }
}
