using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using LiquidNun.Directory.OneDrive;
using Moq;
using Xunit;

namespace LiquidNun.Directory.OneDrive.Test
{
    public class Provider_Exists_Should
    {
        [Fact]
        public void ReturnFalseIfThePathDoesNotExist()
        {
            var mockClient = new Mock<IOneDriveClient>();
            mockClient
                .Setup(c => c.GetItemAsync(It.IsAny<string>()))
                .ReturnsAsync((DriveItemInfo?)null);

            var target = new Provider(mockClient.Object);

            Assert.False(target.Exists("/nonexistent/path"));
        }

        [Fact]
        public void ReturnTrueIfAFolderExists()
        {
            var mockClient = new Mock<IOneDriveClient>();
            mockClient
                .Setup(c => c.GetItemAsync("/Documents"))
                .ReturnsAsync(new DriveItemInfo("Documents", "/Documents", isFile: false, isFolder: true));

            var target = new Provider(mockClient.Object);

            Assert.True(target.Exists("/Documents"));
        }

        [Fact]
        public void ReturnTrueIfAFileExists()
        {
            var mockClient = new Mock<IOneDriveClient>();
            mockClient
                .Setup(c => c.GetItemAsync("/notes.txt"))
                .ReturnsAsync(new DriveItemInfo("notes.txt", "/notes.txt", isFile: true, isFolder: false));

            var target = new Provider(mockClient.Object);

            Assert.True(target.Exists("/notes.txt"));
        }
    }
}
