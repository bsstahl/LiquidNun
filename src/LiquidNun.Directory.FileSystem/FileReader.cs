using LiquidNun.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiquidNun.Directory.FileSystem
{
    // TODO: Implement IDisposable

    public class FileReader : IFileReader
    {
        System.IO.TextReader _reader;

        public FileReader() { }

        public FileReader(string filePath)
        {
            _reader = System.IO.File.OpenText(filePath);
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
