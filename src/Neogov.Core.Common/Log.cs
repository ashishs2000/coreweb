using System;

namespace Neogov.Core.Common
{
    public static class Log
    {
        public static void Info(string message)
        {
        }

        public static void Error(string message)
        {
        }

        public static void Debug(string message)
        {
        }

        public static void Exception(Exception exception, string message = null)
        {
        }

        public static void Message(LoggingLevel loggingLevel, string message)
        {
        }
    }

    public enum LoggingLevel
    {
        Error,
        Warn,
        Info
    }
}