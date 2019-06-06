using System;
using System.Collections.Generic;
using System.Text;
using TestHelperExtensions;
using Xunit;

namespace LiquidNun.Directory.FileSystem.Test
{
    public class Provider_Exists_Should
    {
        [Fact]
        public void ReturnFalseIfThePathDoesNotExist()
        {
            string filePath = $"{string.Empty.GetRandom()}\\{string.Empty.GetRandom()}.txt";
            var target = new Provider();
            Assert.False(target.Exists(filePath));
        }

        [Fact]
        public void ReturnTrueIfTheFileNotExists()
        {
            string filePath = System.IO.Path.GetFullPath(".");
            var target = new Provider();
            Assert.True(target.Exists(filePath));
        }
    }
}
