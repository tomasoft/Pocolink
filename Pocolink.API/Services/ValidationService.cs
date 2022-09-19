using System.Text.RegularExpressions;

namespace Pocolink.API.Services
{
    public class ValidationService : IValidationService
    {
        public bool ValidateUrl(string url)
        {
            var isValid = Regex.IsMatch(url, "^(?:http(s)?:\\/\\/)?[\\w.-]+(?:\\.[\\w\\.-]+)+[\\w\\-\\._~:/?#[\\]@!\\$&'\\(\\)\\*\\+,;=.]+$");
            return isValid && !string.IsNullOrWhiteSpace(url);
        }
    }
}
