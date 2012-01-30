using System.Collections.Generic;
using System.Linq;
using System.Text;
using NJasmine;

namespace YUNoAMD.Test
{
    public class Makes_system_methods_available_in_javascript : GivenWhenThenFixture
    {
        public override void Specify()
        {
            var context = new CompilerUsage(this);

            describe("print", delegate()
            {
                it("writes the message to the console", delegate()
                {
                    context.compiler.Evaluate("print('123')");

                    context.ExpectLines("123");
                });

                it("writes multiple messages to the console", delegate()
                {
                    context.compiler.Evaluate("print('123','456',789);");

                    context.ExpectLines("123", "456", "789");
                });
            });

            describe("warn", delegate()
            {
                it("writes a warning to the console", delegate()
                {
                    context.compiler.Evaluate("warn('message',1,'source.js', 3)");

                    context.ExpectLines("WARNING: source.js:1:3  message");
                });
            });
        }
    }
}
