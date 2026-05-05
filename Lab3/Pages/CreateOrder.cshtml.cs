using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PL.Pages
{
    public class CreateOrderModel : PageModel
    {
        private readonly IStudioService _studioService;

        [BindProperty]
        public OrderDTO Order { get; set; } = new OrderDTO();

        public IEnumerable<DesignServiceDTO> AvailableServices { get; set; } = new List<DesignServiceDTO>();

        public CreateOrderModel(IStudioService studioService)
        {
            _studioService = studioService;
        }

        public void OnGet()
        {
            AvailableServices = _studioService.GetServices().Where(s => !s.IsCustom);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                AvailableServices = _studioService.GetServices().Where(s => !s.IsCustom);
                return Page();
            }

            try
            {
                _studioService.MakeOrder(Order);
                return RedirectToPage("/Success");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                AvailableServices = _studioService.GetServices().Where(s => !s.IsCustom);
                return Page();
            }
        }
    }
}