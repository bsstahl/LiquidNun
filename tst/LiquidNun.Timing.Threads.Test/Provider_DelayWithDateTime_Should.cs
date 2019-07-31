using System;
using TestHelperExtensions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LiquidNun.Timing.Threads.Test
{
    public class Provider_DelayWithDateTime_Should
    {
        [Fact]
        public void AlwaysDelayAtLeastUntilTheSpecifiedTime()
        {
            const int executionCount = 100;

            var maxDelayValue = 25;
            var minDelayValue = 15;

            var delayInMs = maxDelayValue.GetRandom(minDelayValue);
            var delayTicks = TimeSpan.FromMilliseconds(delayInMs).Ticks;

            Int64 currentTicks = 0;
            var results = new List<bool>();
            var target = new Threads.Provider();
            for (int i = 0; i < executionCount; i++)
            {
                var delayDateTime = DateTime.Now.AddTicks(delayTicks);
                target.Delay(delayDateTime);
                currentTicks = DateTime.Now.Ticks;
                results.Add(currentTicks >= delayDateTime.Ticks);
            }

            Assert.DoesNotContain(results, m => !m);
        }

        [Fact]
        public void AlwaysDelayAtLeastUntilTheSpecifiedTimeForLongerDelays()
        {
            const int executionCount = 5;

            var target = new Threads.Provider();
            var maxDelayValue = 500;
            var minDelayValue = 300;

            var delayInMs = maxDelayValue.GetRandom(minDelayValue);
            var delayTicks = TimeSpan.FromMilliseconds(delayInMs).Ticks;

            var results = new List<bool>();

            for (int i = 0; i < executionCount; i++)
            {
                var delayDateTime = DateTime.Now.AddTicks(delayTicks);
                target.Delay(delayDateTime);
                results.Add(DateTime.Now.Ticks >= delayDateTime.Ticks);
            }

            Assert.DoesNotContain(results, m => !m);
        }

        [Fact]
        public void NeverDelayLongerThanTheTolerance()
        {
            // This is only a sanity-test.  I don't need to check
            // that Microsoft's Delay API works properly, I just need
            // to make sure that I called it properly so that I
            // am getting reasonable results.

            const int executionCount = 50;
            const double tolerance = 0.07;  // 7% tolerance at the high-end

            var target = new Threads.Provider();
            var maxDelayValue = 50;
            var minDelayValue = 30;

            var delayInMs = maxDelayValue.GetRandom(minDelayValue);
            var delayTicks = TimeSpan.FromMilliseconds(delayInMs).Ticks;
            var delayToleranceTicks = Convert.ToInt64(Convert.ToDouble(delayTicks) * tolerance);

            var results = new List<bool>();
            Int64 currentTicks = 0;

            for (int i = 0; i < executionCount; i++)
            {
                var delayDateTime = DateTime.Now.AddTicks(delayTicks);
                target.Delay(delayDateTime);
                currentTicks = DateTime.Now.Ticks;
                results.Add((delayDateTime.Ticks + delayToleranceTicks) >= currentTicks);
            }

            Assert.DoesNotContain(results, m => !m);
        }
    }
}
