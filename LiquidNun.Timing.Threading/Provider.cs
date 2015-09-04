using LiquidNun.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiquidNun.Timing.Threading
{
    /// <summary>
    /// Implements a delay service using the System.Threading API
    /// in the Microsoft .NET Framework.
    /// </summary>
    public class Provider : ITimingProvider
    {
        /// <summary>
        /// Delays until the specified absolute time (time with timezone offset).
        /// </summary>
        /// <param name="until">The time, with timezone information, at which the method should return.</param>
        public void Delay(DateTimeOffset until)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delays until the specified UTC time.
        /// </summary>
        /// <param name="untilUtc">The UTC time at which the method should return.</param>
        public void Delay(DateTime untilUtc)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delays for the specified amount of time.
        /// </summary>
        /// <param name="length">The length of time after which the method should return.</param>
        public void Delay(TimeSpan length)
        {
            System.Threading.Thread.Sleep(length);
        }
    }
}
