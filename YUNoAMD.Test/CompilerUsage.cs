using System;
using System.IO;
using NJasmine;
using NUnit.Framework;

namespace YUNoAMD.Test
{
    public class CompilerUsage
    {
        public StringWriter writer;
        public RequireJsCompiler compiler;

        public CompilerUsage(GivenWhenThenFixture fixture)
        {
            writer = new StringWriter();
            compiler = fixture.arrange(() => new RequireJsCompiler(writer));
        }

        public void ExpectLines(params string[] lines)
        {
            Assert.That(GetLines(), Is.EquivalentTo(lines));
        }

        private String[] GetLines()
        {
            return writer.ToString().Split(new[] { writer.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}