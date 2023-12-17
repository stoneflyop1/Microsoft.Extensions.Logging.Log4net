using System;
using System.Collections.Concurrent;
using System.IO;
using log4net.Config;
using log4net.Repository;

namespace Microsoft.Extensions.Logging
{
    public class Log4netProvider : ILoggerProvider
    {
        private readonly Log4netConfiguration m_configuration;

        private readonly ILoggerRepository m_repo;

        private readonly ConcurrentDictionary<string, ILogger> m_loggerCache 
            = new ConcurrentDictionary<string, ILogger>();

        public Log4netProvider(string? configFile = null, bool watch = true) 
            : this(new Log4netConfiguration(configFile, watch))
        {
        }

        public Log4netProvider(Log4netConfiguration config)
        {
            m_configuration = config;
            var repo = CreateRepository(m_configuration);
            if (repo == null)
            {
                throw new Exception("create LoggerRepo failed!");
            }
            m_repo = repo;

            if (m_configuration.UseWebOrAppConfig)
            {
                XmlConfigurator.Configure(this.m_repo);
            }
            else
            {
                if (m_configuration.Watch)
                {
                    XmlConfigurator.ConfigureAndWatch(this.m_repo, new FileInfo(m_configuration.ConfigFile));
                }
                else
                {
                    XmlConfigurator.Configure(this.m_repo, new FileInfo(m_configuration.ConfigFile));
                }
            }
        }

        public ILogger CreateLogger(string categoryName)
        {
            return GetOrCreateLogger(categoryName);
        }

        private ILogger GetOrCreateLogger(string categoryName)
        {
            return m_loggerCache.GetOrAdd(categoryName, cn => new Log4netLogger(m_configuration, cn));
        }

        public void Dispose()
        {
            m_repo.Shutdown();
            m_loggerCache.Clear();
        }

        private static ILoggerRepository? CreateRepository(Log4netConfiguration config)
        {
            ILoggerRepository? repo;
            if (!String.IsNullOrWhiteSpace(config.LoggerRepository))
            {
                try
                {
                    repo = log4net.LogManager.GetRepository(config.LoggerRepository);
                }
                catch {
                    repo = null;
                }
                if (repo == null)
                {
                    repo = log4net.LogManager.CreateRepository(config.LoggerRepository);
                }
            }
            else
            {
                try
                {
                    repo = log4net.LogManager.GetRepository();
                }
                catch
                {
                    repo = null;
                }
                if (repo == null)
                {
                    repo = log4net.LogManager.CreateRepository(typeof(log4net.Repository.Hierarchy.Hierarchy));
                }
            }
            return repo;

        }
    }
}
