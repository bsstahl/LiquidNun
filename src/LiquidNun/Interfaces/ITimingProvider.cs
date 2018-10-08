using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidNun.Interfaces
{
    /// <summary>
    /// Describes the interface of a service that accepts times and time spans in
    /// various forms and synchronously returns at the appropriate time, thus
    /// providing a "delay service".
    /// </summary>
    public interface ITimingProvider
    {
        /// <summary>
        /// Delays for the specified amount of time.
        /// </summary>
        /// <param name="length">The length of time after which the method should return.</param>
        void Delay(TimeSpan length);

        /// <summary>
        /// Delays until the specified UTC time.
        /// </summary>
        /// <param name="untilUtc">The UTC time at which the method should return.</param>
        void Delay(DateTime untilUtc);

        /// <summary>
        /// Delays until the specified absolute time (time with timezone offset).
        /// </summary>
        /// <param name="until">The time, with timezone information, at which the method should return.</param>
        void Delay(DateTimeOffset until);
    }
}
