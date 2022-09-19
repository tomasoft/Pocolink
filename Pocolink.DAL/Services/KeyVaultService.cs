using System;

using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace Pocolink.DAL.Services
{
    public class KeyVaultService : IKeyVaultService
    {
        public string RetrieveSecret(string secretKey)
        {
            var kvUri = "https://payrockeyvault.vault.azure.net/";

            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

            var secret = client.GetSecret(secretKey);

            return secret.Value.Value;
        }
    }
}
