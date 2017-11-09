﻿using System;
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

        IEnumerable<string> IDirectoryService.GetFiles(string pathName)
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

        #endregion
    }

}
