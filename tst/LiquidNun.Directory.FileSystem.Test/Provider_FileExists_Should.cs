using System;
using System.Collections.Generic;
using System.Text;
using TestHelperExtensions;
using Xunit;

namespace LiquidNun.Directory.FileSystem.Test
{
    public class Provider_FileExists_Should
    {
        [Fact]
        public void ReturnFalseIfTheFileDoesNotExist()
        {
            string filePath = $".\\{string.Empty.GetRandom()}.txt";
            var target = new Provider();
            Assert.False(target.FileExists(filePath));
        }

        [Fact]
        public void ReturnFalseIfThePathDoesNotExist()
        {
            string filePath = $"{string.Empty.GetRandom()}\\{string.Empty.GetRandom()}.txt";
            var target = new Provider();
            Assert.False(target.FileExists(filePath));
        }

        [Fact]
        public void ReturnTrueIfTheFileNotExists()
        {
            var target = new Provider();
            Assert.True(target.FileExists(Constants.TextFilePath));
        }
    }
}
