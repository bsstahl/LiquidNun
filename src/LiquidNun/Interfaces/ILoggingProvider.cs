using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiquidNun.Entities;

namespace LiquidNun.Interfaces
{
    /// <summary>
    /// Describes the interface of a service that accepts log entry information,
    /// formats the information as needed for the particular implementation, and provides 
    /// that data to the appropriate store.
    /// </summary>
    public interface ILoggingProvider
    {
        /// <summary>
        /// Writes the log entry to the underlying data store
        /// </summary>
        /// <param name="category">The application specific category of the log entry.</param>
        /// <param name="title">The title for the log entry. This is usually the 1st thing that 
        /// will be seen when entries are reviewed.</param>
        /// <param name="message">The full text of the message sent to the logging provider.</param>
        /// <param name="severity">An indication of how critical an event the entry represents.</param>
        /// <param name="entryType">The type of entry as described in the EventLogEntryType enumeration.</param>
        void Write(string category, string title, string message, int severity, EventLogEntryType entryType);

        /// <summary>
        /// Writes a log entry for an informational event
        /// </summary>
        /// <param name="category">The application specific category of the log entry.</param>
        /// <param name="title">The title for the log entry. This is usually the 1st thing that 
        /// will be seen when entries are reviewed.</param>
        /// <param name="message">The full text of the message sent to the logging provider.</param>
        void LogInformation(string category, string title, string message);

        /// <summary>
        /// Writes a log entry for a warning event
        /// </summary>
        /// <param name="category">The application specific category of the log entry.</param>
        /// <param name="title">The title for the log entry. This is usually the 1st thing that 
        /// will be seen when entries are reviewed.</param>
        /// <param name="message">The full text of the message sent to the logging provider.</param>
        void LogWarning(string category, string title, string message);

        /// <summary>
        /// Writes a log entry for an error event
        /// </summary>
        /// <param name="category">The application specific category of the log entry.</param>
        /// <param name="title">The title for the log entry. This is usually the 1st thing that 
        /// will be seen when entries are reviewed.</param>
        /// <param name="message">The full text of the message sent to the logging provider.</param>
        void LogError(string category, string title, string message);

        /// <summary>
        /// Writes a log entry for a crash event
        /// </summary>
        /// <param name="category">The application specific category of the log entry.</param>
        /// <param name="title">The title for the log entry. This is usually the 1st thing that 
        /// will be seen when entries are reviewed.</param>
        /// <param name="message">The full text of the message sent to the logging provider.</param>
        void LogCrash(string category, string title, string message);

        /// <summary>
        /// Writes a log entry for an informational event
        /// </summary>
        /// <param name="category">The application specific category of the log entry.</param>
        /// <param name="title">The title for the log entry. This is usually the 1st thing that 
        /// will be seen when entries are reviewed.</param>
        /// <param name="messageFormat">The formatting template for the text of the message.</param>
        /// <param name="messageParameters">The parameters to be applied to the messageFormat template.</param>
        void LogInformation(string category, string title, string messageFormat, params object[] messageParameters);

        /// <summary>
        /// Writes a log entry for a warning event
        /// </summary>
        /// <param name="category">The application specific category of the log entry.</param>
        /// <param name="title">The title for the log entry. This is usually the 1st thing that 
        /// will be seen when entries are reviewed.</param>
        /// <param name="messageFormat">The formatting template for the text of the message.</param>
        /// <param name="messageParameters">The parameters to be applied to the messageFormat template.</param>
        void LogWarning(string category, string title, string messageFormat, params object[] messageParameters);

        /// <summary>
        /// Writes a log entry for an error event
        /// </summary>
        /// <param name="category">The application specific category of the log entry.</param>
        /// <param name="title">The title for the log entry. This is usually the 1st thing that 
        /// will be seen when entries are reviewed.</param>
        /// <param name="messageFormat">The formatting template for the text of the message.</param>
        /// <param name="messageParameters">The parameters to be applied to the messageFormat template.</param>
        void LogError(string category, string title, string messageFormat, params object[] messageParameters);

        /// <summary>
        /// Writes a log entry for a crash event
        /// </summary>
        /// <param name="category">The application specific category of the log entry.</param>
        /// <param name="title">The title for the log entry. This is usually the 1st thing that 
        /// will be seen when entries are reviewed.</param>
        /// <param name="messageFormat">The formatting template for the text of the message.</param>
        /// <param name="messageParameters">The parameters to be applied to the messageFormat template.</param>
        void LogCrash(string category, string title, string messageFormat, params object[] messageParameters);
    }
}
