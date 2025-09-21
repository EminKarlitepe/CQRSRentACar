using Microsoft.AspNetCore.Mvc;
using CQRSRentACar.CQRSPattern.Commands.CarCommands;
using CQRSRentACar.CQRSPattern.Handlers.CarHandlers;
using CQRSRentACar.CQRSPattern.Queries.CarQueries;
using CQRSRentACar.CQRSPattern.Handlers.AirportHandlers;
using CQRSRentACar.CQRSPattern.Queries.AirportQueries;
using CQRSRentACar.CQRSPattern.Handlers.CarRentalHandlers;
using CQRSRentACar.CQRSPattern.Queries.CarRentalQueries;
using CQRSRentACar.Services;

namespace CQRSRentACar.Controllers
{
    public class CarController : Controller
    {
        private readonly GetCarQueryHandler _getCarQueryHandler;
        private readonly GetCarByIdQueryHandler _getCarByIdQueryHandler;
        private readonly CreateCarCommandHandler _createCarCommandHandler;
        private readonly UpdateCarCommandHandler _updateCarCommandHandler;
        private readonly RemoveCarCommandHandler _removeCarCommandHandler;
        private readonly GetAirportByIdQueryHandler _getAirportByIdQueryHandler;
        private readonly GetRentedCarIdsQueryHandler _getRentedCarIdsQueryHandler;
        private readonly IAirportService _airportService;
        private readonly IChatGptService _chatGptService;

        public CarController(GetCarQueryHandler getCarQueryHandler, GetCarByIdQueryHandler getCarByIdQueryHandler, CreateCarCommandHandler createCarCommandHandler, UpdateCarCommandHandler updateCarCommandHandler, RemoveCarCommandHandler removeCarCommandHandler, GetAirportByIdQueryHandler getAirportByIdQueryHandler, GetRentedCarIdsQueryHandler getRentedCarIdsQueryHandler, IAirportService airportService, IChatGptService chatGptService)
        {
            _getCarQueryHandler = getCarQueryHandler;
            _getCarByIdQueryHandler = getCarByIdQueryHandler;
            _createCarCommandHandler = createCarCommandHandler;
            _updateCarCommandHandler = updateCarCommandHandler;
            _removeCarCommandHandler = removeCarCommandHandler;
            _getAirportByIdQueryHandler = getAirportByIdQueryHandler;
            _getRentedCarIdsQueryHandler = getRentedCarIdsQueryHandler;
            _airportService = airportService;
            _chatGptService = chatGptService;
        }

