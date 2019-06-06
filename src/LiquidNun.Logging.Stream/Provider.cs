using System;
using LiquidNun.Entities;

namespace LiquidNun.Logging.Stream
{
    /// <summary>
    /// Logs the specified events to the Stream provided in the constructor
    /// </summary>
    public class Provider : LoggingProviderBase
    {
        private readonly System.IO.StreamWriter _writer;

        public Provider(System.IO.Stream stream)
        {
            _writer = new System.IO.StreamWriter(stream);
            _writer.AutoFlush = true;
        }

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
            _writer.WriteLine("Logger: {0} ({1}/{3}) - {2}\r\n", title, category, message, severity);
        }
    }
}
