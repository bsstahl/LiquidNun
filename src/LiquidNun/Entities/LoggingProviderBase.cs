using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiquidNun.Interfaces;

namespace LiquidNun.Entities
{
    /// <summary>
    /// An abstract base class used to help implement logging providers.
    /// </summary>
    public abstract class LoggingProviderBase : ILoggingProvider
    {
        /// <summary>
        /// A method that needs to be exposed by the provider to write the log entry to the underlying data store.
        /// </summary>
        /// <param name="category">The application specific category of the log entry.</param>
        /// <param name="title">The title for the log entry. This is usually the 1st thing that 
        /// will be seen when entries are reviewed.</param>
        /// <param name="message">The full text of the message sent to the logging provider.</param>
        /// <param name="severity">An indication of how critical an event the entry represents.</param>
        /// <param name="entryType">The type of entry as described in the EventLogEntryType enumeration.</param>
        public abstract void Write(string category, string title, string message, int severity, EventLogEntryType entryType);

        /// <summary>
        /// Calls the write method with the appropriate parameters to create a log entry representing a crash event.
        /// </summary>
        /// <param name="category">The application specific category of the log entry.</param>
        /// <param name="title">The title for the log entry. This is usually the 1st thing that 
        /// will be seen when entries are reviewed.</param>
        /// <param name="messageFormat">The formatting template for the text of the message.</param>
        /// <param name="messageParameters">The parameters to be applied to the messageFormat template.</param>
        public void LogCrash(string category, string title, string messageFormat, params object[] messageParameters)
        {
            this.LogCrash(category, title, string.Format(System.Globalization.CultureInfo.CurrentCulture, messageFormat, messageParameters));
        }

        /// <summary>
        /// Calls the write method with the appropriate parameters to create a log entry representing a crash event.
        /// </summary>
        /// <param name="category">The application specific category of the log entry.</param>
        /// <param name="title">The title for the log entry. This is usually the 1st thing that 
        /// will be seen when entries are reviewed.</param>
        /// <param name="message">The full text of the message sent to the logging provider.</param>
        public void LogCrash(string category, string title, string message)
        {
            this.Write(category, title, message, 0, EventLogEntryType.Error);
        }

        /// <summary>
        /// Calls the write method with the appropriate parameters to create a log entry representing a error event.
        /// </summary>
        /// <param name="category">The application specific category of the log entry.</param>
        /// <param name="title">The title for the log entry. This is usually the 1st thing that 
        /// will be seen when entries are reviewed.</param>
        /// <param name="messageFormat">The formatting template for the text of the message.</param>
        /// <param name="messageParameters">The parameters to be applied to the messageFormat template.</param>
        public void LogError(string category, string title, string messageFormat, params object[] messageParameters)
        {
            this.LogError(category, title, string.Format(System.Globalization.CultureInfo.CurrentCulture, messageFormat, messageParameters));
        }

        /// <summary>
        /// Calls the write method with the appropriate parameters to create a log entry representing a error event.
        /// </summary>
        /// <param name="category">The application specific category of the log entry.</param>
        /// <param name="title">The title for the log entry. This is usually the 1st thing that 
        /// will be seen when entries are reviewed.</param>
        /// <param name="message">The full text of the message sent to the logging provider.</param>
        public void LogError(string category, string title, string message)
        {
            this.Write(category, title, message, 2, EventLogEntryType.Error);
        }

        /// <summary>
        /// Calls the write method with the appropriate parameters to create a log entry representing a informational event.
        /// </summary>
        /// <param name="category">The application specific category of the log entry.</param>
        /// <param name="title">The title for the log entry. This is usually the 1st thing that 
        /// will be seen when entries are reviewed.</param>
        /// <param name="messageFormat">The formatting template for the text of the message.</param>
        /// <param name="messageParameters">The parameters to be applied to the messageFormat template.</param>
        public void LogInformation(string category, string title, string messageFormat, params object[] messageParameters)
        {
            this.LogInformation(category, title, string.Format(System.Globalization.CultureInfo.CurrentCulture, messageFormat, messageParameters));
        }

        /// <summary>
        /// Calls the write method with the appropriate parameters to create a log entry representing a informational event.
        /// </summary>
        /// <param name="category">The application specific category of the log entry.</param>
        /// <param name="title">The title for the log entry. This is usually the 1st thing that 
        /// will be seen when entries are reviewed.</param>
        /// <param name="message">The full text of the message sent to the logging provider.</param>
        public void LogInformation(string category, string title, string message)
        {
            this.Write(category, title, message, 5, EventLogEntryType.Information);
        }

        /// <summary>
        /// Calls the write method with the appropriate parameters to create a log entry representing a warning event.
        /// </summary>
        /// <param name="category">The application specific category of the log entry.</param>
        /// <param name="title">The title for the log entry. This is usually the 1st thing that 
        /// will be seen when entries are reviewed.</param>
        /// <param name="messageFormat">The formatting template for the text of the message.</param>
        /// <param name="messageParameters">The parameters to be applied to the messageFormat template.</param>
        public void LogWarning(string category, string title, string messageFormat, params object[] messageParameters)
        {
            this.LogWarning(category, title, string.Format(System.Globalization.CultureInfo.CurrentCulture, messageFormat, messageParameters));
        }

        /// <summary>
        /// Calls the write method with the appropriate parameters to create a log entry representing a warning event.
        /// </summary>
        /// <param name="category">The application specific category of the log entry.</param>
        /// <param name="title">The title for the log entry. This is usually the 1st thing that 
        /// will be seen when entries are reviewed.</param>
        /// <param name="message">The full text of the message sent to the logging provider.</param>
        public void LogWarning(string category, string title, string message)
        {
            this.Write(category, title, message, 3, EventLogEntryType.Warning);
        }

    }
}