using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShortUrl.Service;

namespace ShortUrl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortUrlController : ControllerBase
    {

        private readonly IUrlService _urlServices;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShortUrlController( IUrlService urlServices, IHttpContextAccessor httpContextAccessor)
        {
            _urlServices = urlServices;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("{url}")]
        public IActionResult Get(string url)
        {
            var longUrl = _urlServices.GetUrl(url);
            if (string.IsNullOrEmpty(longUrl))
                return BadRequest();
            return Ok(longUrl);
        }

        [HttpPost]
        [Route("{url}")]
        public IActionResult Post(string url)
        {
            var shortUrl = _urlServices.InsertUrl(url);
            if (!string.IsNullOrEmpty(shortUrl))
            {
                var host = showURL(_httpContextAccessor);
                return Ok(host + "?url=" + shortUrl);
            }
            return BadRequest();
        }

        public string showURL(IHttpContextAccessor httpcontextaccessor)
        {
            var request = httpcontextaccessor.HttpContext.Request;

            var absoluteUri = string.Concat(
                        request.Scheme,
                        "://",
                        request.Host.ToUriComponent(),
                        request.PathBase.ToUriComponent());
            return absoluteUri;
        }
    }
}