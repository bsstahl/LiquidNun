using LiquidNun.Entities;
using LiquidNun.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiquidNun.Logging.GenericSink
{
    /// <summary>
    /// Logs the specified events to the Console
    /// </summary>
    public class Provider : LoggingProviderBase
    {
        /// <summary>
        /// Writes the log entry into the sink
        /// </summary>
        /// <param name="category">The application specific category of the log entry.</param>
        /// <param name="title">The title for the log entry. This is usually the 1st thing that 
        /// will be seen when entries are reviewed.</param>
        /// <param name="message">The full text of the message sent to the logging provider.</param>
        /// <param name="severity">An indication of how critical an event the entry represents.</param>
        /// <param name="entryType">The type of entry as described in the EventLogEntryType enumeration.</param>
        public override void Write(string category, string title, string message, int severity, EventLogEntryType entryType)
        {
            Console.WriteLine("Logger: {0} ({1}/{3}) - {2}\r\n", title, category, message, severity);
        }
    }
}
