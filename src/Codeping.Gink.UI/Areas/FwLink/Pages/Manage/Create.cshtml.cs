using Codeping.Gink.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Codeping.Gink.UI.Areas.FwLink.Pages.Manage
{
    public class CreateModel : PageModel
    {
        private readonly IGoLinkService _service;

        public CreateModel(IGoLinkService service)
        {
            _service = service;
        }

        [BindProperty]
        public Link Link { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _service.ToShortAsync(this.Link.LongUrl);

            if (result.Succeeded)
            {
                this.Link.Id = Url.Page("/FwLink?LinkId=" + result.Value);
            }
            else
            {
                ModelState.AddModelError("", result.ErrorMessage);
            }

            return Page();
        }
    }
}