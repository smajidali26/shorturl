using System;
using System.Collections.Generic;
using System.Text;

namespace ShortUrl.Service
{
    public interface IUrlService
    {
        string InsertUrl(string longUrl);

        string GetUrl(string shortUrl);

        bool ExistKey(string key);
    }
}
