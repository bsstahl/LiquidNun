using System;
using System.Linq;
using TestHelperExtensions;
using Xunit;

namespace LiquidNun.Directory.FileSystem.Test
{
    public class Provider_GetFiles_Should
    {
        [Fact]
        public void ThrowDirectoryNotFoundIfFolderDoesNotExist()
        {
            string filePath = $"{string.Empty.GetRandom()}";
            var target = new Provider();
            Assert.Throws<Exceptions.DirectoryNotFoundException>(() => target.GetFiles(filePath));
        }

        [Fact]
        public void IncludeTheTestFile()
        {
            string filePath = ".";
            var target = new Provider();

            var expected = $".\\{Constants.TextFilePath}";
            var actual = target.GetFiles(filePath);

            Assert.Contains(actual, f => f == expected);
        }

        [Fact]
        public void IncludeMultipleFiles()
        {
            string filePath = ".";
            var target = new Provider();

            var expected = $".\\{Constants.TextFilePath}";
            var actual = target.GetFiles(filePath);

            Assert.True(actual.Count() > 1);
        }
    }
}
