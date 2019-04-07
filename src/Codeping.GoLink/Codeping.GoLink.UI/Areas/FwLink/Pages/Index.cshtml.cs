using Codeping.GoLink.Core;
using Codeping.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;

namespace Codeping.GoLink.UI.Areas.FwLink.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IGoLinkService _service;

        public IndexModel(IGoLinkService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGetAsync(string linkId)
        {
            if (!linkId.IsEmpty())
            {
                var link = await _service.ToLongAsync(linkId);

                if (link.Succeeded)
                {
                    return Redirect(link.Value);
                }
            }

            return BadRequest();
        }
    }
}