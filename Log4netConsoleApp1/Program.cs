
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Log4netConsoleApp1
{
    internal class Program
    {
        static Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            TestConfigFile();
            return TestAppConfigAsync(args);
        }

        static void TestConfigFile()
        {
            using var logFact = new LoggerFactory();
            logFact.AddLog4net("log4net.config");
            var log = logFact.CreateLogger("nihao");
            log.LogWarning("test for log4net.config");
            var log2 = logFact.CreateLogger("wohao");
            log2.LogWarning("test for log4net.config2");
        }

        static Task TestAppConfigAsync(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var app = host.Services.GetRequiredService<HostAppService>();
            app.Run();
            return host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureServices(
                srvs => srvs.AddTransient<HostAppService>()).ConfigureLogging(builder =>
            {
                builder.AddLog4net();
            });
        }
    }
}
