using System;
using LiquidNun.Entities;

namespace LiquidNun.Logging.TestContext
{
    /// <summary>
    /// Logs the specified events to the Test Context
    /// </summary>
    public class Provider : LoggingProviderBase
    {
        internal Microsoft.VisualStudio.TestTools.UnitTesting.TestContext _testContext { get; set; }

        /// <summary>
        /// Creates an instance of the TestContext Logging Provider using the TestContext
        /// instance passed into the method.
        /// </summary>
        /// <param name="testContext">The instance of a TestContext object to be logged to.</param>
        public Provider(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            _testContext = testContext;
        }

        /// <summary>
        /// Writes the log entry into the Test Context
        /// </summary>
        /// <param name="category">The application specific category of the log entry.</param>
        /// <param name="title">The title for the log entry. This is usually the 1st thing that 
        /// will be seen when entries are reviewed.</param>
        /// <param name="message">The full text of the message sent to the logging provider.</param>
        /// <param name="severity">An indication of how critical an event the entry represents.</param>
        /// <param name="entryType">The type of entry as described in the EventLogEntryType enumeration.</param>
        public override void Write(string category, string title, string message, int severity, EventLogEntryType entryType)
        {
            throw new NotImplementedException();
        }
    }
}
