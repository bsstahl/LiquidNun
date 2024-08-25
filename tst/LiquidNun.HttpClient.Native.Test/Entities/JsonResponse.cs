using LiquidNun.HttpClient.Native.Test.Extensions;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json.Serialization;

namespace LiquidNun.HttpClient.Native.Test.Entities;

[ExcludeFromCodeCoverage]
public class JsonResponse
{
    [JsonPropertyName("args")]
    public Args Args { get; set; }

    [JsonPropertyName("data")] // , JsonConverter(typeof(RawStringConverter))]
    public string EncodedData { get; set; }

    public Files files { get; set; }
    public Form form { get; set; }


    [JsonPropertyName("headers")]
    public Headers Headers { get; set; }

    [JsonPropertyName("json")]
    public object Json { get; set; }
    public string origin { get; set; }
    public string url { get; set; }

    public string Data
        => this.EncodedData.FromContentTypeEncoding(this.Headers.ContentType);
}

[ExcludeFromCodeCoverage]
public class Args
{
}

[ExcludeFromCodeCoverage]
public class Files
{
}

[ExcludeFromCodeCoverage]
public class Form
{
}

[ExcludeFromCodeCoverage]
public class Headers
{
    public string Authorization { get; set; }

    [JsonPropertyName("Content-Length")]
    public string ContentLength { get; set; }

    [JsonPropertyName("Content-Type")]
    public string ContentType { get; set; }
    public string Host { get; set; }

    [JsonPropertyName("X-Amzn-Trace-Id")]
    public string XAmznTraceId { get; set; }
}
