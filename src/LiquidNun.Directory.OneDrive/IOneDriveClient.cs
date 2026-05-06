using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LiquidNun.Directory.OneDrive
{
    /// <summary>
    /// Abstracts the Microsoft Graph drive operations required by <see cref="Provider"/>.
    /// Keeping this internal allows the <see cref="Provider"/> to be unit-tested without
    /// a live OneDrive connection.
    /// </summary>
    internal interface IOneDriveClient
    {
        /// <summary>
        /// Returns the children of the folder at <paramref name="path"/>, or
        /// <see langword="null"/> when the folder does not exist.
        /// </summary>
        Task<IEnumerable<DriveItemInfo>?> GetChildrenAsync(string path);

        /// <summary>
        /// Returns the raw content stream of the file at <paramref name="filePath"/>, or
        /// <see langword="null"/> when the file does not exist.
        /// </summary>
        Task<Stream?> GetFileContentStreamAsync(string filePath);

        /// <summary>
        /// Returns metadata for the item at <paramref name="path"/>, or
        /// <see langword="null"/> when the item does not exist.
        /// </summary>
        Task<DriveItemInfo?> GetItemAsync(string path);
    }
}
