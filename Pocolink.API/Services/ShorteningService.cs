using Pocolink.Models.Models;
using Pocolink.DAL;

namespace Pocolink.API.Services
{
    public class ShorteningService : IShorteningService
    {
        private readonly HashingService _hashingService;
        private readonly IUrlRetrievalService _urlRetrievalService;
        private readonly IDataProviderService _dataProviderService;

        public ShorteningService(HashingService hashingService, IUrlRetrievalService urlRetrievalService, IDataProviderService dataProviderService )
        {
            _hashingService = hashingService;
            _urlRetrievalService = urlRetrievalService;
            _dataProviderService = dataProviderService;
        }

        public string ShortenUrl(string longUrl)
        {
            var shortUrl = _hashingService.HashUrl(longUrl)[..8];

            if (!string.IsNullOrEmpty(_urlRetrievalService.RetrieveUrl(shortUrl))) 
                return shortUrl;
            
            var document = new ShortenedUrl
            {
                SUrl = shortUrl,
                LUrl = longUrl
            };

            _dataProviderService.AddDocumentToCollection(document);

            return shortUrl;
        }
    }
}
