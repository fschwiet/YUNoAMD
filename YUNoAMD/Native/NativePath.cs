﻿using System;
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
    }
}
