using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiquidNun.Interfaces;

namespace LiquidNun.Directory.FileSystem
{
    public class Provider : Interfaces.IDirectoryService
    {
        #region IDirectoryService Members

        public string ReadAllText(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                throw new Exceptions.FileNotFoundException(filePath);
            return System.IO.File.ReadAllText(filePath);
        }

        public IEnumerable<string> GetFiles(string pathName)
        {
            try
            {
                return System.IO.Directory.GetFiles(pathName);
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                throw new Exceptions.DirectoryNotFoundException(pathName);
            }
        }

        public bool Exists(string pathName)
        {
            return System.IO.Directory.Exists(pathName);
        }

        public bool FileExists(string filePath)
        {
            return System.IO.File.Exists(filePath);
        }

        #endregion
    }

}