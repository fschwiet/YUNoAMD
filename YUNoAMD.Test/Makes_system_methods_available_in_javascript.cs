using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NJasmine;
using NUnit.Framework;

namespace YUNoAMD.Test
{
    public class CompilerUsage
    {
        public StringWriter writer;
        public RequireJsCompiler compiler;

        public CompilerUsage(GivenWhenThenFixture fixture)
        {
            writer = new StringWriter();
            compiler = fixture.arrange(() => new RequireJsCompiler(writer));
        }

        public void ExpectLines(params string[] lines)
        {
            Assert.That(GetLines(), Is.EquivalentTo(lines));
        }

        private String[] GetLines()
        {
            return writer.ToString().Split(new[] { writer.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }
    }

    public class Makes_system_methods_available_in_javascript : GivenWhenThenFixture
    {
        public override void Specify()
        {
            var context = new CompilerUsage(this);

            describe("print", delegate()
            {
                it("writes the message to the console", delegate()
                {
                    context.compiler.Run("print('123')");

                    context.ExpectLines("123");
                });

                it("writes multiple messages to the console", delegate()
                {
                    context.compiler.Run("print('123','456',789);");

                    context.ExpectLines("123", "456", "789");
                });
            });

            describe("warn", delegate()
            {
                it("writes a warning to the console", delegate()
                {
                    context.compiler.Run("warn('message',1,'source.js', 3)");

                    context.ExpectLines("WARNING: source.js:1:3  message");
                });
            });
        }
    }
}
