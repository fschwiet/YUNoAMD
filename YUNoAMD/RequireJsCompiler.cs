using System;
using System.IO;

namespace YUNoAMD
{
    public class RequireJsCompiler
    {
        public string Compile(string appPath)
        {
            return File.ReadAllText(appPath);
        }
    }
}