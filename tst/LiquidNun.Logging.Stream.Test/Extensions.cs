using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace LiquidNun.Logging.Stream.Test
{
    public static class Extensions
    {
        public static void Verify(this System.IO.Stream stream, Func<string, bool> test, string expectedText)
        {
            stream.Position = 0;

            var reader = new StreamReader(stream);
            var s = reader.ReadToEnd();

            string errorMessage = $"Expected: '{expectedText}'\r\nActual: '{s}'";
            Assert.True(test.Invoke(s), errorMessage);
        }
    }
}
