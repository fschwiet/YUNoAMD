using System;
using System.IO;
using System.Linq;
using Jurassic;
using NJasmine;
using NJasmine.Extras;

namespace YUNoAMD.Test
{
    public class TestFolder
    {
        public string FullName;

        public TestFolder(GivenWhenThenFixture fixture)
        {
            FullName = Path.Combine(Path.GetTempPath(), "YUNoAMD.Test");

            fixture.arrange(() => DirectoryUtil.DeleteDirectory(FullName));
            fixture.arrange(() => Directory.CreateDirectory(FullName)); 
        }
    }
}
