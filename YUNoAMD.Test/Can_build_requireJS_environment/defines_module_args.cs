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
            ioe.print(args[i]);
        }
    });
";
                when("jslib\\YUNoarg\\args.js is ran with the script", delegate()
                {
                    var context = new CompilerUsage(this);

                    arrange(delegate
                    {
                        context.compiler.LoadResource(@"build\jslib\yunoamd\args.js");

                        context.compiler.RunWithArguments(script, new string[]
                        {
                            "1",
                            23.ToString(),
                            "456"
                        });
                    });

                    it("echoes the arguments", delegate()
                    {
                        context.ExpectLines("1", "23", "456");
                    });
                });
            });
        }
    }
}
