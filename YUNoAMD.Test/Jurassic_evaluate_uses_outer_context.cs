using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jurassic;
using Jurassic.Library;
using NJasmine;

namespace YUNoAMD.Test
{
    public class Jurassic_evaluate_uses_outer_context : GivenWhenThenFixture
    {
        public override void Specify()
        {
            it(".NET methods evaluate scripts within their current context", delegate()
            {
                var engine = new ScriptEngine();

                var evaluator = new NestingEvaluator(engine);

                engine.SetGlobalValue("foo", evaluator);

                var result = engine.Evaluate("var x = 16; (function(x) { return foo.evaluateAndDouble('x');})(2)");

                expect(() => (int)result == 4);
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

            [JSFunction(Name = "evaluateAndDouble")]
            public int evaluateAndDouble(string expression)
            {
                return (int)_engine.Evaluate(expression) * 2; 
            }
        }
    }
}
