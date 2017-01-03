using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidNun.Enumerations
{
    /// <summary>
    /// Represents the importance of an item with the lowest value 
    /// representing the lowest importance.
    /// </summary>
    public enum Priority
    {
        /// <summary>
        /// Represents the minimum possible priority 
        /// </summary>
        Lowest = 0,

        /// <summary>
        /// Represents an extremely low, but not the lowest priority
        /// </summary>
        VeryLow = 1,

        /// <summary>
        /// Represents a below average priority
        /// </summary>
        Low = 2,

        /// <summary>
        /// Represents the standard (default) priority
        /// </summary>
        Normal = 3,

        /// <summary>
        /// Represents a slightly above-average priority
        /// </summary>
        AboveNormal = 4,

        /// <summary>
        /// Represents a high, but not nearly the highest priority
        /// </summary>
        High = 5,

        /// <summary>
        /// Represents an extremely high, but not the highest priority
        /// </summary>
        VeryHigh = 6,

        /// <summary>
        /// Represents the minimum possible priority 
        /// </summary>
        Highest = 7
    }
}
