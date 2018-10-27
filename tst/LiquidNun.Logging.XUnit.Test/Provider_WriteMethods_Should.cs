using System;
using Xunit;
using Xunit.Abstractions;
using Moq;
using TestHelperExtensions;

namespace LiquidNun.Logging.XUnit.Test
{
    public class Provider_WriteMethods_Should
    {
        [Fact]
        public void CallTheTestOutputHelperIfLogCrashIsCalled()
        {
            var outputHelper = new Mock<ITestOutputHelper>();

            var target = new LiquidNun.Logging.XUnit.Provider(outputHelper.Object);
            target.LogCrash(string.Empty.GetRandom(), string.Empty.GetRandom(), string.Empty.GetRandom());

            outputHelper.Verify(h => h.WriteLine(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void CallTheTestOutputHelperIfLogErrorIsCalled()
        {
            var outputHelper = new Mock<ITestOutputHelper>();

            var target = new LiquidNun.Logging.XUnit.Provider(outputHelper.Object);
            target.LogError(string.Empty.GetRandom(), string.Empty.GetRandom(), string.Empty.GetRandom());

            outputHelper.Verify(h => h.WriteLine(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void CallTheTestOutputHelperIfLogWarningIsCalled()
        {
            var outputHelper = new Mock<ITestOutputHelper>();

            var target = new LiquidNun.Logging.XUnit.Provider(outputHelper.Object);
            target.LogWarning(string.Empty.GetRandom(), string.Empty.GetRandom(), string.Empty.GetRandom());

            outputHelper.Verify(h => h.WriteLine(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void CallTheTestOutputHelperIfLogInformationIsCalled()
        {
            var outputHelper = new Mock<ITestOutputHelper>();

            var target = new LiquidNun.Logging.XUnit.Provider(outputHelper.Object);
            target.LogInformation(string.Empty.GetRandom(), string.Empty.GetRandom(), string.Empty.GetRandom());

            outputHelper.Verify(h => h.WriteLine(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void CallTheTestOutputHelperIfWriteIsCalled()
        {
            var outputHelper = new Mock<ITestOutputHelper>();

            var target = new LiquidNun.Logging.XUnit.Provider(outputHelper.Object);
            target.Write(string.Empty.GetRandom(), string.Empty.GetRandom(), string.Empty.GetRandom(), 10.GetRandom(), (Entities.EventLogEntryType)3.GetRandom());

            outputHelper.Verify(h => h.WriteLine(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void WriteTheCorrectCategory()
        {
            string expected = string.Empty.GetRandom();
            var outputHelper = new Mock<ITestOutputHelper>();

            var target = new LiquidNun.Logging.XUnit.Provider(outputHelper.Object);
            target.Write(expected, string.Empty.GetRandom(), string.Empty.GetRandom(), 10.GetRandom(), (Entities.EventLogEntryType)3.GetRandom());

            outputHelper.Verify(h => h.WriteLine(It.Is<string>(v => v.Contains(expected))), Times.Once);
        }

        [Fact]
        public void WriteTheCorrectTitle()
        {
            string expected = string.Empty.GetRandom();
            var outputHelper = new Mock<ITestOutputHelper>();

            var target = new LiquidNun.Logging.XUnit.Provider(outputHelper.Object);
            target.Write(string.Empty.GetRandom(), expected, string.Empty.GetRandom(), 10.GetRandom(), (Entities.EventLogEntryType)3.GetRandom());

            outputHelper.Verify(h => h.WriteLine(It.Is<string>(v => v.Contains(expected))), Times.Once);
        }

        [Fact]
        public void WriteTheCorrectMessage()
        {
            string expected = string.Empty.GetRandom();
            var outputHelper = new Mock<ITestOutputHelper>();

            var target = new LiquidNun.Logging.XUnit.Provider(outputHelper.Object);
            target.Write(string.Empty.GetRandom(), string.Empty.GetRandom(), expected, 10.GetRandom(), (Entities.EventLogEntryType)3.GetRandom());

            outputHelper.Verify(h => h.WriteLine(It.Is<string>(v => v.Contains(expected))), Times.Once);
        }

        [Fact]
        public void WriteTheCorrectSeverity()
        {
            int expected = 10.GetRandom();
            var outputHelper = new Mock<ITestOutputHelper>();

            var target = new LiquidNun.Logging.XUnit.Provider(outputHelper.Object);
            target.Write(string.Empty.GetRandom(), string.Empty.GetRandom(), string.Empty.GetRandom(), expected, (Entities.EventLogEntryType)3.GetRandom());

            outputHelper.Verify(h => h.WriteLine(It.Is<string>(v => v.Contains(expected.ToString()))), Times.Once);
        }
    }
}
