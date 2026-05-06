using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using LiquidNun.Directory.OneDrive;
using Moq;
using Xunit;

namespace LiquidNun.Directory.OneDrive.Test
{
    public class Provider_FileExists_Should
    {
        [Fact]
        public void ReturnFalseIfTheItemDoesNotExist()
        {
            var mockClient = new Mock<IOneDriveClient>();
            mockClient
                .Setup(c => c.GetItemAsync(It.IsAny<string>()))
                .ReturnsAsync((DriveItemInfo?)null);

            var target = new Provider(mockClient.Object);

            Assert.False(target.FileExists("/nonexistent/file.txt"));
        }

        [Fact]
        public void ReturnFalseIfThePathPointsToAFolder()
        {
            var mockClient = new Mock<IOneDriveClient>();
            mockClient
                .Setup(c => c.GetItemAsync("/Documents"))
                .ReturnsAsync(new DriveItemInfo("Documents", "/Documents", isFile: false, isFolder: true));

            var target = new Provider(mockClient.Object);

            Assert.False(target.FileExists("/Documents"));
        }

        [Fact]
        public void ReturnTrueIfTheFileExists()
        {
            var mockClient = new Mock<IOneDriveClient>();
            mockClient
                .Setup(c => c.GetItemAsync("/Documents/report.pdf"))
                .ReturnsAsync(new DriveItemInfo("report.pdf", "/Documents/report.pdf", isFile: true, isFolder: false));

            var target = new Provider(mockClient.Object);

            Assert.True(target.FileExists("/Documents/report.pdf"));
        }
    }
}
