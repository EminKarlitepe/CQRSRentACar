using Microsoft.AspNetCore.Mvc;
using CQRSRentACar.Services;

namespace CQRSRentACar.Controllers
{
    public class FuelPriceController : Controller
    {
        private readonly IFuelPriceService _fuelPriceService;

        public FuelPriceController(IFuelPriceService fuelPriceService)
        {
            _fuelPriceService = fuelPriceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFuelPrices()
        {
            var fuelPrices = await _fuelPriceService.GetFuelPricesAsync();
            return Json(fuelPrices);
        }
    }
}

