using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.Logging
{
    public static class Log4netExtensions
    {

        public static ILoggerFactory AddLog4net(this ILoggerFactory loggerFactory, string? configFile = null, bool watch = false)
        {
            loggerFactory.AddProvider(new Log4netProvider(configFile, watch));
            return loggerFactory;
        }

        public static ILoggerFactory AddLog4net(this ILoggerFactory loggerFactory, Log4netConfiguration config)
        {
            loggerFactory.AddProvider(new Log4netProvider(config));
            return loggerFactory;
        }

        public static ILoggingBuilder AddLog4net(this ILoggingBuilder builder, string? configFile = null, bool watch = false)
        {
            return builder.AddProvider(new Log4netProvider(configFile, watch));
        }

        public static ILoggingBuilder AddLog4net(this ILoggingBuilder builder, Log4netConfiguration config)
        {
            return builder.AddProvider(new Log4netProvider(config));
        }
    }
}
