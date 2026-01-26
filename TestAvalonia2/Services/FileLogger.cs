using NLog;
using System;

namespace TestAvalonia2.Services
{
    public interface IFileLogger
    {
        void LogFileOpened(string filePath);
        void LogFileClosed(string filePath);
        void LogError(string message, Exception exception = null);
    }

    public class FileLogger : IFileLogger
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public void LogFileOpened(string filePath)
        {
            Logger.Info($"Открыта картинка: {filePath}");
        }

        public void LogFileClosed(string filePath)
        {
            Logger.Info($"Закрыта: {filePath}");
        }

        public void LogError(string message, Exception exception = null)
        {
            if (exception != null)
                Logger.Error(exception, message);
            else
                Logger.Error(message);
        }
    }
}