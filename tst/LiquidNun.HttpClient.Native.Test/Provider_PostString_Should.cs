using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using TestHelperExtensions;

namespace LiquidNun.HttpClient.Native.Test
{
    public class Provider_PostString_Should
    {
        const string _url = @"http://httpbin.org/post";

        readonly System.Net.Http.HttpClient _client = new System.Net.Http.HttpClient();

        [Fact]
        public void PostToHttpBinIfConnected()
        {
            var message = string.Empty.GetRandom();
            var target = new Provider(_client);
            var result = target.PostString(_url, message);
            Assert.Contains(message, result);
        }

    }
}
