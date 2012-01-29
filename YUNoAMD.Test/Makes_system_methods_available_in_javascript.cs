using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NJasmine;

namespace YUNoAMD.Test
{
    public class Makes_system_methods_available_in_javascript : GivenWhenThenFixture
    {
        public override void Specify()
        {
            var writer = new StringWriter();
            var compiler = arrange(() => new RequireJsCompiler(writer));

            describe("print", delegate()
            {
                it("writes the message to the console", delegate()
                {
                    compiler.Run("print('123')");

                    expect(() => writer.ToString() == "123" + writer.NewLine);
                });

                it("writes multiple messages to the console", delegate()
                {
                    compiler.Run("print('123','456',789);");

                    expect(() => writer.ToString() == "123" + writer.NewLine + "456" + writer.NewLine + "789" + writer.NewLine);
                });
            });

            describe("warn", delegate()
            {
                it("writes a warning to the console", delegate()
                {
                    compiler.Run("warn('message',1,'source.js', 3)");

                    expect(() => writer.ToString() == "WARNING: source.js:1:3  message" + writer.NewLine);
                });
            });
        }
    }
}
