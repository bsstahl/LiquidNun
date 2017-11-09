using System;
using System.Collections.Generic;
using System.Linq;

namespace LiquidNun.Interfaces
{
    /// <summary>
    /// Describes an interface of a provider that implements directory 
    /// services for files and other file system type objects.
    /// </summary>
    public interface IDirectoryService
    {
        /// <summary>
        /// Returns the names of files in the specified directory.
        /// </summary>
        /// <param name="pathName">Path name.</param>
        IEnumerable<string> GetFiles(string pathName);

        //void MoveFile(string sourceFilePath, string destinationFolderPath);
        //void MoveFile(string sourceFilePath, string destinationFolderPath, string destinationFileName);

        //void CopyFile(string sourceFilePath, string destinationFolderPath);
        //void CopyFile(string sourceFilePath, string destinationFolderPath, string destinationFileName);
        //void CopyFile(string sourceFilePath, string destinationFolderPath, bool overwrite);
        //void CopyFile(string sourceFilePath, string destinationFolderPath, string destinationFileName, bool overwrite);

        /// <summary>
        /// Returns all of the text of the file at the specified path
        /// </summary>
        /// <param name="filePath">Path name.</param>
        string ReadAllText(string filePath);

        //string[] ReadAllLines(string filePath);
        //void DeleteFile(string filePath);
        bool FileExists(string filePath);
        //byte[] ReadAllBytes(string filePath);
        //System.IO.Stream GetFileData(string filePath);

        bool Exists(string pathName);
    }
}
