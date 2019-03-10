using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static System.Linq.Expressions.Expression;

namespace UnitTest.SyntaxLambda
{
    public static class SyntaxLambdaHelper
    {
        public static void ExecuteExpression(string expression, Dictionary<string, object> variables)
        {
            var assignment = (AssignmentExpressionSyntax)SyntaxFactory.ParseExpression(expression);

            var left = (IdentifierNameSyntax)assignment.Left;
            var leftVarName = left.Identifier.ValueText;

            var context = assignment.Right.ToModel(variables);

            var body = context.RootExpression.ToExpression(context.MetaVariables);
            var lambda = Lambda(body, context.MetaVariables.Values);
            Console.WriteLine(lambda);
            var func = lambda.Compile();
            variables[leftVarName] = func.DynamicInvoke(context.MetaVariables.Keys.Select(n => variables[n]).ToArray());
        }

        static SyntaxContext ToModel(this ExpressionSyntax sourceExpression, Dictionary<string, object> variables)
        {
            var metaVars = sourceExpression.DescendantNodesAndSelf()
                .OfType<IdentifierNameSyntax>()
                .Select(n => n.Identifier.ValueText)
                .Distinct()
                .ToDictionary(n => n, n => Variable(variables[n].GetType(), n));

            var script_vars = string.Join("\r\n", metaVars.Values.Select(v => $"var {v.Name} = default({v.Type});"));
            var script = $"{script_vars}\r\n{sourceExpression}";
            var tree = SyntaxFactory.ParseSyntaxTree(script, CSharpParseOptions.Default.CommonWithKind(SourceCodeKind.Script));
            var compilation = CSharpCompilation.CreateScriptCompilation("CompilationSample", tree, new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) });
            var semanticModel = compilation.GetSemanticModel(tree);

            var root = tree.GetCompilationUnitRoot();
            var rootExpression = root.Members.Last().DescendantNodes().OfType<ExpressionSyntax>().First();

            return new SyntaxContext
            {
                RootExpression = rootExpression,
                SemanticModel = semanticModel,
                MetaVariables = metaVars,
            };
        }

        public static Expression ToExpression(this ExpressionSyntax syntax, Dictionary<string, ParameterExpression> vars)
        {
            var kind = syntax.Kind();

            switch (syntax)
            {
                case BinaryExpressionSyntax s:
                    var typeName = kind.ToString().Replace("Expression", "");
                    if (!Enum.TryParse<ExpressionType>(typeName, out var type)) throw new NotSupportedException();
                    return MakeBinary(type, s.Left.ToExpression(vars), s.Right.ToExpression(vars));
                case CastExpressionSyntax s:
                    return Convert(s.Expression.ToExpression(vars), s.Type.ToType());
                case IdentifierNameSyntax s:
                    return vars[s.Identifier.ValueText];
                case LiteralExpressionSyntax s:
                    return Constant(s.Token.Value);
                case ParenthesizedExpressionSyntax s:
                    return s.Expression.ToExpression(vars);
                default:
                    throw new NotSupportedException();
            }
        }

        static Type ToType(this TypeSyntax syntax)
        {
            switch (syntax)
            {
                case PredefinedTypeSyntax s:
                    switch (s.Keyword.Kind())
                    {
                        case SyntaxKind.DoubleKeyword: return typeof(double);
                        default: throw new NotSupportedException();
                    }
                default:
                    throw new NotSupportedException();
            }
        }
    }

    public class SyntaxContext
    {
        public ExpressionSyntax RootExpression { get; set; }
        public SemanticModel SemanticModel { get; set; }
        public Dictionary<string, ParameterExpression> MetaVariables { get; set; }

        public ISymbol GetSymbol(ExpressionSyntax syntax)
        {
            return SemanticModel.GetSymbolInfo(syntax).Symbol;
        }
    }
}
