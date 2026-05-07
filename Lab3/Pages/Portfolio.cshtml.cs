using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PL.Pages
{
    public class PortfolioModel(IStudioService studioService) : PageModel
    {
        public IEnumerable<PortfolioItemDTO> PortfolioItems { get; set; } = [];

        public void OnGet()
        {
            PortfolioItems = studioService.GetPortfolioItems();
        }
    }
}
