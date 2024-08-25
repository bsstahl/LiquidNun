using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace LiquidNun.HttpClient.Native.Test.Extensions;

[ExcludeFromCodeCoverage]
public static class StringExtensions
{
    public static Encoding GetEncoding(this string contentType)
    {
        var encodingName = contentType.GetEncodingName();
        return Encoding.GetEncoding(encodingName);
    }

    public static string GetEncodingName(this string contentType)
    {
        var a = contentType.Split(';');
        var b = a[1].Split('=');
        var encodingName = b[1].Trim();
        return string.IsNullOrWhiteSpace(encodingName)
            ? Encoding.Unicode.EncodingName
            : encodingName;
    }

    public static string TransformEncoding(this string value, Encoding sourceEncoding, Encoding targetEncoding)
    {
        var defaultBytes = sourceEncoding.GetBytes(value);
        return targetEncoding.GetString(defaultBytes);
    }

    public static void VerifyEncoding(this string encodedValue, string unencodedValue)
    {
        if (encodedValue.Equals(unencodedValue))
            throw new InvalidOperationException("Value was not encoded in a distinguishable way");
    }

    public static string FromContentTypeEncoding(this string value, string contentType)
    {
        var sourceEncoding = contentType.GetEncoding();
        var bytes = Encoding.Default.GetBytes(value);
        var result = sourceEncoding.GetString(bytes);
        return result;
    }

}
