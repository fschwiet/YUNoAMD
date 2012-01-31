using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jurassic;
using Jurassic.Library;
using NJasmine;

namespace YUNoAMD.Test
{
    public class Jurassic_evaluate_uses_current_context : GivenWhenThenFixture
    {
        public override void Specify()
        {
            it(".NET methods evaluate scripts within their current context", delegate()
            {
                var engine = new ScriptEngine();

                var evaluator = new NestingEvaluator(engine);

                engine.SetGlobalValue("foo", evaluator);

                var result1 = engine.Evaluate("var x = 16; (function(x) { return foo.evaluateAndDouble1('x');})(2)");
                var result2 = engine.Evaluate("var x = 16; (function(x) { return foo.evaluateAndDouble2('x');})(2)");

                expect(() => (int)result1 == (int)result2 && (int)result1 == 4);
            });
        }

        public class NestingEvaluator : ObjectInstance
        {
            private readonly ScriptEngine _engine;

            public NestingEvaluator(ScriptEngine engine) : base(engine)
            {
                _engine = engine;
                this.PopulateFunctions();
            }

            [JSFunction(Name = "evaluateAndDouble1")]
            public int evaluateAndDouble1(string expression)
            {
                return (int)_engine.Evaluate(expression) * 2;
            }

            [JSFunction(Name = "evaluateAndDouble2", Flags = JSFunctionFlags.HasEngineParameter)]
            public static int evaluateAndDouble2(ScriptEngine engine, string expression)
            {
                return (int)engine.Evaluate(expression) * 2;
            }
        }
    }
}
