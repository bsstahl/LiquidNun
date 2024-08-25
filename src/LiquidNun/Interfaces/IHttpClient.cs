using System.Text;

namespace LiquidNun.Interfaces;

public interface IHttpClient
{
    string GetString(string requestAddress);

    string PostString(string requestAddress, string message);
    
    string PostString(string requestAddress, string message, string? bearerToken);
    string PostString(string requestAddress, string message, Encoding encoding);

    string PostString(string requestAddress, string message, Encoding encoding, string? bearerToken);
    string PostString(string requestAddress, string message, string contentType, string? bearerToken);

    string PostJsonString(string requestAddress, string message);
    string PostJsonString(string requestAddress, string message, string? bearerToken);
    string PostJsonString(string requestAddress, string message, Encoding encoding);

    string PostString(string requestAddress, string message, Encoding encoding, string contentType, string? bearerToken);

}
