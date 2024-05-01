using System;
using System.Collections.Generic;

namespace LiquidNun.EmailClient.HtmlWithBasicAuth.Extensions;

public static class StringExtensions
{
    internal static IEnumerable<string> ParseAddresses(this string addresses)
    {
        return string.IsNullOrWhiteSpace(addresses) 
            ? Array.Empty<string>() 
            : (IEnumerable<string>)addresses.Split(';');
    }

}