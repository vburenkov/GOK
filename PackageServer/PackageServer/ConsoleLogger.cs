using System;
using System.Collections.Generic;
using System.Text;

namespace PackageServer
{
    public class ConsoleLogger : ILogger
    {
        public void LogError(string s)
        {
            Console.WriteLine(s);
        }

        public void LogInfo(string s)
        {
            Console.WriteLine(s);
        }
    }
}
