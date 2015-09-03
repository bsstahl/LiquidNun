using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelperExtensions;

namespace LiquidNun.Logging.TestContext.Test
{
    [TestClass]
    public class Provider_Write_Should
    {
        public Microsoft.VisualStudio.TestTools.UnitTesting.TestContext TestContext { get; set; }

        [TestMethod]
        public void CallWriteLineOnTheTestContextExactlyOnce()
        {
            var mocks = new Rhino.Mocks.MockRepository();
            var testContext = mocks.StrictMock<Microsoft.VisualStudio.TestTools.UnitTesting.TestContext>();

            testContext.WriteLine(string.Empty, null);
            Rhino.Mocks.LastCall.IgnoreArguments().Repeat.Once();

            mocks.ReplayAll();

            string message = string.Empty.GetRandom();
            var target = new LiquidNun.Logging.TestContext.Provider(testContext);
            target.LogInformation("Category", "Title", message);

            mocks.VerifyAll();
        }

        [TestMethod]
        public void IncludeTheMessageInWhatIsWritten()
        {
            var mocks = new Rhino.Mocks.MockRepository();
            var tc = mocks.StrictMock<Microsoft.VisualStudio.TestTools.UnitTesting.TestContext>();
            string message = string.Empty.GetRandom();

            tc.WriteLine(string.Empty, null);
            Rhino.Mocks.LastCall.IgnoreArguments()
                .Constraints(
                    Rhino.Mocks.Constraints.Is.Anything(), 
                    Rhino.Mocks.Constraints.List.IsIn(message))
                .Repeat.Once();

            mocks.ReplayAll();

            var target = new LiquidNun.Logging.TestContext.Provider(tc);
            target.LogInformation("Category", "Title", message);

            mocks.VerifyAll();
        }

    }
}
