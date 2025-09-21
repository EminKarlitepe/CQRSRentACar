using CQRSRentACar.CQRSPattern.Handlers.CarHandlers;
using Microsoft.AspNetCore.Mvc;

namespace CQRSRentACar.ViewComponents
{
    public class _PopularCarsComponentPartial : ViewComponent
    {
        private readonly GetCarQueryHandler _getCarQueryHandler;

        public _PopularCarsComponentPartial(GetCarQueryHandler getCarQueryHandler)
        {
            _getCarQueryHandler = getCarQueryHandler;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cars = await _getCarQueryHandler.Handle();
            var popularCars = cars.OrderByDescending(c => c.Rating).Take(3).ToList();
            
            return View(popularCars);
        }
    }
}

