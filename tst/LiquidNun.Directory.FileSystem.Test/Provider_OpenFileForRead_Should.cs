using System;
using System.Collections.Generic;
using System.Text;
using TestHelperExtensions;
using Xunit;

namespace LiquidNun.Directory.FileSystem.Test
{
    public class Provider_OpenFileForRead_Should
    {
        [Fact]
        public void ThrowFileNotFoundIfTheFileDoesNotExist()
        {
            string filePath = $"{string.Empty.GetRandom()}.txt";
            var target = new Provider();
            Assert.Throws<Exceptions.FileNotFoundException>(() => target.OpenFileForRead(filePath));
        }

        [Fact]
        public void ReturnAnInstanceOfAnIFileReaderIfTheFileExists()
        {
            var target = new Provider();
            var actual = target.OpenFileForRead(Constants.TextFilePath);
            Assert.NotNull(actual);
        }
    }
}
