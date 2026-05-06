using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PL.Pages
{
    public class OrdersModel : PageModel
    {
        private readonly IStudioService _studioService;
        public IEnumerable<OrderDTO> Orders { get; set; } = [];

        public OrdersModel(IStudioService studioService)
        {
            _studioService = studioService;
        }

        public void OnGet()
        {
            Orders = _studioService.GetOrders();
        }
    }
}