        public async Task<IActionResult> CarList()
        {
            var values = await _getCarQueryHandler.Handle();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar(CreateCarCommand command)
        {
            await _createCarCommandHandler.Handle(command);
            return RedirectToAction("CarList");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCar(int id)
        {
            await _removeCarCommandHandler.Handle(new RemoveCarCommand(id));
            return RedirectToAction("CarList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCar(int id)
        {
            var dto = await _getCarByIdQueryHandler.Handle(new GetCarByIdQuery(id));

            var command = new UpdateCarCommand
            {
                CarId = dto.CarId,
                CarName = dto.CarName,
                CarImageUrl = dto.CarImageUrl,
                Rating = dto.Rating,
                Price = dto.Price,
                Seat = dto.Seat,
                Transmission = dto.Transmission,
                CarType = dto.CarType,
                FuelType = dto.FuelType,
                ModelYear = dto.ModelYear,
                Gear = dto.Gear,
                Kilometer = dto.Kilometer
            };

            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCar(UpdateCarCommand command)
        {
            await _updateCarCommandHandler.Handle(command);
            return RedirectToAction("CarList");
        }

        [HttpGet]
        public async Task<IActionResult> SearchCars(string carType, int? pickUpAirportId, int? dropOffAirportId, DateTime? pickUpDate, DateTime? dropOffDate)
        {
            var allCars = await _getCarQueryHandler.Handle();
            
            var filteredCars = allCars.AsQueryable();
            
            if (!string.IsNullOrEmpty(carType))
            {
                filteredCars = filteredCars.Where(c => c.CarType == carType);
            }
            
            string pickUpLocationName = "";
            string dropOffLocationName = "";
            
            if (pickUpAirportId.HasValue)
            {
                var pickUpAirport = await _getAirportByIdQueryHandler.Handle(new GetAirportByIdQuery(pickUpAirportId.Value));
                pickUpLocationName = pickUpAirport.Name;
            }
            
            if (dropOffAirportId.HasValue)
            {
                var dropOffAirport = await _getAirportByIdQueryHandler.Handle(new GetAirportByIdQuery(dropOffAirportId.Value));
                dropOffLocationName = dropOffAirport.Name;
            }
            
            if (pickUpDate.HasValue && dropOffDate.HasValue)
            {
                var rentedCarIds = await GetRentedCarIdsAsync(pickUpDate.Value, dropOffDate.Value, pickUpLocationName, dropOffLocationName);
                filteredCars = filteredCars.Where(c => !rentedCarIds.Contains(c.CarId));
            }
            
            ViewBag.SearchParams = new
            {
                CarType = carType,
                PickUpAirportId = pickUpAirportId,
                DropOffAirportId = dropOffAirportId,
                PickUpLocation = pickUpLocationName,
                DropOffLocation = dropOffLocationName,
                PickUpDate = pickUpDate,
                DropOffDate = dropOffDate
            };
            
            return View("PublicCarList", filteredCars.ToList());
        }

        private async Task<List<int>> GetRentedCarIdsAsync(DateTime pickUpDate, DateTime dropOffDate, string? pickUpLocation = null, string? dropOffLocation = null)
        {
            return await _getRentedCarIdsQueryHandler.Handle(new GetRentedCarIdsQuery(pickUpDate, dropOffDate, pickUpLocation, dropOffLocation));
        }

        [HttpPost]
        public async Task<IActionResult> GetCarRecommendation([FromBody] CarRecommendationRequest request)
        {
            var allCarsResult = await _getCarQueryHandler.Handle();
            
            var allCars = allCarsResult.Select(c => new Entities.Car
            {
                CarId = c.CarId,
                CarName = c.CarName,
                CarImageUrl = c.CarImageUrl,
                Rating = c.Rating,
                Price = c.Price,
                Seat = c.Seat,
                Transmission = c.Transmission,
                CarType = c.CarType,
                FuelType = c.FuelType,
                ModelYear = c.ModelYear,
                Gear = c.Gear,
                Kilometer = c.Kilometer
            }).ToList();
            
            if (request.PickUpDate.HasValue && request.DropOffDate.HasValue)
            {
                var rentedCarIds = await GetRentedCarIdsAsync(
                    request.PickUpDate.Value, 
                    request.DropOffDate.Value, 
                    request.PickUpLocation, 
                    request.DropOffLocation
                );
                
                allCars = allCars.Where(c => !rentedCarIds.Contains(c.CarId)).ToList();
            }

            var recommendation = await _chatGptService.GetCarRecommendationAsync(request.Message, allCars);

            return Json(new { 
                success = true, 
                recommendation = recommendation,
                availableCarsCount = allCars.Count(),
                messageType = "CarRecommendation"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableCarsCount(DateTime? pickUpDate = null, DateTime? dropOffDate = null, string? pickUpLocation = null, string? dropOffLocation = null)
        {
            var allCarsResult = await _getCarQueryHandler.Handle();
            var allCars = allCarsResult.Select(c => new Entities.Car
            {
                CarId = c.CarId,
                CarName = c.CarName,
                CarImageUrl = c.CarImageUrl,
                Rating = c.Rating,
                Price = c.Price,
                Seat = c.Seat,
                Transmission = c.Transmission,
                CarType = c.CarType,
                FuelType = c.FuelType,
                ModelYear = c.ModelYear,
                Gear = c.Gear,
                Kilometer = c.Kilometer
            }).ToList();
            
            if (pickUpDate.HasValue && dropOffDate.HasValue)
            {
                var rentedCarIds = await GetRentedCarIdsAsync(
                    pickUpDate.Value, 
                    dropOffDate.Value, 
                    pickUpLocation, 
                    dropOffLocation
                );
                
                allCars = allCars.Where(c => !rentedCarIds.Contains(c.CarId)).ToList();
            }

            return Json(new { 
                success = true, 
                availableCarsCount = allCars.Count(),
                totalCarsCount = allCarsResult.Count()
            });
        }
    }

    public class CarRecommendationRequest
    {
        public string Message { get; set; } = "";
        public DateTime? PickUpDate { get; set; }
        public DateTime? DropOffDate { get; set; }
        public string? PickUpLocation { get; set; }
        public string? DropOffLocation { get; set; }
    }
}
