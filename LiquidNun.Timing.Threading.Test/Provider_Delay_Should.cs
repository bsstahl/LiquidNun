using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelperExtensions;
using System.Collections.Generic;

namespace LiquidNun.Timing.Threading.Test
{
    [TestClass]
    public class Provider_Delay_Should
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void ReturnAfterTheCorrectDelayIfADelayTimespanIsSpecified()
        {
            // This is only a sanity-test.  I don't need to check
            // that Microsoft's Delay API works properly, I just need
            // to make sure that I called it properly so that I
            // am getting reasonable results.

            const double tolerancePercent = 0.05;

            var target = new Timing.Threading.Provider();
            var delayInMs = 100.GetRandom(50);
            var delayTimespan = TimeSpan.FromMilliseconds(delayInMs);
            var delayToleranceMs = (Convert.ToDouble(delayInMs) * tolerancePercent);
            var minDelayMs = Math.Floor(Convert.ToDouble(delayInMs) - delayToleranceMs);
            var maxDelayMs = Math.Ceiling(Convert.ToDouble(delayInMs) + delayToleranceMs);

            var timer = new System.Diagnostics.Stopwatch();
            timer.Start();
            target.Delay(delayTimespan);
            timer.Stop();

            var actualDelayMs = Convert.ToDouble(timer.ElapsedMilliseconds);

            TestContext.WriteLine("Delay - Min Allowed: {0}  Max Allowed: {1}  Actual: {2}", minDelayMs, maxDelayMs, actualDelayMs);
            Assert.IsTrue(actualDelayMs <= maxDelayMs);
            Assert.IsTrue(actualDelayMs >= minDelayMs);
        }

        [TestMethod]
        public void HaveTheCorrectMedianDelayIfADelayTimespanIsSpecified()
        {
            // This is only a sanity-test.  I don't need to check
            // that Microsoft's Delay API works properly, I just need
            // to make sure that I called it properly so that I
            // am getting reasonable results.

            const double delayToleranceMs = 2.0;
            const int executionCount = 100;

            var target = new Timing.Threading.Provider();
            var delayInMs = (10.0).GetRandom(5.0);
            var delayTimespan = TimeSpan.FromMilliseconds(delayInMs);
            var minDelayMs = delayInMs - delayToleranceMs;
            var maxDelayMs = delayInMs + delayToleranceMs;

            var results = new List<long>();

            for (int i = 0; i < executionCount; i++)
            {
                var timer = new System.Diagnostics.Stopwatch();
                timer.Start();
                target.Delay(delayTimespan);
                timer.Stop();
                results.Add(timer.ElapsedMilliseconds);
            }

            var actualDelayMs = results.Median();

            TestContext.WriteLine("Delay - Min Allowed: {0}  Max Allowed: {1}  Actual: {2}", minDelayMs, maxDelayMs, actualDelayMs);
            Assert.IsTrue(actualDelayMs <= maxDelayMs);
            Assert.IsTrue(actualDelayMs >= minDelayMs);
        }

        [TestMethod]
        public void HaveAnAcceptableRangeOfDelaysIfADelayTimespanIsSpecified()
        {
            // This is only a sanity-test.  I don't need to check
            // that Microsoft's Delay API works properly, I just need
            // to make sure that I called it properly so that I
            // am getting reasonable results.

            const double maxDelayRange = 4.0;
            const int executionCount = 100;

            var target = new Timing.Threading.Provider();
            var delayInMs = 10.GetRandom(5);
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

            var actualDelayMs = results.Range();

            TestContext.WriteLine("Delay - Delay Ms: {0}  Range of Delays: {1}  Max allowed Range: {2}", delayInMs, actualDelayMs, maxDelayRange);
            Assert.IsTrue(actualDelayMs <= maxDelayRange);
        }
    }
}
