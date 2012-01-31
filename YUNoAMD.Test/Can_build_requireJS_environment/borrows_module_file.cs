using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NJasmine;

namespace YUNoAMD.Test.Can_build_requireJS_environment
{
    public class borrows_module_file : GivenWhenThenFixture
    {
        public override void Specify()
        {
            describe("the file module provided for node only needs fs and path", delegate()
            {
                var testFolder = new TestFolder(this);
            
                var context = new CompilerUsage(this, testFolder.FullName);

                var fileToWrite = arrange(() => Path.Combine(testFolder.FullName, "file.txt"));
                
                it("allows path to be used", delegate()
                {
                    var script = @"
require(['env!env/args', 'file'],
    function (args, file) {
        file.saveUtf8File(args[0], 'hello, world');
    });
";
                    expect(() => !File.Exists(fileToWrite));

                    context.compiler.RunWithArguments(script, new[] { fileToWrite });

                    expect(() => File.ReadAllText(fileToWrite) == "hello, world");
                });
            });
        }
    }
}
