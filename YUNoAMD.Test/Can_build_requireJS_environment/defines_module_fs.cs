using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using NJasmine;
using NJasmine.Extras;

namespace YUNoAMD.Test.Can_build_requireJS_environment
{
    public class defines_module_fs : GivenWhenThenFixture
    {
        public override void Specify()
        {
            var context = new CompilerUsage(this);

            var testDirectory = Path.Combine(Path.GetTempPath(), "YUNoAMD.Test");
            arrange(() => DirectoryUtil.DeleteDirectory(testDirectory));

            describe("supports read and write", delegate()
            {
                var expectedContent = Guid.NewGuid().ToString();
                var targetPath = arrange(() => Path.Combine(testDirectory, "wrote.txt"));
                var writeScript = arrange(() =>  "require(['fs'], function(fs) { fs.writeFileSync( "
                    + Serialize(targetPath) + ", " + Serialize(expectedContent) + ",'utf8'); });");

                it("can write a string", delegate()
                {
                    context.compiler.Execute(writeScript);

                    expect(() => File.ReadAllText(targetPath) == expectedContent);
                });

                it("can read a string", delegate()
                {
                    context.compiler.Execute(writeScript);

                    var echoScript = arrange(() => "require(['fs', 'print'], function(fs, print) { print(fs.readFileSync( "
                        + Serialize(targetPath) + ",'utf8')); });");

                    context.compiler.Execute(echoScript);

                    context.ExpectLines(expectedContent);
                });
            });
        }

        public string Serialize(object o)
        {
            return new JavaScriptSerializer().Serialize(o);
        }
    }
}
