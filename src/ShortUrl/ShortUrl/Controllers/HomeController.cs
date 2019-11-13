using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShortUrl.Models;
using ShortUrl.Service;

namespace ShortUrl.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUrlService _urlServices;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, IUrlService urlServices, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _urlServices = urlServices;
            _httpContextAccessor = httpContextAccessor;
        }

        

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string url)
        {
            var shortUrl = _urlServices.InsertUrl(url);
            var host = showURL(_httpContextAccessor);
            TempData["ShortUrl"] = host +"?url="+ shortUrl;
            return View();
        }

        public IActionResult Url(string url)
        {
            var longUrl = _urlServices.GetUrl(url);
            if (string.IsNullOrEmpty(longUrl))
                return RedirectToAction("Index");
            return Redirect(longUrl);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
