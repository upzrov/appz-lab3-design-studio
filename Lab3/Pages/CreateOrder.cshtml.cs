using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PL.Pages
{
    public class CreateOrderModel(IStudioService studioService) : PageModel
    {
        [BindProperty]
        public OrderDTO Order { get; set; } = new OrderDTO();

        public IEnumerable<DesignServiceDTO> AvailableServices { get; set; } = [];

        public void OnGet()
        {
            AvailableServices = studioService.GetServices();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                AvailableServices = studioService.GetServices();
                return Page();
            }

            try
            {
                studioService.MakeOrder(Order);
                return RedirectToPage("/Success");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                AvailableServices = studioService.GetServices();
                return Page();
            }
        }
    }
}