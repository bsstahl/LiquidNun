using LiquidNun.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LiquidNun.Directory.FileSystem
{
    public class FileReader : IFileReader
    {
        readonly System.IO.TextReader _reader;

        public FileReader(string filePath)
        {
            try
            {
                _reader = System.IO.File.OpenText(filePath);
            }
            catch (FileNotFoundException ex)
            {
                throw new Exceptions.FileNotFoundException(filePath, ex);
            }
        }

        public void Close()
        {
            _reader.Dispose();
        }

        public string ReadLine()
        {
            return _reader.ReadLine();
        }

    }
}
