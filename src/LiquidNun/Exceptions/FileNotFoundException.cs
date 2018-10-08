using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidNun.Exceptions
{
    /// <summary>
    /// Represents an error that indicates that the specified file
    /// could not be found within the directory system.
    /// </summary>
    public class FileNotFoundException : Exception
    {
        /// <summary>
        /// The path that was specified for the file
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// The default constructor of the exception
        /// </summary>
        public FileNotFoundException() : base() { }

        /// <summary>
        /// The primary (preferred) constructor of the exception
        /// </summary>
        /// <param name="filePath">The path that was specified for the directory</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])")]
        public FileNotFoundException(string filePath)
            : base($"File '{filePath}', not found.")
        {
            this.FilePath = filePath;
        }

        /// <summary>
        /// A standard implementation for an exception constructor
        /// </summary>
        /// <param name="message">The description of the error</param>
        /// <param name="innerException">The error that caused this error</param>
        public FileNotFoundException(string message, Exception innerException) : base(message, innerException) { }

    }
}