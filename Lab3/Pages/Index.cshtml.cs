using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PL.Pages
{
    public class IndexModel(IStudioService studioService) : PageModel
    {
        public IEnumerable<DesignServiceDTO> Services { get; set; } = [];

        public void OnGet()
        {
            Services = studioService.GetServices();
        }
    }
}