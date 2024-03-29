﻿using NLog;
using OmerOzkan.ToDo.Business.Interfaces;

namespace OmerOzkan.ToDo.Business.CustomLogger
{
    public class NLogLogger : ICustomLogger
    {
        public void LogError(string message)
        {
            var logger = LogManager.GetLogger("loggerFile");
            logger.Log(LogLevel.Error, message);
        }
    }
}
