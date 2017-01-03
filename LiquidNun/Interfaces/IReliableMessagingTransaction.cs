using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidNun.Interfaces
{
    /// <summary>
    /// Describes the interface of an object that represents a transaction 
    /// within a reliable messaging subsystem.
    /// </summary>
    public interface IReliableMessagingTransaction
    {
        /// <summary>
        /// The transaction Id
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Indicates if the transaction is currently active
        /// </summary>
        bool Active { get; }

        /// <summary>
        /// A property bag holding information about the transaction
        /// </summary>
        System.Collections.Generic.Dictionary<string, object> PropertyBag { get; }

        /// <summary>
        /// Instructs the system to commit the actions within the transaction
        /// </summary>
        void Commit();

        /// <summary>
        /// Instructs the system to rollback any changes made within the transaction
        /// </summary>
        void Rollback();

        /// <summary>
        /// Adds an additional sink to the transaction
        /// </summary>
        /// <param name="sinkName"></param>
        void AddWriteTransaction(string sinkName);

        /// <summary>
        /// Adds an additional source to the transaction
        /// </summary>
        /// <param name="sourceName"></param>
        void AddReadTransaction(string sourceName);

    }
}
