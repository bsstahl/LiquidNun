using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidNun.Exceptions
{
    /// <summary>
    /// Represents an error that indicates that the specified folder
    /// could not be found within the directory system.
    /// </summary>
    public class DirectoryNotFoundException : System.Exception
    {
        /// <summary>
        /// The path that was specified for the directory
        /// </summary>
        public string PathName { get; set; }

        /// <summary>
        /// The default constructor of the exception
        /// </summary>
        public DirectoryNotFoundException() : base() { }

        /// <summary>
        /// The primary (preferred) constructor of the exception
        /// </summary>
        /// <param name="pathName">The path that was specified for the directory</param>
        public DirectoryNotFoundException(string pathName)
            : base(string.Format(System.Globalization.CultureInfo.CurrentCulture, "Directory '{0}', not found.", pathName))
        {
            this.PathName = pathName;
        }

        /// <summary>
        /// A standard implementation for an exception constructor
        /// </summary>
        /// <param name="message">The description of the error</param>
        /// <param name="innerException">The error that caused this error</param>
        public DirectoryNotFoundException(string message, Exception innerException) : base(message, innerException) { }

    }

}
