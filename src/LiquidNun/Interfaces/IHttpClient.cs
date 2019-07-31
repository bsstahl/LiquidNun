using System;
using System.Collections.Generic;
using System.Text;

namespace LiquidNun.Interfaces
{
    public interface IHttpClient
    {
        string GetString(string url);
    }
}
