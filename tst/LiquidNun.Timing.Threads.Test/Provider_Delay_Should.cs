﻿using System;
using TestHelperExtensions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LiquidNun.Timing.Threads.Test
{
    public class Provider_Delay_Should
    {
        [Fact]
        public void AlwaysDelayAtLeastAsLongAsTheSpecifiedValue()
        {
            const int executionCount = 100;

            var target = new Threads.Provider();
            var maxDelayValue = 25;
            var minDelayValue = 15;

            var delayInMs = maxDelayValue.GetRandom(minDelayValue);
            var delayTimespan = TimeSpan.FromMilliseconds(delayInMs);

            var results = new List<long>();

            for (int i = 0; i < executionCount; i++)
            {
                var timer = new System.Diagnostics.Stopwatch();
                timer.Start();
                target.Delay(delayTimespan);
                timer.Stop();
                results.Add(timer.ElapsedMilliseconds);
            }

            Console.WriteLine($"Specified: {delayTimespan.Milliseconds}  Min Actual: {results.Min(m => m)}");
            Console.WriteLine($"Max Actual: {results.Max(m => m)}    Average: {results.Average()}");

            var actual = results.OrderBy(m => m);
            Assert.DoesNotContain(actual, m => m < delayInMs);
        }

        [Fact]
        public void AlwaysDelayAtLeastAsLongAsTheSpecifiedValueForLongerValues()
        {
            const int executionCount = 5;

            var target = new Threads.Provider();
            var maxDelayValue = 500;
            var minDelayValue = 300;

            var delayInMs = maxDelayValue.GetRandom(minDelayValue);
            var delayTimespan = TimeSpan.FromMilliseconds(delayInMs);

            var results = new List<long>();

            for (int i = 0; i < executionCount; i++)
            {
                var timer = new System.Diagnostics.Stopwatch();
                timer.Start();
                target.Delay(delayTimespan);
                timer.Stop();
                results.Add(timer.ElapsedMilliseconds);
            }

            Console.WriteLine($"Specified: {delayTimespan.Milliseconds}  Min Actual: {results.Min(m => m)}");
            Console.WriteLine($"Max Actual: {results.Max(m => m)}    Average: {results.Average()}");

            var actual = results.OrderBy(m => m);
            Assert.DoesNotContain(actual, m => m < delayInMs);
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
            var maxDelayRange = delayInMs * (tolerance + 1.0);
            var delayTimespan = TimeSpan.FromMilliseconds(delayInMs);

            var results = new List<long>();

            for (int i = 0; i < executionCount; i++)
            {
                var timer = new System.Diagnostics.Stopwatch();
                timer.Start();
                target.Delay(delayTimespan);
                timer.Stop();
                results.Add(timer.ElapsedMilliseconds);
            }

            Console.WriteLine($"Specified: {delayTimespan.Milliseconds}  Max Allowed: {maxDelayRange}  Max Actual: {results.Max(m => m)}");

            var actual = results.OrderByDescending(m => m);
            Assert.DoesNotContain(actual, m => m > maxDelayRange);
        }
    }
}
