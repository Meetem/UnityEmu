using System;

namespace UnityEngine
{
    public static class Debug
    {
        private static void Log(string message, string level, object context = null)
        {
            var dt = DateTime.Now;
            var dtStr = dt.ToString("d hh:mm:ss.") + dt.Millisecond.ToString("000");
            
            Console.WriteLine($"[{dtStr}] {level}: {message}");
        }
        
        public static void Log(object context, string message)
        {
            Log(message, "Log", context);
        }

        public static void LogError(object context, string message)
        {
            Log(message, "Error", context);
        }
        
        public static void LogWarning(object context, string message)
        {
            Log(message, "Warning", context);
        }

        public static void LogFormat(string format, params object[] args)
        {
            Log(string.Format(format, args), "Log");
        }

        public static void LogFormat(object context, string format, params object[] args)
        {
            Log(string.Format(format, args), "Log", context);
        }
        
        public static void LogWarningFormat(string format, params object[] args)
        {
            Log(string.Format(format, args), "Warning");
        }

        public static void LogWarningFormat(object context, string format, params object[] args)
        {
            Log(string.Format(format, args), "Warning", context);
        }
        
        public static void LogErrorFormat(string format, params object[] args)
        {
            Log(string.Format(format, args), "Error");
        }
        
        public static void LogErrorFormat(object context, string format, params object[] args)
        {
            Log(string.Format(format, args), "Error", context);
        }
    }
}