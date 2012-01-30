using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using NJasmine;

namespace YUNoAMD.Test.Can_build_requireJS_environment
{
    public class defines_module_path : GivenWhenThenFixture
    {
        public override void Specify()
        {
            var testFolder = new TestFolder(this);

            var context = new CompilerUsage(this, testFolder.FullName);

            it("supports existsSync", delegate()
            {
                var otherFile = Path.Combine(testFolder.FullName, "someOtherFile.txt");
                File.WriteAllText(otherFile, "foo");

                var otherfolder = Path.Combine(testFolder.FullName, "RabOof");

                var script = @"
require(['path', 'print'], function(path, print) { 
    print(path.existsSync(" + Serialize(testFolder.FullName) + @"));
    print(path.existsSync(" + Serialize(otherfolder) + @"));
    print(path.existsSync(" + Serialize(otherFile) + @"));
});";
                context.compiler.Execute(script);    

                context.ExpectLines("True", "False", "True");
            });

            it("supports normalize", delegate()
            {
                var further = Path.Combine(testFolder.FullName, "turtles\\turtles\\alltheway.txt");
                var expected = further.ToLower();

                var script = @"
require(['path', 'print'], function(path, print) { 
    print(path.normalize('.\\turtles\\turtles\\alltheway.txt'));
    print(path.normalize('.\\turtles\\turtles\\..\\turtles\\alltheway.txt'));
    print(path.normalize('TuRtLeS\\tUrTlEs\\alltheway.txt'));
    print(path.normalize(" + Serialize(further) + @"));
});";
                context.compiler.Execute(script);

                context.ExpectLines(expected, expected, expected, expected);
            });

            it("supports join", delegate()
            {
                var script = @"
require(['path', 'print'], function(path, print) { 
    print(path.join('c:\\test'));
    print(path.join('c:\\test', 'second'));
    print(path.join('c:\\test', 'second\\'));
    print(path.join('c:\\test', 'second', 'third'));
});";
                context.compiler.Execute(script);

                context.ExpectLines(@"c:\test",@"c:\test\second",@"c:\test\second\",@"c:\test\second\third");
            });

            it("supports path", delegate()
            {
                var otherFile = Path.Combine(testFolder.FullName, "foo\\bar\\baz.txt");

                var script = @"
require(['path', 'print'], function(path, print) { 
    var topPath = path.path(" + Serialize(otherFile)+ @");
    print(topPath);
    var secondPath = path.path(topPath);
    print(secondPath);
});";
                context.compiler.Execute(script);

                string topPath = new FileInfo(otherFile).DirectoryName;
                string secondPath = new FileInfo(topPath).DirectoryName;
                context.ExpectLines(topPath,secondPath);
                
            });
        }

        public string Serialize(object o)
        {
            return new JavaScriptSerializer().Serialize(o);
        }
    }
}
