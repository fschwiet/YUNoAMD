using System;
using System.IO;
using Jurassic;
using Jurassic.Library;

namespace YUNoAMD.Native
{
    public class NativeFS : NativeBase
    {
        public NativeFS(ScriptEngine engine) : base(engine)
        {

        }

        [JSFunction(Name = "writeFileSync")]
        public void writeFileSync(string path, string content, string encoding)
        {
            CheckEncoding(encoding);

            Directory.CreateDirectory(new FileInfo(path).Directory.FullName);

            File.WriteAllText(path, content);
        }

        [JSFunction(Name = "readFileSync")]
        public string readFileSync(string path, string encoding)
        {
            CheckEncoding(encoding);

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
    }
}