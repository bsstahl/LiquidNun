using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LiquidNun.Directory.OneDrive;
using Moq;
using Xunit;

namespace LiquidNun.Directory.OneDrive.Test
{
    public class Provider_GetFiles_Should
    {
        [Fact]
        public void ThrowDirectoryNotFoundIfFolderDoesNotExist()
        {
            var mockClient = new Mock<IOneDriveClient>();
            mockClient
                .Setup(c => c.GetChildrenAsync(It.IsAny<string>()))
                .ReturnsAsync((IEnumerable<DriveItemInfo>?)null);

            var target = new Provider(mockClient.Object);

            Assert.Throws<Exceptions.DirectoryNotFoundException>(() => target.GetFiles("/nonexistent"));
        }

        [Fact]
        public void ReturnOnlyFiles_ExcludingFolders()
        {
            var mockClient = new Mock<IOneDriveClient>();
            mockClient
                .Setup(c => c.GetChildrenAsync("/Documents"))
                .ReturnsAsync(new[]
                {
                    new DriveItemInfo("file.txt",   "/Documents/file.txt",   isFile: true,  isFolder: false),
                    new DriveItemInfo("subfolder",  "/Documents/subfolder",  isFile: false, isFolder: true),
                });

            var target = new Provider(mockClient.Object);
            var result = target.GetFiles("/Documents").ToList();

            Assert.Single(result);
            Assert.Contains("/Documents/file.txt", result);
        }

        [Fact]
        public void ReturnAllFilesWhenFolderContainsMultipleFiles()
        {
            var mockClient = new Mock<IOneDriveClient>();
            mockClient
                .Setup(c => c.GetChildrenAsync("/Reports"))
                .ReturnsAsync(new[]
                {
                    new DriveItemInfo("q1.xlsx", "/Reports/q1.xlsx", isFile: true, isFolder: false),
                    new DriveItemInfo("q2.xlsx", "/Reports/q2.xlsx", isFile: true, isFolder: false),
                });

            var target = new Provider(mockClient.Object);
            var result = target.GetFiles("/Reports").ToList();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void ReturnEmptyCollectionWhenFolderIsEmpty()
        {
            var mockClient = new Mock<IOneDriveClient>();
            mockClient
                .Setup(c => c.GetChildrenAsync("/Empty"))
                .ReturnsAsync(new DriveItemInfo[0]);

            var target = new Provider(mockClient.Object);
            var result = target.GetFiles("/Empty");

            Assert.Empty(result);
        }
    }
}
