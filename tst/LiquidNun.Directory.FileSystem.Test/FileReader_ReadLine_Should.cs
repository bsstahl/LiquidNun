using System;
using System.Collections.Generic;
using System.Text;
using TestHelperExtensions;
using Xunit;

namespace LiquidNun.Directory.FileSystem.Test
{
    public class FileReader_ReadLine_Should
    {
        [Fact]
        public void ReturnTheContentsOfASingleLineFile()
        {
            var provider = new Provider();
            var target = provider.OpenFileForRead(Constants.TextFilePath);

            var actual = target.ReadLine();
            target.Close();

            Assert.Equal(Constants.TextFileContents, actual);
        }

        [Fact]
        public void ReturnNullIfReadIsBeyondEndOfFile()
        {
            var provider = new Provider();
            var target = provider.OpenFileForRead(Constants.TextFilePath);

            target.ReadLine();
            var actual = target.ReadLine();
            target.Close();

            Assert.Null(actual);
        }

        [Fact]
        public void ReturnTheFirstLineOfAMultiLineFile()
        {
            var provider = new Provider();
            var target = provider.OpenFileForRead(Constants.MultiLineFilePath);

            var actual = target.ReadLine();
            target.Close();

            Assert.Equal(Constants.MultiLineFileLines[0], actual);
        }

        [Fact]
        public void ReturnTheSecondLineOfAMultiLineFile()
        {
            var provider = new Provider();
            var target = provider.OpenFileForRead(Constants.MultiLineFilePath);

            target.ReadLine();
            var actual = target.ReadLine();
            target.Close();

            Assert.Equal(Constants.MultiLineFileLines[1], actual);
        }

        [Fact]
        public void ReturnTheLastLineOfAMultiLineFile()
        {
            var provider = new Provider();
            var target = provider.OpenFileForRead(Constants.MultiLineFilePath);

            target.ReadLine();
            target.ReadLine();
            var actual = target.ReadLine();
            target.Close();

            Assert.Equal(Constants.MultiLineFileLines[2], actual);
        }
    }
}
