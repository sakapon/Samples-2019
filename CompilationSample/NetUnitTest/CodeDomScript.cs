using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetUnitTest
{
    public static class CodeDomScript
    {
        static readonly Regex ExpressionPattern = new Regex(@"^\s*(.+?)\s*\S?=.+$");

        public static void ExecuteExpression(string expression, Dictionary<string, object> variables)
        {
            var match = ExpressionPattern.Match(expression);
            if (!match.Success) throw new FormatException();

            var leftVarName = match.Groups[1].Value;
            if (!variables.ContainsKey(leftVarName))
                variables[leftVarName] = null;
            var leftVarIndex = Array.IndexOf(variables.Keys.ToArray(), leftVarName);

            var script_vars = string.Join(", ", variables.Select(v => $"{(v.Key == leftVarName ? "ref " : "")}{(v.Value?.GetType() ?? typeof(object)).FullName} {v.Key}"));
            var source = $@"
public static class Program
{{
public static void Execute({script_vars})
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

            var action = results.CompiledAssembly.GetType("Program").GetMethod("Execute");
            var args = variables.Values.ToArray();
            action.Invoke(null, args);
            variables[leftVarName] = args[leftVarIndex];
        }
    }
}
