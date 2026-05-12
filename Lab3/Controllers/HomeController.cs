using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class HomeController(IStudioService studioService) : Controller
    {
        public IActionResult Index()
        {
            var services = studioService.GetAllServices();
            return View(services);
        }
    }
}
