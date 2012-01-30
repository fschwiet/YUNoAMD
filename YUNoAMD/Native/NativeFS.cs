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

        [JSFunction(Name= "writeFileSync")]
        public void writeFileSync(string path, string content, string encoding)
        {
            if (encoding != "utf8")
                throw new JavaScriptException(_engine, "Error", "Unexpected encoding: " + encoding);

            Directory.CreateDirectory(new FileInfo(path).Directory.FullName);

            File.WriteAllText(path, content);
        }
    }
}