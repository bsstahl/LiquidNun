using System.IO;
using System.Text;
using LiquidNun.Directory.OneDrive;
using LiquidNun.Interfaces;
using Moq;
using Xunit;

namespace LiquidNun.Directory.OneDrive.Test
{
    public class Provider_OpenFileForRead_Should
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

            Assert.Throws<Exceptions.FileNotFoundException>(() => target.OpenFileForRead("/missing.txt"));
        }

        [Fact]
        public void ReturnAnIFileReaderWhenTheFileExists()
        {
            var mockClient = new Mock<IOneDriveClient>();
            mockClient
                .Setup(c => c.GetFileContentStreamAsync("/data.txt"))
                .ReturnsAsync(ToStream("content"));

            var target = new Provider(mockClient.Object);
            var reader = target.OpenFileForRead("/data.txt");

            Assert.NotNull(reader);
            reader.Close();
        }

        [Fact]
        public void ReturnAReaderThatCanReadTheFileContent()
        {
            const string expected = "sample line";

            var mockClient = new Mock<IOneDriveClient>();
            mockClient
                .Setup(c => c.GetFileContentStreamAsync("/data.txt"))
                .ReturnsAsync(ToStream(expected));

            var target = new Provider(mockClient.Object);
            using var reader = (FileReader)target.OpenFileForRead("/data.txt");

            Assert.Equal(expected, reader.ReadLine());
        }
    }
}
