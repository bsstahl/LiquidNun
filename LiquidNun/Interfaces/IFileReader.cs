using System;
using System.Collections.Generic;
using System.Text;

namespace LiquidNun.Interfaces
{
    public interface IFileReader
    {
        string ReadLine();

        void Close();
    }
}
