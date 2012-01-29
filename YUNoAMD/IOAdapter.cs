using System;
using System.IO;

namespace YUNoAMD
{
    public class IOAdapter
    {
        private readonly TextWriter _consoleOut;

        public IOAdapter(TextWriter consoleOut)
        {
            _consoleOut = consoleOut;
        }

        public void print(params string[] messages)
        {
            foreach (var message in messages)
                _consoleOut.WriteLine(message);
        }

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
            throw new NotImplementedException();
        }
    }
}