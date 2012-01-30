using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;
using NJasmine;
using NJasmine.Extras;

namespace YUNoAMD.Test.Can_build_requireJS_environment
{
    public class defines_module_fs : GivenWhenThenFixture
    {
        public override void Specify()
        {
            var testDirectory = Path.Combine(Path.GetTempPath(), "YUNoAMD.Test");

            var context = new CompilerUsage(this, testDirectory);

            arrange(() => DirectoryUtil.DeleteDirectory(testDirectory));

            var expectedContent = Guid.NewGuid().ToString();
            string relativeTargetPath = @"la\de\da\wrote.txt";
            var targetPath = arrange(() => Path.Combine(testDirectory, relativeTargetPath));

            var writeScript = arrange(() =>  "require(['fs'], function(fs) { fs.writeFileSync( "
                + Serialize(targetPath) + ", " + Serialize(expectedContent) + ",'utf8'); });");

            it("supports writeFileSync", delegate()
            {
                context.compiler.Execute(writeScript);

                expect(() => File.ReadAllText(targetPath) == expectedContent);
            });

            it("supports readFileSync", delegate()
            {
                context.compiler.Execute(writeScript);

                var echoScript = arrange(() => "require(['fs', 'print'], function(fs, print) { print(fs.readFileSync( "
                    + Serialize(targetPath) + ",'utf8')); });");

                context.compiler.Execute(echoScript);

                context.ExpectLines(expectedContent);
            });

            it("supports mkdirSync", delegate()
            {
                var otherDirectory = Path.Combine(testDirectory, "CreatedByNativeFS");

                expect(() => !Directory.Exists(otherDirectory));

                var echoScript = "require(['fs'], function(fs) { fs.mkdirSync( "
                    + Serialize(otherDirectory) + ",'0777'); });";

                context.compiler.Execute(echoScript);

                expect(() => Directory.Exists(otherDirectory));
            });

            foreach(var pathVariation in new[] { relativeTargetPath, ".\\" + relativeTargetPath})
            {
                it("supports realpathSync for " + pathVariation, delegate()
                {
                    context.compiler.Execute(writeScript);

                    var realScript = "require(['fs', 'print'], function(fs, print) { print(fs.realpathSync( "
                        + Serialize(pathVariation) + ",'utf8')); });";

                    context.compiler.Execute(realScript);

                    context.ExpectLines(targetPath);
                });
            }

            describe("statSync", delegate()
            {
                foreach (var expression in new Dictionary<string, string>()
                {
                    {"fileStats.isFile()", "True"},
                    {"dirStats.isFile()", "False"},
                    {"fileStats.isDirectory()", "False"},
                    {"dirStats.isDirectory()", "True"}
                })
                it("evalutes expression " + expression.Key + " as " + expression.Value, delegate()
                {
                    context.compiler.Execute(writeScript);

                    var statScript = @"
require(['fs', 'print'], function(fs, print) { 
    var fileStats = fs.statSync(" + Serialize(targetPath) + @");
    var dirStats = fs.statSync(" + Serialize(new FileInfo(targetPath).Directory.FullName) + @");
    print(" + expression.Key + @");
});";

                    context.compiler.Execute(statScript);

                    context.ExpectLines(expression.Value);
                });

                it("reports last modified time", delegate()
                {
                    var before = (double)DateTime.UtcNow.ToFileTimeUtc();
                    context.compiler.Execute(writeScript);
                    var after = (double)DateTime.UtcNow.ToFileTimeUtc();

                    var statScript = @"
require(['fs', 'print'], function(fs, print) { 
    var fileStats = fs.statSync(" + Serialize(targetPath) + @");
    print(fileStats.mtime.getTime());
});";
                    context.compiler.Execute(statScript);

                    var time = double.Parse(context.GetLines().Single());

                    expect(() => before <= time && time <= after);
                });
            });

            describe("readdirSync", delegate()
            {
                it("lists file and directory names", delegate()
                {
                    Directory.CreateDirectory(testDirectory);

                    File.WriteAllText(Path.Combine(testDirectory, "foo.txt"), "123");
                    File.WriteAllText(Path.Combine(testDirectory, "bar.html"), "123");
                    File.WriteAllText(Path.Combine(testDirectory, "baz.css"), "123");
                    Directory.CreateDirectory(Path.Combine(testDirectory, "qux"));

                    var readdirScript = @"
require(['fs', 'print'], function(fs, print) { 
    var names = fs.readdirSync(" + Serialize(testDirectory) + @");
    print.apply(null, names);
});";
                    context.compiler.Execute(readdirScript);

                    context.ExpectLines("bar.html", "baz.css", "foo.txt", "qux");
                });
            });
        }

        public string Serialize(object o)
        {
            return new JavaScriptSerializer().Serialize(o);
        }
    }
}
