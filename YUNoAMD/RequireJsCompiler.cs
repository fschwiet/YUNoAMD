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
            jsEngine.Evaluate(LoadResource("adapt/rhino.js"));

            jsEngine.Evaluate(@"require({ baseUrl:'http://requirejs.resources/'});");

            var ioAdapter = new IOAdapter();

            jsEngine.SetGlobalFunction("print", new Action<string>(s => {}));


            return File.ReadAllText(appPath);
        }

        private string LoadResource(string path)
        {
            var assembly = this.GetType().Assembly;

            using(var reader = assembly.GetManifestResourceStream(this.GetType().Namespace + ".requireJS." + path))
            {
                
            }

            return "";
        }
    }
}