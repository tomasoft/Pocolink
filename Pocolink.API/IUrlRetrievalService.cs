using System.Threading.Tasks;

namespace Pocolink.API
{
    public interface IUrlRetrievalService
    {
        public string RetrieveUrl(string sUrl);
    }
}