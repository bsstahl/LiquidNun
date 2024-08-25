using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using LiquidNun.Interfaces;

namespace LiquidNun.HttpClient.Native;

public class Provider : IHttpClient
{
    public static Encoding DefaultEncoding => Encoding.UTF8;
    public static string DefaultContentType => "text/plain";
    public static string DefaultJsonContentType => "application/json";
    public static string? DefaultBearerToken => null;


    private readonly System.Net.Http.HttpClient _client;

    public Provider(): this(new System.Net.Http.HttpClient())
    { }

    public Provider(System.Net.Http.HttpClient client)
    {
        _client = client;
    }

    public String GetString(String requestAddress)
    {
        var task = _client.GetStringAsync(requestAddress);
        task.Wait();
        return task.Result;
    }

    public String PostString(String requestAddress, String message)
    {
        return PostString(requestAddress, message, Provider.DefaultEncoding, Provider.DefaultContentType, Provider.DefaultBearerToken);
    }

    public String PostString(String requestAddress, String message, Encoding encoding)
    {
        return PostString(requestAddress, message, encoding, Provider.DefaultContentType, Provider.DefaultBearerToken);
    }

    public String PostString(String requestAddress, String message, Encoding encoding, string? bearerToken)
    {
        return PostString(requestAddress, message, encoding, Provider.DefaultContentType, bearerToken);
    }

    public string PostString(string requestAddress, string message, string? bearerToken)
    {
        return PostString(requestAddress, message, Provider.DefaultEncoding, Provider.DefaultContentType, bearerToken);
    }

    public string PostString(string requestAddress, string message, string contentType, string? bearerToken)
    {
        return PostString(requestAddress, message, Provider.DefaultEncoding, contentType, bearerToken);
    }


    public string PostJsonString(string requestAddress, string message)
    {
        return PostString(requestAddress, message, Provider.DefaultEncoding, Provider.DefaultJsonContentType, Provider.DefaultBearerToken);
    }

    public string PostJsonString(string requestAddress, string message, string? bearerToken)
    {
        return PostString(requestAddress, message, Provider.DefaultEncoding, Provider.DefaultJsonContentType, bearerToken);
    }

    public string PostJsonString(string requestAddress, string message, Encoding encoding)
    {
        return PostString(requestAddress, message, encoding, Provider.DefaultJsonContentType, Provider.DefaultBearerToken);
    }


    public string PostString(string requestAddress, string message, Encoding encoding, string contentType, string? bearerToken)
    {
        // Set the request's Authorization header if a bearer token is supplied
        if (!string.IsNullOrWhiteSpace(bearerToken))
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);

        string result;
        using (var content = new StringContent(message, encoding, contentType))
        {
            var postTask = _client.PostAsync(requestAddress, content);
            postTask.Wait();

            var resultTask = postTask.Result.Content.ReadAsStringAsync();
            resultTask.Wait();

            result = resultTask.Result;
        }

        // Clear the Authorization header
        _client.DefaultRequestHeaders.Authorization = null;

        return result;
    }

}
