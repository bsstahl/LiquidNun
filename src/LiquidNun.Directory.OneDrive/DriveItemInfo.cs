namespace LiquidNun.Directory.OneDrive
{
    /// <summary>
    /// Holds metadata about a single item (file or folder) in a OneDrive drive.
    /// </summary>
    internal sealed class DriveItemInfo
    {
        /// <summary>The display name of the item.</summary>
        internal string Name { get; }

        /// <summary>The drive-root-relative path of the item, e.g. <c>/Documents/report.pdf</c>.</summary>
        internal string Path { get; }

        /// <summary><see langword="true"/> when the item is a file.</summary>
        internal bool IsFile { get; }

        /// <summary><see langword="true"/> when the item is a folder.</summary>
        internal bool IsFolder { get; }

        internal DriveItemInfo(string name, string path, bool isFile, bool isFolder)
        {
            Name = name;
            Path = path;
            IsFile = isFile;
            IsFolder = isFolder;
        }
    }
}
