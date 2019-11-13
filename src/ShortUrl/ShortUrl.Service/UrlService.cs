using ShortUrl.Data;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace ShortUrl.Service
{
    public class UrlService : IUrlService
    {

        #region Private Fields
        private List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
        private List<char> characters = new List<char>()
    {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
    'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B',
    'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
    'Q', 'R', 'S',  'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '-', '_'};
        private readonly IRepository<Core.Domain.UrlEntity> _repository;

        #endregion

        #region ctor

        public UrlService(IRepository<Core.Domain.UrlEntity> repository)
        {
            _repository = repository;
        }

        #endregion


        #region Methods

        
        public string GetUrl(string shortUrl)
        {
            var existUrl = _repository.Table.FirstOrDefault(x => x.Key.Equals(shortUrl));
            if (existUrl != null && !string.IsNullOrEmpty(existUrl.Key))
                return existUrl.Url;
            return null;
        }

        public string InsertUrl(string longUrl)
        {
            if (!string.IsNullOrWhiteSpace(longUrl))
            {
                longUrl = longUrl.Trim().ToLower();
            }

            var existUrl = _repository.Table.FirstOrDefault(x => x.Url.Equals(longUrl));
            if (existUrl != null && !string.IsNullOrEmpty(existUrl.Key))
                return existUrl.Key;

            try
            {
                var url = new Uri(longUrl);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (HttpGetStatusCode(longUrl).Result == HttpStatusCode.OK)
            {

                String newKey = null;
                string key = GetShortURL();
                while (string.IsNullOrEmpty(newKey))
                {

                    if (!ExistKey(key))
                    {
                        newKey = key;
                        _repository.Insert(new Core.Domain.UrlEntity{ Key = newKey, Url = longUrl, DateCreated = DateTime.Now });
                        
                    }
                    else
                    {
                        key = GetShortURL();
                    }
                }

                return newKey;
            }
            else
            {
                throw new Exception("Invalid Url");
            }
            
        }

        public bool ExistKey(string key)
        {
           return _repository.Table.Any(x => x.Key.Equals(key));
        }
        #endregion

        #region Private Methods

        public async Task<HttpStatusCode> HttpGetStatusCode(string Url)
        {
            try
            {
                var httpclient = new HttpClient();
                httpclient.Timeout = TimeSpan.FromSeconds(100);
                var response = await httpclient.GetAsync(Url, HttpCompletionOption.ResponseHeadersRead);

                string text = null;

                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    var bytes = new byte[10];
                    var bytesread = stream.Read(bytes, 0, 10);
                    stream.Close();

                    text = Encoding.UTF8.GetString(bytes);

                    Console.WriteLine(text);
                }

                return response.StatusCode;
            }
            catch (Exception ex)
            {
                return HttpStatusCode.NotFound;
            }
        }

        

        public string GetShortURL()
        {
            string URL = "";
            Random rand = new Random();
            // run the loop till I get a string of 10 characters  
            for (int i = 0; i < 11; i++)
            {
                // Get random numbers, to get either a character or a number...  
                int random = rand.Next(0, 3);
                if (random == 1)
                {
                    // use a number  
                    random = rand.Next(0, numbers.Count);
                    URL += numbers[random].ToString();
                }
                else
                {
                    random = rand.Next(0, characters.Count);
                    URL += characters[random].ToString();
                }
            }
            return URL;
        }

        #endregion
    }
}
