using System;
using System.IO;
using Jurassic;

namespace YUNoAMD
{
    public class RequireJsCompiler
    {

        public RequireJsCompiler()
        {
            ScriptEngine jsEngine = new Jurassic.ScriptEngine();

            jsEngine.Evaluate(LoadResource("require.js"));
            jsEngine.Evaluate(LoadResource("json2.js"));
            jsEngine.Evaluate(LoadResource("adapt\rhino.js"));

            jsEngine.Evaluate(@"require({ baseUrl:'http://requirejs.resources/'});");

            var ioAdapter = new IOAdapter();

            jsEngine.SetGlobalFunction("print", (Action<string>)ioAdapter.print);
            jsEngine.SetGlobalFunction("warn", (Action<string,int,string,int>)ioAdapter.warn);
            jsEngine.SetGlobalFunction("readFile", (Func<string,string>)ioAdapter.readFile);
            jsEngine.SetGlobalFunction("load", (Action<string>)ioAdapter.load);
        }

        public string Compile(string appPath)
        {
            return File.ReadAllText(appPath);
        }

        private string LoadResource(string path)
        {
            var assembly = this.GetType().Assembly;

            foreach (var name in assembly.GetManifestResourceNames())
                Console.WriteLine(name);

            var resourceName = this.GetType().Namespace + ".requireJS." + path.Replace('\\', '.');
            using(var stream = assembly.GetManifestResourceStream(resourceName))
            {
                try
                {
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("Unable to find resource {0} at {1}.", path, resourceName), e);
                }
            }
        }
    }
}