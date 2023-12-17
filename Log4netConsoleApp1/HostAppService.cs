using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Log4netConsoleApp1
{
    public class HostAppService
    {
        private readonly ILogger m_logger;

        public HostAppService(ILogger<HostAppService> logger)
        {
            m_logger = logger;
        }

        public void Run()
        {
            m_logger.LogWarning("Step1...");
            m_logger.LogWarning("Step2...");
        }
    }
}
