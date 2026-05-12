using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class OrdersController(IStudioService studioService) : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AvailableServices = studioService.GetServices();
            return View(new OrderDTO());
        }

        [HttpPost]
        public IActionResult Create(OrderDTO order)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AvailableServices = studioService.GetServices();
                return View(order);
            }

            try
            {
                studioService.MakeOrder(order);
                return RedirectToAction("Success");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewBag.AvailableServices = studioService.GetServices();
                return View(order);
            }
        }

        [HttpGet]
        public IActionResult Success() => View();

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Index()
        {
            var orders = studioService.GetOrders();
            return View(orders);
        }
    }
}
