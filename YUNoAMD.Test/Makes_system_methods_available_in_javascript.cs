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
                it("passes a message to the console", delegate()
                {
                    compiler.Run("print('123')");

                    expect(() => writer.ToString() == "123" + writer.NewLine);
                });

                it("passes multiple messages to the console", delegate()
                {
                    compiler.Run("print('123','456',789);");

                    expect(() => writer.ToString() == "123" + writer.NewLine + "456" + writer.NewLine + "789" + writer.NewLine);
                });
            });
        }
    }
}
