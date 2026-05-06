using System.IO;
using System.Text;
using System.Threading.Tasks;
using LiquidNun.Directory.OneDrive;
using Moq;
using Xunit;

namespace LiquidNun.Directory.OneDrive.Test
{
    public class Provider_ReadAllText_Should
    {
        private static Stream ToStream(string content)
            => new MemoryStream(Encoding.UTF8.GetBytes(content));

        [Fact]
        public void ThrowFileNotFoundIfTheFileDoesNotExist()
        {
            var mockClient = new Mock<IOneDriveClient>();
            mockClient
                .Setup(c => c.GetFileContentStreamAsync(It.IsAny<string>()))
                .ReturnsAsync((Stream?)null);

            var target = new Provider(mockClient.Object);

            Assert.Throws<Exceptions.FileNotFoundException>(() => target.ReadAllText("/missing.txt"));
        }

        [Fact]
        public void ReturnTheTextFromTheFile()
        {
            const string expected = "Hello, OneDrive!";

            var mockClient = new Mock<IOneDriveClient>();
            mockClient
                .Setup(c => c.GetFileContentStreamAsync("/notes.txt"))
                .ReturnsAsync(ToStream(expected));

            var target = new Provider(mockClient.Object);
            var actual = target.ReadAllText("/notes.txt");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReturnEmptyStringForAnEmptyFile()
        {
            var mockClient = new Mock<IOneDriveClient>();
            mockClient
                .Setup(c => c.GetFileContentStreamAsync("/empty.txt"))
                .ReturnsAsync(ToStream(string.Empty));

            var target = new Provider(mockClient.Object);
            var actual = target.ReadAllText("/empty.txt");

            Assert.Equal(string.Empty, actual);
        }
    }
}
