using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using Jurassic;
using Jurassic.Library;

namespace YUNoAMD.Native
{
    public class NativeFS : NativeBase
    {
        private readonly string _currentPath;

        public NativeFS(ScriptEngine engine, string currentPath) : base(engine)
        {
            _currentPath = currentPath;
        }

        private string AbsolutePath(string path)
        {
            if (path.StartsWith("."))
                path = path.Substring(1).TrimStart('\\', '/');

            return Path.Combine(_currentPath, path);
        }

        [JSFunction(Name = "writeFileSync")]
        public void writeFileSync(string path, string content, string encoding)
        {
            CheckEncoding(encoding);

            path = AbsolutePath(path);

            Directory.CreateDirectory(new FileInfo(path).Directory.FullName);

            File.WriteAllText(path, content);
        }

        [JSFunction(Name = "readFileSync")]
        public string readFileSync(string path, string encoding)
        {
            CheckEncoding(encoding);

            path = AbsolutePath(path);

            return File.ReadAllText(path);
        }

        private void CheckEncoding(string encoding)
        {
            if (encoding != "utf8")
                throw new JavaScriptException(_engine, "Error", "Unexpected encoding: " + encoding);
        }

        [JSFunction(Name = "mkdirSync")]
        public void mkdirSync(string path, string permissions)
        {
            if (permissions != "0777")
                throw new JavaScriptException(_engine, "Error", "Unexpected permission: " + permissions);

            Directory.CreateDirectory(path);
        }

        [JSFunction(Name = "realpathSync")]
        public string realpathSync(string path)
        {
            return AbsolutePath(path);
        }

        [JSFunction(Name = "statSync")]
        public ObjectInstance statSync(string path)
        {
            return new NativeFileStat(_engine, AbsolutePath(path));
        }

        [JSFunction(Name = "readdirSync")]
        public object readdirSync(string path)
        {
            string[] files = Directory.GetFileSystemEntries(AbsolutePath(path), "*");

            if (files.Length == 0)
                throw new Exception("emptied");
            List<string> results = new List<string>();

            foreach(var filepath in files)
            {
                results.Add(new FileInfo(filepath).Name);
            }

            return new JavaScriptSerializer().Serialize(results.OrderBy(r => r).ToArray());
        }
    }
}