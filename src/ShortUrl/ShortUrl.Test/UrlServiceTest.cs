using AutoFixture;
using Moq;
using NUnit.Framework;
using ShortUrl.Core.Domain;
using ShortUrl.Data;
using ShortUrl.Service;
using System;
using System.Collections.Generic;
using System.Linq;
namespace ShortUrl.Test
{
    public class UrlServiceTest
    {
        private Mock<IRepository<UrlEntity>> _urlRepositoryMock;
        private IList<UrlEntity> _urlEntities;
        private IUrlService _urlService;

        [SetUp]
        public void Setup()
        {
            _urlRepositoryMock = new Mock<IRepository<UrlEntity>>();
            _urlEntities = new Fixture().Build<UrlEntity>().CreateMany(10).ToList();
            _urlRepositoryMock.Setup(x => x.Table).Returns(_urlEntities.AsQueryable());
            _urlService = new UrlService(_urlRepositoryMock.Object);
        }

        [Test]
        public void UrlService_InsertUrl_ShouldReturnShortUrl()
        {
            var url = "https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpresponsemessage?view=netframework-4.8";
            var shortUrlKey = _urlService.InsertUrl(url);
            Assert.IsNotNull(shortUrlKey);
            Assert.IsTrue(shortUrlKey.Length > 0);
        }

        [Test]
        public void UrlService_InsertUrl_InvalidUrl_ShouldThrowException()
        {
            var url = new Fixture().Create<string>();
            var ex = Assert.Catch<UriFormatException>(() => _urlService.InsertUrl(url));
            Assert.That(ex.Message== "Invalid URI: The format of the URI could not be determined.");
        }

        [Test]
        public void UrlService_GetUrl_ShouldReturnLongUrl()
        {
            var urlEntity = _urlEntities.FirstOrDefault();
            var longUrl = _urlService.GetUrl(urlEntity.Key);
            Assert.IsNotNull(longUrl);
            Assert.IsTrue(urlEntity.Url.Equals(longUrl));
        }
    }
}