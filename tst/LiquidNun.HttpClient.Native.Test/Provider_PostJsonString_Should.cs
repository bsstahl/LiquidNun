using System;
using Xunit;
using TestHelperExtensions;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using LiquidNun.HttpClient.Native.Test.Entities;
using LiquidNun.HttpClient.Native.Test.Extensions;
using System.Text;

namespace LiquidNun.HttpClient.Native.Test;

[ExcludeFromCodeCoverage]
public class Provider_PostJsonString_Should
{
    const string _url = @"http://httpbin.org/post";

    readonly Encoding _defaultEncoding = Encoding.Unicode;
    readonly System.Net.Http.HttpClient _client = new System.Net.Http.HttpClient();

    [Fact]
    public void PostToHttpBinIfConnected()
    {
        var value = string.Empty.GetRandom();
        var message = $"{{ \"message\": \"{value}\" }}";
        
        var target = new Provider(_client);
        var result = target.PostJsonString(_url, message);
        var jsonResult = JsonSerializer.Deserialize<JsonResponse>(result);

        Assert.Contains(value, jsonResult.Data);
    }

    [Fact]
    public void PostToHttpBinWithBearerTokenIfConnected()
    {
        var value = string.Empty.GetRandom();
        var message = $"{{ \"message\": \"{value}\" }}";
        var token = string.Empty.GetRandom();

        var target = new Provider(_client);
        var result = target.PostJsonString(_url, message, token);
        var jsonResult = JsonSerializer.Deserialize<JsonResponse>(result);

        Assert.Contains($"Bearer {token}", jsonResult.Headers.Authorization, StringComparison.Ordinal);
    }

    [Fact]
    public void PostToHttpBinWithEncodingIfConnected()
    {
        var encoding = Encoding.BigEndianUnicode;
        var value = string.Empty.GetRandom();

        var encodedValue = value.TransformEncoding(_defaultEncoding, encoding);
        encodedValue.VerifyEncoding(value);

        var target = new Provider(_client);
        var result = target.PostJsonString(_url, encodedValue, encoding);

        var jsonResult = JsonSerializer.Deserialize<JsonResponse>(result);
        Assert.Contains(value, jsonResult.Data, StringComparison.Ordinal);
    }

    [Fact]
    public void PostToHttpBinWithIncorrectEncodingPassesInvalidValue()
    {
        var encoding = Encoding.BigEndianUnicode;
        var value = string.Empty.GetRandom();
        var message = $"{{ \"message\": \"{value}\" }}";
        var encodedMessage = message.TransformEncoding(_defaultEncoding, encoding);
        encodedMessage.VerifyEncoding(message);

        var target = new Provider(_client);
        var result = target.PostJsonString(_url, encodedMessage, Encoding.UTF8);
        var jsonResult = JsonSerializer.Deserialize<JsonResponse>(result);

        Assert.DoesNotContain(value, jsonResult.Data, StringComparison.Ordinal);
    }

}
