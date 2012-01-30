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
                it("can write a string", delegate()
                {
                    var expectedContent = Guid.NewGuid().ToString();
                    var targetPath = Path.Combine(testDirectory, "wrote.txt");

                    Func<string,string> serialize = new JavaScriptSerializer().Serialize;

                    var script = "require(['fs'], function(fs) { fs.writeFileSync( " 
                        + serialize(targetPath) + ", " + serialize(expectedContent) + ",'utf8'); });";

                    context.compiler.Execute(script);

                    expect(() => File.ReadAllText(targetPath) == expectedContent);
                });
            });
        }
    }
}
