using System.Threading.Tasks;

namespace Pocolink.DAL
{
    public interface IKeyVaultService
    {
        public string RetrieveSecret(string secretKey);
    }
}