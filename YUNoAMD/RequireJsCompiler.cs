using System;
using System.IO;

namespace YUNoAMD
{
    public class RequireJsCompiler
    {
        public string Compile(string appPath)
        {
            var jsEngine = new Jurassic.ScriptEngine();

            jsEngine.Evaluate(LoadResource("require.js"));
            jsEngine.Evaluate(LoadResource("json2.js"));
            jsEngine.Evaluate(LoadResource("adapt\rhino.js"));

            jsEngine.Evaluate(@"require({ baseUrl:'http://requirejs.resources/'});");

            var ioAdapter = new IOAdapter();

            jsEngine.SetGlobalFunction("print", new Action<string>(s => {}));


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