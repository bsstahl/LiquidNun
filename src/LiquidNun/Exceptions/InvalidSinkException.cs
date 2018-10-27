using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidNun.Exceptions
{
    /// <summary>
    /// Indicates that the logging sink provided
    /// was not valid for the logging provider type
    /// </summary>
    public class InvalidSinkException: Exception
    {
        /// <summary>
        /// Initializes a new instance of the Exception class
        /// </summary>
        public InvalidSinkException():base() { }

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message
        /// </summary>
        /// <param name="message">The error message to be passed</param>
        public InvalidSinkException(string message):base(message) { }

        /// <summary>
        /// Initializes a new instance of the Exception class with 
        /// a specified error message and an inner exception
        /// </summary>
        /// <param name="message">The error message to be passed</param>
        /// <param name="innerException">The exception that resulted in the current exception being thrown</param>
        public InvalidSinkException(string message, Exception innerException) : base(message, innerException) { }
    }
}
