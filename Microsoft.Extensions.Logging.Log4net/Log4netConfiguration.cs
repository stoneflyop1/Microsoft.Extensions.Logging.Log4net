using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using log4net.Config;

namespace Microsoft.Extensions.Logging
{
    public class Log4netConfiguration
    {
        public Log4netConfiguration(string? configFile = null, bool watch = true)
        {
            if (configFile == null)
            {
                UseWebOrAppConfig = true;
            }
            else
            {
                ConfigFile = configFile;
            }
            Watch = watch;
        }

        public string? ConfigFile { get; }
        public bool UseWebOrAppConfig { get; }

        public bool Watch { get; }

        public string? LoggerRepository { get; set; }
    }
}
