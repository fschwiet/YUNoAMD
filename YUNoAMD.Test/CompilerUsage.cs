using System;
using System.IO;
using NJasmine;
using NUnit.Framework;

namespace YUNoAMD.Test
{
    public class CompilerUsage
    {
        private StringWriter _writer;
        public RequireJsCompiler compiler;

        public CompilerUsage(GivenWhenThenFixture fixture)
        {
            _writer = new StringWriter();
            compiler = fixture.arrange(() => new RequireJsCompiler(_writer));
        }

        public void ExpectLines(params string[] lines)
        {
            Assert.That(GetLines(), Is.EquivalentTo(lines));
        }

        private String[] GetLines()
        {
            return _writer.ToString().Split(new[] { _writer.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}