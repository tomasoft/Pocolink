using System.Linq;
using Pocolink.DAL;

namespace Pocolink.API.Services
{
    public class UrlRetrievalService : IUrlRetrievalService
    {
        private readonly IDataProviderService _dataProviderService;

        public UrlRetrievalService(IDataProviderService dataProviderService)
        {
            _dataProviderService = dataProviderService;
        }

        public string RetrieveUrl(string sUrl)
        {
            var documents = _dataProviderService.ListDocuments(f => f.SUrl == sUrl);
            var lUrl = documents.FirstOrDefault()?.LUrl.ToString();
            return lUrl;
        }
    }
}
