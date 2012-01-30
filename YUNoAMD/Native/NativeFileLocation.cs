using System.IO;
using Jurassic;

namespace YUNoAMD.Native
{
    public class NativeFileLocation : NativeBase
    {
        private string _currentPath;

        public NativeFileLocation(ScriptEngine engine, string currentPath) : base(engine)
        {
            _currentPath = currentPath;
        }

        protected string AbsolutePath(string path)
        {
            if (path.StartsWith("."))
                path = path.Substring(1).TrimStart('\\', '/');

            return Path.Combine(_currentPath, path);
        }
    }
}