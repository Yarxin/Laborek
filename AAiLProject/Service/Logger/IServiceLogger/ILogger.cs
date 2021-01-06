using System;

namespace AAiLProject.Service.Logger.IServiceLogger
{
    internal interface ILogger
    {
        void LogError(Exception exception, string errorCode);
        void LogEvent(string eventCode);
    }
}