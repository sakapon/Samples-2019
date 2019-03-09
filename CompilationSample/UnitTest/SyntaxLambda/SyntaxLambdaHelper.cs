using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static System.Linq.Expressions.Expression;

namespace UnitTest.SyntaxLambda
{
    public static class SyntaxLambdaHelper
    {
        public static void ExecuteExpression(string expression, Dictionary<string, object> vars)
        {
            var assignment = GetAssignment(expression);

            var left = (IdentifierNameSyntax)assignment.Left;
            var leftVarName = left.Identifier.ValueText;

            var rightVars = assignment.Right.DescendantNodesAndSelf()
                .OfType<IdentifierNameSyntax>()
                .Select(n => n.Identifier.ValueText)
                .Distinct()
                .ToDictionary(n => n, n => Variable(vars[n].GetType(), n));

            var body = assignment.Right.ToExpression(rightVars);
            var lambda = Lambda(body, rightVars.Values);
            Console.WriteLine(lambda);
            var func = lambda.Compile();
            vars[leftVarName] = func.DynamicInvoke(rightVars.Keys.Select(n => vars[n]).ToArray());
        }

        public static AssignmentExpressionSyntax GetAssignment(string expression)
        {
            var method = (MethodDeclarationSyntax)ParseMember($"void Action_0() => {expression};");
            return (AssignmentExpressionSyntax)method.ExpressionBody.Expression;
        }

        public static MemberDeclarationSyntax ParseMember(string text)
        {
            var tree = CSharpSyntaxTree.ParseText(text);
            var diagnostics = tree.GetDiagnostics().ToArray();
            if (diagnostics.Length > 0) return null;

            var root = tree.GetCompilationUnitRoot();
            return root.Members[0];
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
}
