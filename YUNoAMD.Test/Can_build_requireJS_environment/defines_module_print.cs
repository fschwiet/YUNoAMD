using NJasmine;

namespace YUNoAMD.Test.Can_build_requireJS_environment
{
    public class defines_module_print : GivenWhenThenFixture
    {
        public override void Specify()
        {
            var context = new CompilerUsage(this);

            arrange(() => context.compiler.SetupModuleFromResource(RequireJsCompiler.ResourceBaseUrl + "print.js", @"build\jslib\yunoamd\print.js"));

            it("writes the message to the console", delegate()
            {
                context.compiler.Evaluate("require(['print'], function(print) { print('123'); });");

                context.ExpectLines("123");
            });

            it("writes multiple messages to the console", delegate()
            {
                context.compiler.Evaluate("require(['print'], function(print) { print('123', '456', '789'); });");

                context.ExpectLines("123", "456", "789");
            });

            describe("warn", delegate()
            {
                it("writes a warning to the console", delegate()
                {
                    context.compiler.Evaluate("ioe.warn('message',1,'source.js', 3)");

                    context.ExpectLines("WARNING: source.js:1:3  message");
                });
            });
        }
    }
}
