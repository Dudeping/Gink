using Codeping.Gink.Core;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using X.PagedList;
using System.Linq;

namespace Codeping.Gink.UI.Areas.FwLink.Pages.Manage
{
    public class IndexModel : PageModel
    {
        private readonly IGinkService _service;

        public IndexModel(IGinkService service)
        {
            _service = service;
        }

        public IPagedList<Link> Links { get; set; }

        public async Task OnGetAsync(int pageNumber)
        {
            this.Links = await _service.Where(x => true)
                .OrderByDescending(x => x.Total)
                .ToPagedListAsync(pageNumber, 50);
        }
    }
}