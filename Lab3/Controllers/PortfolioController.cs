using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PortfolioController(IStudioService studioService) : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Services = studioService.GetAllServices();
            return View();
        }

        [HttpPost]
        public IActionResult Create(PortfolioItemDTO model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Services = studioService.GetAllServices();
                return View(model);
            }

            studioService.CreatePortfolioItem(model);
            return RedirectToPage("/Portfolio");
        }
    }
}
