using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Pocolink.API.Services
{
    public class HashingService : IHashingService
    {
        public string HashUrl(string url)
        {
            return string.Join("", SHA512.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(url))
                .Select(item => item.ToString("x2")));
        }
    }
}
