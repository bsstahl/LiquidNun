using System;
using System.Collections.Generic;
using System.Text;
using TestHelperExtensions;
using Xunit;

namespace LiquidNun.Directory.FileSystem.Test
{
    public class Provider_ReadAllText_Should
    {
        [Fact]
        public void ThrowFileNotFoundIfTheFileDoesNotExist()
        {
            string filePath = $"{string.Empty.GetRandom()}\\{string.Empty.GetRandom()}.txt";
            var target = new Provider();
            Assert.Throws<Exceptions.FileNotFoundException>(() => target.ReadAllText(filePath));
        }

        [Fact]
        public void ReturnTheTextFromTheFile()
        {
            var target = new Provider();
            var actual = target.ReadAllText(Constants.TextFilePath);
            Assert.Equal(Constants.TextFileContents, actual);
        }
    }
}
