using System;
using System.Threading.Tasks;
using LiquidNun.Interfaces;

namespace LiquidNun.HttpClient.Native
{
    public class Provider : IHttpClient
    {
        private readonly System.Net.Http.HttpClient _client;

        public Provider(): this(new System.Net.Http.HttpClient())
        { }

        public Provider(System.Net.Http.HttpClient client)
        {
            _client = client;
        }

        public String GetString(String url)
        {
            var task = Task.Run(() => _client.GetStringAsync(url));
            Task.WaitAll(task);
            return task.Result;
        }
    }
}
