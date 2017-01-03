using LiquidNun.Enumerations;

namespace LiquidNun.Interfaces
{
    /// <summary>
    /// Describes the interface of a message exchange service that guarantees the delivery of messages
    /// </summary>
    public interface IReliableMessaging
    {
        /// <summary>
        /// Returns a value indicating if the specified message sink
        /// or source supports transactions
        /// </summary>
        /// <param name="name">The name of the message sink or message source</param>
        /// <returns>A boolean true if the item supports transactions, false if it does not</returns>
        bool IsTransactable(string name);

        /// <summary>
        /// Returns a value indicating the default priority of the message sink
        /// </summary>
        /// <param name="sinkName">The name of the message sink</param>
        /// <returns>An enumeration representing the default priority of the message sink 
        /// If the sink does not support prioritization, Priority.Normal will be returned</returns>
        Priority DefaultPriority(string sinkName);

        /// <summary>
        /// Sends the provided message to the specified message sink
        /// </summary>
        /// <param name="sinkName">The name of the message sink</param>
        /// <param name="message">The message to be sent</param>
        void PutMessage(string sinkName, string message);

        /// <summary>
        /// Sends the provided message to the specified message sink with the specified priority
        /// </summary>
        /// <param name="sinkName">The name of the message sink</param>
        /// <param name="message">The message to be sent</param>
        /// <param name="priority">An enumeration representing the priority of the message relative to other messages</param>
        void PutMessage(string sinkName, string message, Priority priority);

        /// <summary>
        /// Sends the provided message to the specified message sink using the provided transaction
        /// </summary>
        /// <param name="transaction">A transaction that the message sink is currently participating in</param>
        /// <param name="sinkName">The name of the message sink</param>
        /// <param name="message">The message to be sent</param>
        void PutMessage(IReliableMessagingTransaction transaction, string sinkName, string message);

        /// <summary>
        /// Sends the provided message to the specified message sink using the provided transaction and priority
        /// </summary>
        /// <param name="transaction">A transaction that the message sink is currently participating in</param>
        /// <param name="sinkName">The name of the message sink</param>
        /// <param name="message">The message to be sent</param>
        /// <param name="priority">An enumeration representing the priority of the message relative to other messages</param>
        void PutMessage(IReliableMessagingTransaction transaction, string sinkName, string message, Priority priority);

        /// <summary>
        /// Retrieves a message from the specified message source
        /// </summary>
        /// <param name="sourceName">The name of the message source</param>
        string GetMessage(string sourceName);

        /// <summary>
        /// Retrieves a message from the specified message source using the provided transaction
        /// </summary>
        /// <param name="transaction">A transaction that the message source is currently participating in</param>
        /// <param name="sourceName">The name of the message source</param>
        string GetMessage(IReliableMessagingTransaction transaction, string sourceName);


        /// <summary>
        /// Opens a message source for transacted reads
        /// </summary>
        /// <param name="sourceName">The name of the message source</param>
        IReliableMessagingTransaction BeginReadTransaction(string sourceName);

        /// <summary>
        /// Opens a message sink for transacted writes
        /// </summary>
        /// <param name="sinkName">The name of the message sink</param>
        IReliableMessagingTransaction BeginWriteTransaction(string sinkName);

        /// <summary>
        /// Returns the local depth value of the specified sink or source.
        /// </summary>
        /// <param name="name">The name of the message sink or message source</param>
        /// <remarks>If messages are not stored locally, the depth value will always be 0</remarks>
        long GetDepth(string name);

    }
}
