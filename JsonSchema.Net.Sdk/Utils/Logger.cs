using System;
using Microsoft.Build.Utilities;

namespace JsonSchema.Net.Sdk.Utils
{
    public static class Logger
    {
        public static void Init(TaskLoggingHelper logger)
        {
            Logger.logger = logger;
        }

        public static void LogError(string message)
        {
            logger?.LogError(message);
        }

        public static void LogError(Exception ex, string file)
        {
            logger?.LogErrorFromException(ex, false, true, file);
        }

        public static void LogMessage(string message)
        {
            logger?.LogMessage(message);
        }

        public static void LogWarning(string message)
        {
            logger?.LogWarning(message);
        }

        private static TaskLoggingHelper logger;
    }
}