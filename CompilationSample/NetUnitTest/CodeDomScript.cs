using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

namespace NetUnitTest
{
    public static class CodeDomScript
    {
        public static void ExecuteExpression(string expression, Dictionary<string, object> variables)
        {
            var script_vars = string.Join(", ", variables.Select(v => $"ref {(v.Value?.GetType() ?? typeof(object)).FullName} {v.Key}"));
            var source = $@"
public static class Program
{{
public static void Action_0({script_vars})
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

            var action = results.CompiledAssembly.GetType("Program").GetMethod("Action_0");
            var args = variables.Values.ToArray();
            action.Invoke(null, args);

            var newValues = variables.Zip(args, (p, v) => (name: p.Key, v)).ToArray();
            foreach (var (name, value) in newValues)
                variables[name] = value;
        }
    }
}
