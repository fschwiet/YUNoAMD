using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Jurassic;
using Jurassic.Library;

namespace YUNoAMD.Native
{
    public class NativePath : NativeFileLocation
    {
        public NativePath(ScriptEngine engine, string currentPath) : base(engine, currentPath)
        {
        }

        [JSFunction(Name = "existsSync")]
        public bool existsSync(string path)
        {
            path = AbsolutePath(path);
            return File.Exists(path) || Directory.Exists(path);
        }

        [JSFunction(Name = "normalize")]
        public string normalize(string path)
        {
            path = AbsolutePath(path);
            return Path.GetFullPath(path).ToLowerInvariant();
        }

        [JSFunction(Name = "join")]
        public string join(params string[] paths)
        {
            var current = paths[0];

            for(var i = 1; i <paths.Length; i++)
            {
                current = Path.Combine(current, paths[i]);
            }

            return current;
        }

        [JSFunction(Name = "dirname")]
        public string dirname(string path)
        {
            return new FileInfo(AbsolutePath(path)).DirectoryName;
        }
    }
}
