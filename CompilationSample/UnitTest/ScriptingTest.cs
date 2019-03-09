using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class ScriptingTest
    {
        [TestMethod]
        public void Variables()
        {
            var vars = new Dictionary<string, object>
            {
                { "x", 1 },
                { "z", 0.0 },
            };
            var globals = Tuple.Create(vars);

            var script = CSharpScript.Create($@"
var x = ({vars["x"].GetType().FullName})Item1[""x""];
var y = 1.3;
var z = ({vars["z"].GetType().FullName})Item1[""z""];
z += x + y;
", globalsType: globals.GetType());

            // 初回は 2 秒ほどかかります。
            var state = script.RunAsync(globals)
                .GetAwaiter().GetResult();
            Assert.AreEqual(2.3, state.GetVariable("z").Value);

            vars["z"] = state.GetVariable("z").Value;
            var state2 = script.RunAsync(globals)
                .GetAwaiter().GetResult();
            Assert.AreEqual(4.6, state2.GetVariable("z").Value);
        }
    }
}
