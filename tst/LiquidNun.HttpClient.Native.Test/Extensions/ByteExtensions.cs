using System;

namespace LiquidNun.HttpClient.Native.Test.Extensions;

public static class ByteExtensions
{
    public static string FromContentTypeEncoding(this byte[] value, string contentType)
    {
        var sourceEncoding = contentType.GetEncoding();
        var result = sourceEncoding.GetString(value);
        return result;
    }

}
