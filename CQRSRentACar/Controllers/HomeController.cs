using System.Diagnostics;
using CQRSRentACar.Models;
using Microsoft.AspNetCore.Mvc;
using CQRSRentACar.Services;

namespace CQRSRentACar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFuelPriceService _fuelPriceService;

        public HomeController(ILogger<HomeController> logger, IFuelPriceService fuelPriceService)
        {
            _logger = logger;
            _fuelPriceService = fuelPriceService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Dashboard()
        {
            var fuelPrices = await _fuelPriceService.GetFuelPricesAsync();
            ViewBag.FuelPrices = fuelPrices;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
