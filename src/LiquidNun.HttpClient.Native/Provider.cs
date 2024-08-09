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
            return this.PostString(url, message, null);
        }

        public String PostString(String url, String message, String bearerToken)
        {
            // Set the request's Authorization header if a bearer token is supplied
            if (!string.IsNullOrWhiteSpace(bearerToken))
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);

            var content = new StringContent(message);
            var task = _client.PostAsync(url, content);
            Task.WaitAll(task);

            var resultTask = task.Result.Content.ReadAsStringAsync();
            Task.WaitAll(resultTask);

            // Clear the Authorization header
            _client.DefaultRequestHeaders.Authorization = null;

            return resultTask.Result;
        }
    }
}
