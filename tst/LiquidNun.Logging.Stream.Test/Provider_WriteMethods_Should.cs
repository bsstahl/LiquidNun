using System;
using Xunit;
using Xunit.Abstractions;
using TestHelperExtensions;
using LiquidNun.Logging.Stream;

namespace LiquidNun.Logging.Stream.Test
{
    public class Provider_WriteMethods_Should
    {
        [Fact]
        public void LogACrashIfLogCrashIsCalled()
        {
            string expected = "/0";
            var stream = new System.IO.MemoryStream();
            var target = new Provider(stream);

            target.LogCrash(string.Empty.GetRandom(), string.Empty.GetRandom(), string.Empty.GetRandom());

            stream.Verify(h => h.Contains(expected), $"Contain '{expected}'");
        }

        [Fact]
        public void LogAnErrorIfLogErrorIsCalled()
        {
            string expected = "/2";
            var stream = new System.IO.MemoryStream();
            var target = new Provider(stream);

            target.LogError(string.Empty.GetRandom(), string.Empty.GetRandom(), string.Empty.GetRandom());

            stream.Verify(h => h.Contains(expected), $"Contain '{expected}'");
        }

        [Fact]
        public void LogAWarningIfLogWarningIsCalled()
        {
            string expected = "/3";
            var stream = new System.IO.MemoryStream();
            var target = new Provider(stream);

            target.LogWarning(string.Empty.GetRandom(), string.Empty.GetRandom(), string.Empty.GetRandom());

            stream.Verify(h => h.Contains(expected), $"Contain '{expected}'");
        }

        [Fact]
        public void LogInformationIfLogInformationIsCalled()
        {
            string expected = "/5";
            var stream = new System.IO.MemoryStream();
            var target = new Provider(stream);

            target.LogInformation(string.Empty.GetRandom(), string.Empty.GetRandom(), string.Empty.GetRandom());

            stream.Verify(h => h.Contains(expected), $"Contain '{expected}'");
        }

        [Fact]
        public void WriteAMessageIfWriteIsCalled()
        {
            var stream = new System.IO.MemoryStream();
            var target = new Provider(stream);

            target.Write(string.Empty.GetRandom(), string.Empty.GetRandom(), string.Empty.GetRandom(), 10.GetRandom(), (Entities.EventLogEntryType)3.GetRandom());

            stream.Verify(h => !string.IsNullOrEmpty(h), "Not null or empty");
        }

        [Fact]
        public void WriteTheCorrectCategory()
        {
            string expected = string.Empty.GetRandom();

            var stream = new System.IO.MemoryStream();
            var target = new Provider(stream);

            target.Write(expected, string.Empty.GetRandom(), string.Empty.GetRandom(), 10.GetRandom(), (Entities.EventLogEntryType)3.GetRandom());

            stream.Verify(h => h.Contains(expected), $"Contain '{expected}'");
        }

        [Fact]
        public void WriteTheCorrectTitle()
        {
            string expected = string.Empty.GetRandom();

            var stream = new System.IO.MemoryStream();
            var target = new Provider(stream);

            target.Write(string.Empty.GetRandom(), expected, string.Empty.GetRandom(), 10.GetRandom(), (Entities.EventLogEntryType)3.GetRandom());

            stream.Verify(h => h.Contains(expected), $"Contain '{expected}'");
        }

        [Fact]
        public void WriteTheCorrectMessage()
        {
            string expected = string.Empty.GetRandom();

            var stream = new System.IO.MemoryStream();
            var target = new Provider(stream);

            target.Write(string.Empty.GetRandom(), string.Empty.GetRandom(), expected, 10.GetRandom(), (Entities.EventLogEntryType)3.GetRandom());

            stream.Verify(h => h.Contains(expected), $"Contain '{expected}'");
        }

        [Fact]
        public void WriteTheCorrectSeverity()
        {
            int expected = 10.GetRandom();

            var stream = new System.IO.MemoryStream();
            var target = new Provider(stream);

            target.Write(string.Empty.GetRandom(), string.Empty.GetRandom(), string.Empty.GetRandom(), expected, (Entities.EventLogEntryType)3.GetRandom());

            stream.Verify(h => h.Contains(expected.ToString()), $"Contain '{expected}'");
        }
    }
}
