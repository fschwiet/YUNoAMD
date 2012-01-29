using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NJasmine;
using NJasmine.Extras;
using NUnit.Framework;

namespace YUNoAMD.Test
{
    public class Can_build_requireJS_project : GivenWhenThenFixture
    {
        public override void Specify()
        {
            given("a project using requireJS", delegate()
            {
                var projectPath = arrange(() => 
                    ZipDeployTools.UnzipBinDeployedToTempDirectory("ConcoctScenario.zip", "YUNoAMD.Test"));

                string appPath = arrange(() => Path.Combine(projectPath, "app.js"));

                expect(() => File.Exists(appPath));

                when("that project's app file is compiled", delegate()
                {
                    var compiledCode = arrange(() => new RequireJsCompiler().Compile(appPath));

                    then("the compiled code equals the expected result", delegate()
                    {
                        var expectedCode = File.ReadAllText(Path.Combine(projectPath, "compiled.js"));

                        Assert.That(compiledCode, Is.EqualTo(expectedCode));
                    });
                });
            });
        }
    }
}
