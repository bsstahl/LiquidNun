using System;
using System.Collections.Generic;
using System.Text;
using TestHelperExtensions;
using Xunit;

namespace LiquidNun.Directory.FileSystem.Test
{
    public class Provider_ReadAllText_Should
    {
        const string _textFilePath = @"TestFile.txt";
        const string _textFileContents = "e4c5112e-3011-4b5f-b957-6294a43abf59";

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
            var actual = target.ReadAllText(_textFilePath);
            Assert.Equal(_textFileContents, actual);
        }
    }
}
