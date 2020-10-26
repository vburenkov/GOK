using System;

namespace PackageServer
{
    public interface ILogger
    {
        void LogInfo(string s);
        void LogError(string s);
    }
}
