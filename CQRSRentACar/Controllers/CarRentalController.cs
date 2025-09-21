using Microsoft.AspNetCore.Mvc;
using CQRSRentACar.CQRSPattern.Commands.CarRentalCommands;
using CQRSRentACar.CQRSPattern.Handlers.CarRentalHandlers;
using CQRSRentACar.CQRSPattern.Handlers.CarHandlers;
using CQRSRentACar.CQRSPattern.Handlers.AirportHandlers;
using CQRSRentACar.CQRSPattern.Queries.CarQueries;
using CQRSRentACar.CQRSPattern.Queries.AirportQueries;
using CQRSRentACar.Services;

namespace CQRSRentACar.Controllers
{
    public class CarRentalController : Controller
    {
        private readonly CreateCarRentalCommandHandler _createCarRentalCommandHandler;
        private readonly GetCarByIdQueryHandler _getCarByIdQueryHandler;
        private readonly GetAirportByIdQueryHandler _getAirportByIdQueryHandler;
        private readonly IAirportDistanceService _airportDistanceService;
        private readonly IFuelPriceService _fuelPriceService;

        public CarRentalController(
            CreateCarRentalCommandHandler createCarRentalCommandHandler,
            GetCarByIdQueryHandler getCarByIdQueryHandler,
            GetAirportByIdQueryHandler getAirportByIdQueryHandler,
            IAirportDistanceService airportDistanceService,
            IFuelPriceService fuelPriceService)
        {
            _createCarRentalCommandHandler = createCarRentalCommandHandler;
            _getCarByIdQueryHandler = getCarByIdQueryHandler;
            _getAirportByIdQueryHandler = getAirportByIdQueryHandler;
            _airportDistanceService = airportDistanceService;
            _fuelPriceService = fuelPriceService;
        }

        [HttpGet]
        public async Task<IActionResult> RentCar(int carId, int? pickUpAirportId, int? dropOffAirportId, DateTime? pickUpDate, DateTime? dropOffDate, string? selectedCountry = "Turkey")
        {
            var car = await _getCarByIdQueryHandler.Handle(new GetCarByIdQuery(carId));

            string pickUpLocationName = "";
            string dropOffLocationName = "";
            string pickUpIata = "";
            string dropOffIata = "";
            
            if (pickUpAirportId.HasValue && pickUpAirportId.Value > 0)
            {
                var pickUpAirport = await _getAirportByIdQueryHandler.Handle(new GetAirportByIdQuery(pickUpAirportId.Value));
                pickUpLocationName = pickUpAirport?.Name ?? "";
                pickUpIata = pickUpAirport?.Iata ?? "";
            }
            
            if (dropOffAirportId.HasValue && dropOffAirportId.Value > 0)
            {
                var dropOffAirport = await _getAirportByIdQueryHandler.Handle(new GetAirportByIdQuery(dropOffAirportId.Value));
                dropOffLocationName = dropOffAirport?.Name ?? "";
                dropOffIata = dropOffAirport?.Iata ?? "";
            }

            double distanceKm = 0;
            double distanceCost = 0;
            
            if (!string.IsNullOrEmpty(pickUpIata) && !string.IsNullOrEmpty(dropOffIata) && pickUpIata != dropOffIata)
            {
                var distanceResult = await _airportDistanceService.GetDistanceAsync(pickUpIata, dropOffIata);
                
                if (distanceResult?.Data != null)
                {
                    distanceKm = distanceResult.Data.DistanceKm;
                    
                    var fuelPrices = await _fuelPriceService.GetFuelPricesAsync();
                    var turkeyData = fuelPrices?.Result?.FirstOrDefault(c => c.Country == "Turkey");
                    
                    double fuelPriceInEuro = 1.5;
                    
                    if (turkeyData != null)
                    {
                        if (car.FuelType.ToLower().Contains("dizel"))
                        {
                            fuelPriceInEuro = turkeyData.DieselPrice;
                        }
                        else if (car.FuelType.ToLower().Contains("benzin"))
                        {
                            fuelPriceInEuro = turkeyData.GasolinePrice;
                        }
                    }
                    
                    double fuelPriceInTL = fuelPriceInEuro * 48;
                    distanceCost = Math.Round(distanceKm * fuelPriceInTL * 0.1);
                }
            }


            var allFuelPrices = await _fuelPriceService.GetFuelPricesAsync();
            var turkeyFuelData = allFuelPrices?.Result?.FirstOrDefault(c => c.Country == "Turkey");
            
            double fuelPricePerKmInTL = 0;
            if (turkeyFuelData != null)
            {
                double fuelPriceInEuro = car.FuelType.ToLower().Contains("dizel") ? turkeyFuelData.DieselPrice : turkeyFuelData.GasolinePrice;
                fuelPricePerKmInTL = fuelPriceInEuro * 48 * 0.1;
            }

            ViewBag.Car = car;
            ViewBag.PickUpAirportId = pickUpAirportId;
            ViewBag.DropOffAirportId = dropOffAirportId;
            ViewBag.PickUpLocation = pickUpLocationName;
            ViewBag.DropOffLocation = dropOffLocationName;
            ViewBag.PickUpDate = pickUpDate;
            ViewBag.DropOffDate = dropOffDate;
            ViewBag.DistanceKm = distanceKm;
            ViewBag.DistanceCost = distanceCost;
            ViewBag.FuelPrices = allFuelPrices;
            ViewBag.SelectedCountry = selectedCountry;
            ViewBag.FuelPricePerKm = fuelPricePerKmInTL;
            ViewBag.SearchParams = new
            {
                CarType = car.CarType,
                PickUpAirportId = pickUpAirportId,
                DropOffAirportId = dropOffAirportId,
                PickUpLocation = pickUpLocationName,
                DropOffLocation = dropOffLocationName,
                PickUpDate = pickUpDate,
                DropOffDate = dropOffDate
            };

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RentCar(CreateCarRentalCommand command)
        {
            await _createCarRentalCommandHandler.Handle(command);
            
            TempData["Success"] = "Araç başarıyla kiralandı!";
            return Json(new { success = true, message = "Araç başarıyla kiralandı!" });
        }

        [HttpGet]
        public IActionResult RentalSuccess()
        {
            return View();
        }
    }
}
