using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelperExtensions;

namespace LiquidNun.Timing.Threading.Test
{
    [TestClass]
    public class Provider_Delay_Should
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void ReturnAfterTheCorrectDelayIfADelayTimespanIsSpecified()
        {
            var target = new LiquidNun.Timing.Threading.Provider();
            var delayInMs = 100.GetRandom(50);
            var delayTimespan = TimeSpan.FromMilliseconds(delayInMs);

            var timer = new System.Diagnostics.Stopwatch();
            timer.Start();
            target.Delay(delayTimespan);
            timer.Stop();

            Assert.AreEqual(delayInMs, timer.ElapsedMilliseconds);
        }
    }
}
