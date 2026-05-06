using System.IO;
using System.Text;
using LiquidNun.Directory.OneDrive;
using Xunit;

namespace LiquidNun.Directory.OneDrive.Test
{
    public class FileReader_ReadLine_Should
    {
        private static Stream ToStream(string content)
            => new MemoryStream(Encoding.UTF8.GetBytes(content));

        private static FileReader Create(string content)
            => new FileReader(ToStream(content));

        [Fact]
        public void ReturnTheContentsOfASingleLineFile()
        {
            const string expected = "single line content";
            using var target = Create(expected);

            var actual = target.ReadLine();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReturnNullIfReadIsBeyondEndOfFile()
        {
            using var target = Create("only one line");

            target.ReadLine(); // consume the only line
            var actual = target.ReadLine();

            Assert.Null(actual);
        }

        [Fact]
        public void ReturnTheFirstLineOfAMultiLineFile()
        {
            using var target = Create("line1\nline2\nline3");

            var actual = target.ReadLine();

            Assert.Equal("line1", actual);
        }

        [Fact]
        public void ReturnTheSecondLineOfAMultiLineFile()
        {
            using var target = Create("line1\nline2\nline3");

            target.ReadLine();
            var actual = target.ReadLine();

            Assert.Equal("line2", actual);
        }

        [Fact]
        public void ReturnTheLastLineOfAMultiLineFile()
        {
            using var target = Create("line1\nline2\nline3");

            target.ReadLine();
            target.ReadLine();
            var actual = target.ReadLine();

            Assert.Equal("line3", actual);
        }
    }
}
