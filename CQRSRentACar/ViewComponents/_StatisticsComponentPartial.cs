using CQRSRentACar.CQRSPattern.Handlers.CarHandlers;
using CQRSRentACar.CQRSPattern.Handlers.CarRentalHandlers;
using CQRSRentACar.CQRSPattern.Handlers.AirportHandlers;
using CQRSRentACar.CQRSPattern.Queries.AirportQueries;
using Microsoft.AspNetCore.Mvc;

namespace CQRSRentACar.ViewComponents
{
    public class _StatisticsComponentPartial : ViewComponent
    {
        private readonly GetCarQueryHandler _getCarQueryHandler;
        private readonly GetCarRentalQueryHandler _getCarRentalQueryHandler;
        private readonly GetAirportQueryHandler _getAirportQueryHandler;

        public _StatisticsComponentPartial(
            GetCarQueryHandler getCarQueryHandler,
            GetCarRentalQueryHandler getCarRentalQueryHandler,
            GetAirportQueryHandler getAirportQueryHandler)
        {
            _getCarQueryHandler = getCarQueryHandler;
            _getCarRentalQueryHandler = getCarRentalQueryHandler;
            _getAirportQueryHandler = getAirportQueryHandler;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cars = await _getCarQueryHandler.Handle();
            var carRentals = await _getCarRentalQueryHandler.Handle();
            var airports = await _getAirportQueryHandler.Handle(new GetAirportQuery());

            var averageRating = cars.Count > 0 ? Math.Round(cars.Average(c => c.Rating) / 10, 1) : 0;

            var statistics = new
            {
                AverageRating = averageRating,
                NumberOfCars = cars.Count,
                CarCenters = airports.Count,
                TotalKilometers = cars.Sum(c => c.Kilometer)
            };

            return View(statistics);
        }
    }
}
