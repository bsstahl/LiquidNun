using System;
using Xunit;

namespace LiquidNun.HttpClient.Native.Test
{
    public class Provider_GetString_Should
    {
        const string _url = @"http://www.google.com";

        readonly System.Net.Http.HttpClient _client = new System.Net.Http.HttpClient();

        [Fact]
        public void LoadGoogleIfConnected()
        {
            var target = new Provider(_client);
            var result = target.GetString(_url);
            Assert.Contains("I'm Feeling Lucky", result);
        }
    }
}
