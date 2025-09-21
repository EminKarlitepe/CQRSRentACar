using CQRSRentACar.CQRSPattern.Handlers.CarRentalHandlers;
using CQRSRentACar.CQRSPattern.Queries.CarRentalQueries;
using Microsoft.AspNetCore.Mvc;

namespace CQRSRentACar.Controllers
{
    public class AdminCarRentalController : Controller
    {
        private readonly GetCarRentalQueryHandler _getCarRentalQueryHandler;

        public AdminCarRentalController(GetCarRentalQueryHandler getCarRentalQueryHandler)
        {
            _getCarRentalQueryHandler = getCarRentalQueryHandler;
        }

        public async Task<IActionResult> Index()
        {
            var carRentals = await _getCarRentalQueryHandler.Handle();
            return View(carRentals);
        }
    }
}

