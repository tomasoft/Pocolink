using System.Threading.Tasks;

namespace Pocolink.UI
{
    public interface IShorteningClient
    {
        public Task<string> ShortenUrl(string longUrl);

        public Task<string> RetrieveUrl(string shortUrl);
    }
}