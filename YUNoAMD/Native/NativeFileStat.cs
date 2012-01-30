using System.IO;
using Jurassic;
using Jurassic.Library;

namespace YUNoAMD.Native
{
    public class NativeFileStat : NativeBase
    {
        private readonly string _path;

        public NativeFileStat(ScriptEngine engine, string path) : base(engine)
        {
            _path = path;
        }

        [JSFunction(Name = "isFile")]
        public bool isFile()
        {
            return File.Exists(_path);  
        }

        [JSFunction(Name = "isDirectory")]
        public bool isDirectory()
        {
            return Directory.Exists(_path);
        }
    }
}