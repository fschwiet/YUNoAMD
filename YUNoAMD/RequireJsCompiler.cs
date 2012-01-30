using System;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using Jurassic;

namespace YUNoAMD
{
    public class RequireJsCompiler
    {
        public const string ResourceBaseUrl = "http://requirejs.resources/";
        
        private ScriptEngine _jsEngine;
        IOAdapter _ioAdapter;
        
        public RequireJsCompiler(TextWriter consoleOut)
        {
            _jsEngine = new ScriptEngine();

            _jsEngine.Evaluate(LoadResource("require.js"));
            _jsEngine.Evaluate(LoadResource("json2.js"));
            _jsEngine.Evaluate(LoadResource(@"adapt\rhino.js"));

            _jsEngine.Evaluate("require(" + new JavaScriptSerializer().Serialize(new {
                baseUrl = ResourceBaseUrl
            }) + ");");

            _ioAdapter = new IOAdapter(_jsEngine, consoleOut);

            _jsEngine.SetGlobalFunction("load", (Action<string>)_ioAdapter.load);

            _jsEngine.SetGlobalValue("ioe", _ioAdapter);
            _jsEngine.SetGlobalValue("IsRunningYUNoAMD", true);

            SetupModuleFromResource(RequireJsCompiler.ResourceBaseUrl + "env.js", @"build\jslib\env.js");
            SetupModuleFromResource(RequireJsCompiler.ResourceBaseUrl + "yunoamd/args.js", @"build\jslib\yunoamd\args.js");
            SetupModuleFromResource(RequireJsCompiler.ResourceBaseUrl + "build.js", @"build\build.js");
            SetupModuleFromResource(RequireJsCompiler.ResourceBaseUrl + "print.js", @"build\jslib\yunoamd\print.js");
            SetupModuleFromResource(RequireJsCompiler.ResourceBaseUrl + "fs.js", @"build\jslib\yunoamd\fs.js");
        }

        private delegate void PrintDelegate(params string[] messages);

        public string Compile(string filePath)
        {
            string appName = filePath;
            if (appName.EndsWith(".js", StringComparison.InvariantCultureIgnoreCase))
                appName = appName.Substring(0, appName.LastIndexOf(".js"));

            var script = File.ReadAllText(filePath);

            RunWithArguments(script, new []{"",
                "",
                "name=" + appName,
                "out=" + "c:\\outpuatPath\\hmm.txt",
                "baseUrl=http://hithere/"});

            return File.ReadAllText(filePath);
        }

        public string LoadResource(string path)
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

        public void Evaluate(string code)
        {
            _jsEngine.Evaluate(code);
        }

        public void Execute(string code)
        {
            _jsEngine.Execute(code);
        }

        public void RunWithArguments(string script, string[] arguments)
        {
            string serializedArguments = new JavaScriptSerializer().Serialize(arguments);

            _jsEngine.Execute("arguments = " + serializedArguments + ";");

            _jsEngine.Execute(script);
        }

        public void SetupModuleFromResource(string name, string resourcePath)
        {
            var resourceContent = LoadResource(resourcePath);
            _ioAdapter.SetContent(name, resourceContent);
        }
    }
}