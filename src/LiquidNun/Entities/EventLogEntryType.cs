using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidNun.Entities
{
    /// <summary>
    /// Enumerates the types of Event Log Entries
    /// available to the logging system.
    /// </summary>
    public enum EventLogEntryType
    {
        /// <summary>
        /// Identifies a log entry as informational only.
        /// </summary>
        Information,

        /// <summary>
        /// Identifies a log entry as resulting from a warning event.
        /// </summary>
        Warning,

        /// <summary>
        /// Identifies a log entry as resulting from an application error event.
        /// </summary>
        Error,

        /// <summary>
        /// Identifies a log entry as resulting from an application crash event.
        /// </summary>
        Crash
    }
}
