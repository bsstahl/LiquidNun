using System;
using TestHelperExtensions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LiquidNun.Timing.Threads.Test
{
    public class Provider_DelayWithTicks_Should
    {
        [Fact]
        public void AlwaysDelayAtLeastUntilTheSpecifiedTime()
        {
            const int executionCount = 100;

            var target = new Threads.Provider();
            var maxDelayValue = 25;
            var minDelayValue = 15;

            var delayInMs = maxDelayValue.GetRandom(minDelayValue);
            var delayTicks = TimeSpan.FromMilliseconds(delayInMs).Ticks;

            var results = new List<bool>();

            for (int i = 0; i < executionCount; i++)
            {
                var endTicks = DateTime.Now.Ticks + delayTicks;
                target.Delay(endTicks);
                results.Add(DateTime.Now.Ticks >= endTicks);
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
                var endTicks = DateTime.Now.Ticks + delayTicks;
                target.Delay(endTicks);
                results.Add(DateTime.Now.Ticks >= endTicks);
            }

            Assert.DoesNotContain(results, m => !m);
        }

        [Fact]
        public void NeverDelayLongerThanTheToleranceIfADelayTimespanIsSpecified()
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
                var endTicks = DateTime.Now.Ticks + delayTicks;
                target.Delay(endTicks);
                currentTicks = DateTimeOffset.Now.Ticks;
                results.Add((endTicks + delayToleranceTicks) >= currentTicks);
            }

            Assert.DoesNotContain(results, m => !m);
        }
    }
}
