using System;
using Xunit;
using TestHelperExtensions;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using LiquidNun.HttpClient.Native.Test.Entities;
using System.Text;
using LiquidNun.HttpClient.Native.Test.Extensions;

namespace LiquidNun.HttpClient.Native.Test;

[ExcludeFromCodeCoverage]
public class Provider_PostString_Should
{
    const string _url = @"http://httpbin.org/post";

    readonly Encoding _defaultEncoding = Encoding.UTF8;
    readonly System.Net.Http.HttpClient _client = new System.Net.Http.HttpClient();

    [Fact]
    public void PostToHttpBinIfConnected()
    {
        var message = string.Empty.GetRandom();

        var target = new Provider(_client);
        var result = target.PostString(_url, message);
        var jsonResult = JsonSerializer.Deserialize<JsonResponse>(result);
        
        Assert.Contains(message, jsonResult.Data);
    }

    [Fact]
    public void PostToHttpBinWithBearerTokenIfConnected()
    {
        var message = string.Empty.GetRandom();
        var token = string.Empty.GetRandom();

        var target = new Provider(_client);
        var result = target.PostString(_url, message, token);
        var jsonResult = JsonSerializer.Deserialize<JsonResponse>(result);

        Assert.Contains($"Bearer {token}", jsonResult.Headers.Authorization);
    }

    [Fact]
    public void PostToHttpBinWithEncodingIfConnected()
    {
        var encoding = Encoding.BigEndianUnicode;
        var message = string.Empty.GetRandom();
        var encodedMessage = message.TransformEncoding(_defaultEncoding, encoding);
        encodedMessage.VerifyEncoding(message);

        var target = new Provider(_client);
        var result = target.PostString(_url, encodedMessage); // , encoding);
        var jsonResult = JsonSerializer.Deserialize<JsonResponse>(result);

        Assert.Contains(message, jsonResult.Data);
    }

    [Fact]
    public void PostToHttpBinWithIncorrectEncodingPassesInvalidValue()
    {
        var encoding = Encoding.BigEndianUnicode;
        var message = string.Empty.GetRandom();
        var encodedMessage = message.TransformEncoding(_defaultEncoding, encoding);
        encodedMessage.VerifyEncoding(message);

        var target = new Provider(_client);
        var result = target.PostString(_url, encodedMessage, Encoding.UTF8);
        var jsonResult = JsonSerializer.Deserialize<JsonResponse>(result);

        var expectedSubstring = jsonResult.Data;
        Assert.DoesNotContain(expectedSubstring, message, StringComparison.Ordinal);
    }

    [Fact]
    public void PostToHttpBinWithEncodingAndBearerTokenIfConnected()
    {
        var encoding = Encoding.BigEndianUnicode;
        var message = string.Empty.GetRandom();
        var encodedMessage = message.TransformEncoding(_defaultEncoding, encoding);
        var token = string.Empty.GetRandom();
        encodedMessage.VerifyEncoding(message);

        var target = new Provider(_client);
        var result = target.PostString(_url, encodedMessage, encoding, token);
        var jsonResult = JsonSerializer.Deserialize<JsonResponse>(result);

        Assert.Contains(message, jsonResult.Data);
        Assert.Contains($"Bearer {token}", jsonResult.Headers.Authorization);
    }

    [Fact]
    public void PostToHttpBinWithIncorrectEncodingAndBearerTokenPassesInvalidValue()
    {
        var encoding = Encoding.BigEndianUnicode;
        var message = string.Empty.GetRandom();
        var encodedMessage = message.TransformEncoding(_defaultEncoding, encoding);
        var token = string.Empty.GetRandom();
        encodedMessage.VerifyEncoding(message);

        var target = new Provider(_client);
        var result = target.PostString(_url, encodedMessage, Encoding.UTF8, token);
        var jsonResult = JsonSerializer.Deserialize<JsonResponse>(result);

        var expectedSubstring = jsonResult.Data;
        Assert.DoesNotContain(expectedSubstring, message, StringComparison.Ordinal);
    }

    [Fact]
    public void PostToHttpBinWithContentTypeAndBearerTokenIfConnected()
    {
        var message = string.Empty.GetRandom();
        var content = $"<myMessage>{message}</myMessage>";
        var token = string.Empty.GetRandom();
        var contentType = "text/xml";

        var target = new Provider(_client);
        var result = target.PostString(_url, message, contentType, token);
        var jsonResult = JsonSerializer.Deserialize<JsonResponse>(result);

        Assert.Contains(contentType, jsonResult.Headers.ContentType);
        Assert.Contains(message, jsonResult.Data);
    }
}
