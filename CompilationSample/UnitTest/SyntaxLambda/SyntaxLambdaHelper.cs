using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static System.Linq.Expressions.Expression;

namespace UnitTest.SyntaxLambda
{
    public static class SyntaxLambdaHelper
    {
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
