# Microsoft.Extensions.Logging.Log4net

A simple log4net Provider for Microsoft.Extensions.Logging.

## How to Use

```csharp
using Microsoft.Extensions.Logging;

using var logFact = new LoggerFactory();
logFact.AddLog4net(); // if app.config is used
logFact.AddLog4net("log4net.config"); // if a seperate config file is used

```