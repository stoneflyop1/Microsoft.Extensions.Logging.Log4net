using System;
using log4net.Core;

namespace Microsoft.Extensions.Logging
{
    public class Log4netLogger : ILogger
    {
        private readonly log4net.Core.ILogger m_logger;

        public Log4netLogger(Log4netConfiguration config, string categoryName)
        {
            if (!String.IsNullOrWhiteSpace(config.LoggerRepository))
            {
                m_logger = log4net.LogManager.GetLogger(config.LoggerRepository, categoryName).Logger;
            }
            else
            {
                m_logger = log4net.LogManager.GetLogger(categoryName).Logger;
            }
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return default!;
        }

        private static Level? FindLevel(LogLevel logLevel)
        {
            switch(logLevel)
            {
                case LogLevel.Debug:
                    return Level.Debug;
                case LogLevel.Trace:
                    return Level.Trace;
                case LogLevel.Information:
                    return Level.Info;
                case LogLevel.Warning:
                    return Level.Warn;
                case LogLevel.Error:
                    return Level.Error;
                case LogLevel.Critical:
                    return Level.Critical;
                default:
                    return null;
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return IsEnabled(logLevel, out _);
        }

        private bool IsEnabled(LogLevel logLevel, out Level? level)
        {
            level = FindLevel(logLevel);
            if (level != null)
            {
                return m_logger.IsEnabledFor(level);
            }
            if (logLevel == LogLevel.Critical)
            {
                return m_logger.IsEnabledFor(Level.Fatal);
            }
            return false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel, out var level))
            {
                return;
            }
            var logEvent = new LoggingEvent(typeof(Log4netExtensions), 
                m_logger.Repository, m_logger.Name, level, formatter.Invoke(state, exception), exception);
            m_logger.Log(logEvent);
        }
    }
}
