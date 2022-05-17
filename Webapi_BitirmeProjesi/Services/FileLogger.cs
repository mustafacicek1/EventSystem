using System.IO;

namespace Webapi_BitirmeProjesi.Services
{
    public class FileLogger : ILoggerService
    {
        public void Log(string message)
        {
            File.AppendAllText("log.txt", message+"\n");
        }
    }
}
