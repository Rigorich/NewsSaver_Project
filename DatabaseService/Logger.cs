using Microsoft.Extensions.Logging;
using ServiceStack;
using System;

namespace ServerService
{
    public class Logger
    {
        public static int CurrentLogLevel { get; set; } = 0;
        static Logger()
        {

        }
        public static void Log(string message, int level = 0)
        {
            if (level >= CurrentLogLevel)
            {
                Console.WriteLine(
                    (LogLevel)level + " - " + DateTime.Now + ": " + message);
            }
        }
    }
}
