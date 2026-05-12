using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DesignServicesController(IStudioService studioService) : Controller
    {
        [HttpGet]
        public IActionResult Create() => View(new DesignServiceDTO());

        [HttpPost]
        public IActionResult Create(DesignServiceDTO service)
        {
            if (!ModelState.IsValid) return View(service);

            studioService.AddService(service);
            return RedirectToAction("Index", "Home");
        }
    }
}
