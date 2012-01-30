using NJasmine;

namespace YUNoAMD.Test.Can_build_requireJS_environment
{
    public class defines_module_print : GivenWhenThenFixture
    {
        public override void Specify()
        {
            var context = new CompilerUsage(this);

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
        }
    }
}
