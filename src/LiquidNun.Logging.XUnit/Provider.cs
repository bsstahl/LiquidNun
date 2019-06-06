using System;
using LiquidNun.Entities;
using Xunit.Abstractions;

namespace LiquidNun.Logging.XUnit
{
    public class Provider : LoggingProviderBase
    {
        readonly ITestOutputHelper _output;
        public Provider(ITestOutputHelper output)
        {
            _output = output;
        }

        public override void Write(string category, string title, string message, int severity, EventLogEntryType entryType)
        {
            _output.WriteLine($"Logger: {title} ({category}/{severity}) - {message}\r\n");
        }
    }
}
