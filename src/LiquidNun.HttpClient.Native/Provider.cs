using System;
using System.Net.Http;
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
            var task = _client.GetStringAsync(url);
            Task.WaitAll(task);
            return task.Result;
        }

        public String PostString(String url, String message)
        {
            var content = new StringContent(message);
            var task = _client.PostAsync(url, content);
            Task.WaitAll(task);

            var resultTask = task.Result.Content.ReadAsStringAsync();
            Task.WaitAll(resultTask);
            return resultTask.Result;
        }
    }
}
