using System;
using System.Collections.Generic;
using System.Text;

namespace ShortUrl.Core.Domain
{
    public class UrlEntity : BaseEntity
    {
        public String Key { get; set; }

        public String Url { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
