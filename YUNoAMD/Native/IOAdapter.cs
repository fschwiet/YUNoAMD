using System;
using System.Collections.Generic;
using System.IO;
using Jurassic;
using Jurassic.Library;

namespace YUNoAMD.Native
{
    public class IOAdapter : NativeBase
    {
        private readonly TextWriter _consoleOut;
        Dictionary<string,string> _preloadedContent = new Dictionary<string, string>();

        public IOAdapter(ScriptEngine engine, TextWriter consoleOut) : base(engine)
        {
            _consoleOut = consoleOut;
        }

        [JSFunction(Name = "print")]
        public void print(params object[] messages)
        {
            foreach (var message in messages)
            {
                if (message != null)
                    _consoleOut.WriteLine(message);
            }
        }

        [JSFunction(Name = "warn")]
        public void warn(string message, int line, string source, int column)
        {
            _consoleOut.WriteLine("WARNING: {0}:{1}:{2}  {3}", source, line, column, message);
        }

        public string readFile(string path)
        {
            throw new NotImplementedException();
        }

        public void load(string path)
        {
            if (_preloadedContent.ContainsKey(path))
            {
                _engine.Execute(_preloadedContent[path]);
            }
            else
            {
                _consoleOut.WriteLine("tried to load: " + path);
                throw new NotImplementedException("tried to load: " + path);
            }
        }

        public void SetContent(string path, string content)
        {
            _preloadedContent[path] = content;
        }
    }
}