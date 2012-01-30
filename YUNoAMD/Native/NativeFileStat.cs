using Jurassic;
using Jurassic.Library;

namespace YUNoAMD.Native
{
    public class NativeFileStat : ObjectInstance
    {
        private readonly string _path;

        public NativeFileStat(ScriptEngine engine, string path) : base(engine)
        {
            _path = path;
        }

        [JSFunction(Name = "isFile")]
        public bool isFile()
        {
            return false;
            //return Directory.Exists(_path);  
        }

        //public bool isDirectory()
    }
}