using System;
using System.Collections.Generic;
using System.Text;
using TestHelperExtensions;
using Xunit;

namespace LiquidNun.Directory.FileSystem.Test
{
    public class FileReader_Close_Should
    {
        [Fact]
        public void CompleteSuccessfully()
        {
            var provider = new Provider();
            var target = provider.OpenFileForRead(Constants.TextFilePath);
            target.Close();
        }
    }
}
