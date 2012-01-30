using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NJasmine;

namespace YUNoAMD.Test.Can_build_requireJS_environment
{
    public class has_warn_as_global : GivenWhenThenFixture
    {
        public override void Specify()
        {
            var context = new CompilerUsage(this);
            describe("warn", delegate()
            {
                it("writes a warning to the console", delegate()
                {
                    context.compiler.Evaluate("ioe.warn('message',1,'source.js', 3)");

                    context.ExpectLines("WARNING: source.js:1:3  message");
                });
            });
        }
    };
}
