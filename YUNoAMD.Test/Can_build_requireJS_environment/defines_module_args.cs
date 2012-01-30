using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using NJasmine;

namespace YUNoAMD.Test.Can_build_requireJS_environment
{
    public class defines_module_args : GivenWhenThenFixture
    {
        
        public override void Specify()
        {
            given("a script which echoes args", delegate()
            {
                var script = @"
require(['env!env/args'],
    function (args) {
        for(var i = 0; i < args.length; i++) {
            print(args[i]);
        }
    });
";
                when("ran with arguments", delegate()
                {
                    var writer = new StringWriter();
                    arrange(delegate()
                    {
                        var compiler = new RequireJsCompiler(writer);

                        compiler.RunWithArguments(script, new object[]
                        {
                            "1",
                            23,
                            "456"
                        });
                    });

                    it("echoes the arguments", delegate()
                    {

                        expect(() => writer.ToString() == "123" + writer.NewLine);
                    });
                    
                });
            });

            describe("global variable arguments can be require()d", delegate()
            {
                
            });
        }
    }
}
