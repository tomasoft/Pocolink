using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Pocolink.Models.Models;

namespace Pocolink.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IShorteningClient _shorteningClient;
        
        [BindProperty] public FormInput FormInput { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IShorteningClient shorteningClient)
        {
            _logger = logger;
            _shorteningClient = shorteningClient;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!Request.QueryString.HasValue) return Page();
            
            var result = await _shorteningClient.RetrieveUrl(Request.QueryString.Value.Split('=')[1]);

            return new RedirectResult(result);
        }

        [ViewData]
        public string ShorteningResult { get; set; }

        public async Task<IActionResult> OnPostAsync(FormInput formInput)
        {
            var result = await _shorteningClient.ShortenUrl(formInput.LongUrl);

            FormInput.ShortUrl = result;
            @ViewData["ShorteningResult"] = FormInput.ShortUrl;
            
            return Page();
        }
    }
}
