using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pocolink.Models.Models;

namespace Pocolink.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PocofyController : ControllerBase
    {
        
        private readonly ILogger<PocofyController> _logger;
        private readonly IShorteningService _shorteningService;
        private readonly IValidationService _validationService;
        private readonly IUrlRetrievalService _urlRetrievalService;

        public PocofyController(ILogger<PocofyController> logger, IShorteningService shorteningService, IValidationService validationService, IUrlRetrievalService urlRetrievalService)
        {
            _logger = logger;
            _shorteningService = shorteningService;
            _validationService = validationService;
            _urlRetrievalService = urlRetrievalService;
        }

        [Route("~/api/ShortenUrl")]
        [HttpPost]
        public string GetShortenedUrl([FromBody] FormInput formInput)
        {
            if (!_validationService.ValidateUrl(formInput.LongUrl))
                throw new HttpRequestException("The longUrl provided is invalid!");
            
            var shortenedUrl = $"https://localhost:44310/?longUrl={_shorteningService.ShortenUrl(formInput.LongUrl)}";

            return shortenedUrl;
        }

        [Route("~/api/RetrieveSourceUrl")]
        [HttpPost]
        public string RetrieveSourceUrl([FromBody] FormInput formInput)
        {
            if (string.IsNullOrEmpty(formInput.ShortUrl))
                throw new HttpRequestException("The shortUrl provided is invalid!");

            var longUrl = _urlRetrievalService.RetrieveUrl(formInput.ShortUrl);

            return longUrl;
        }

    }
}
