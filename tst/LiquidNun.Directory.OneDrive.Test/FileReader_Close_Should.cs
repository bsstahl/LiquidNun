using System.IO;
using System.Text;
using LiquidNun.Directory.OneDrive;
using Xunit;

namespace LiquidNun.Directory.OneDrive.Test
{
    public class FileReader_Close_Should
    {
        [Fact]
        public void CompleteSuccessfully()
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes("content"));
            var target = new FileReader(stream);
            target.Close();
        }

        [Fact]
        public void BeIdempotent_WhenCalledMultipleTimes()
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes("content"));
            var target = new FileReader(stream);
            target.Close();
            target.Close(); // second call must not throw
        }
    }
}
