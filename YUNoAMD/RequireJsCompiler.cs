using System;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using Jurassic;

namespace YUNoAMD
{
    public class RequireJsCompiler
    {
        private ScriptEngine _jsEngine;
        private const string _resourceBaseUrl = "http://requirejs.resources/";

        public RequireJsCompiler(TextWriter consoleOut)
        {
            _jsEngine = new ScriptEngine();

            _jsEngine.Evaluate(LoadResource("require.js"));
            _jsEngine.Evaluate(LoadResource("json2.js"));
            _jsEngine.Evaluate(LoadResource(@"adapt\rhino.js"));

            _jsEngine.Evaluate("require(" + new JavaScriptSerializer().Serialize(new {
                baseUrl = _resourceBaseUrl
            }) + ");");

            var ioAdapter = new IOAdapter(consoleOut);

            _jsEngine.SetGlobalFunction("print", (PrintDelegate)ioAdapter.print);
            _jsEngine.SetGlobalFunction("warn", (Action<string, int, string, int>)ioAdapter.warn);
            _jsEngine.SetGlobalFunction("readFile", (Func<string, string>)ioAdapter.readFile);
            _jsEngine.SetGlobalFunction("load", (Action<string>)ioAdapter.load);
        }

        private delegate void PrintDelegate(params string[] messages);

        public string Compile(string filePath)
        {
            string appName = filePath;
            if (appName.EndsWith(".js", StringComparison.InvariantCultureIgnoreCase))
                appName = appName.Substring(0, appName.LastIndexOf(".js"));
            

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("(function(){ // " + filePath);
            sb.AppendLine("var arguments = ");
            sb.AppendLine(new JavaScriptSerializer().Serialize(new object[]
            {
                "",
                "",
                "name=" + appName,
                "out=" + "c:\\outpuatPath\\hmm.txt",
                "baseUrl=" 
            }));

            sb.AppendLine(LoadResource(@"build\build.js"));

            sb.AppendLine("})();");

            _jsEngine.Execute(sb.ToString());

            return File.ReadAllText(filePath);
        }

        private string LoadResource(string path)
        {
            var assembly = this.GetType().Assembly;

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
                    throw new Exception(String.Format("Unable to find resource {0} at {1}.", path, resourceName), e);
                }
            }
        }

        public void Run(string code)
        {
            _jsEngine.Evaluate(code);
        }

        public void RunWithArguments(string script, object[] arguments)
        {
            _jsEngine.SetGlobalValue("arguments", new JavaScriptSerializer()
                .Serialize(
                    arguments));

            _jsEngine.Execute(script);
        }
    }
}