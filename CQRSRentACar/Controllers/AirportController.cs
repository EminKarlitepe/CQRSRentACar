using Microsoft.AspNetCore.Mvc;
using CQRSRentACar.Services;

namespace CQRSRentACar.Controllers
{
    public class AirportController : Controller
    {
        private readonly IAirportService _airportService;
        private readonly IAirportDistanceService _airportDistanceService;

        public AirportController(IAirportService airportService, IAirportDistanceService airportDistanceService)
        {
            _airportService = airportService;
            _airportDistanceService = airportDistanceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAirportsByCity(string city)
        {
            var airports = await _airportService.GetAirportsByCityFromApiAsync(city);
            var result = airports.Select(a => new
            {
                airportId = a.AirportId,
                name = a.Name,
                city = a.City,
                iata = a.Iata,
                country = a.Country
            }).ToList();

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAirports()
        {
            var airports = await _airportService.GetWorldwideAirportsAsync();
            var result = airports.Select(a => new
            {
                id = a.AirportId,
                name = a.Name,
                city = a.City,
                iata = a.Iata,
                country = a.Country
            }).ToList();

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTurkishAirports()
        {
            var airports = await _airportService.GetTurkishAirportsAsync();
            var result = airports.Select(a => new
            {
                id = a.AirportId,
                name = a.Name,
                city = a.City,
                iata = a.Iata,
                country = a.Country
            }).ToList();

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> SearchTurkishAirports(string searchTerm)
        {
            var airports = await _airportService.SearchTurkishAirportsAsync(searchTerm);
            var result = airports.Select(a => new
            {
                id = a.AirportId,
                name = a.Name,
                city = a.City,
                iata = a.Iata,
                country = a.Country
            }).ToList();

            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAllTurkishAirports()
        {
            await _airportService.SaveAllTurkishAirportsAsync();
            return Json(new { success = true, message = "Tüm Türk havaalanları başarıyla kaydedildi." });
        }

        [HttpGet]
        public async Task<IActionResult> LoadTurkishAirports()
        {
            await _airportService.SaveAllTurkishAirportsAsync();
            return Json(new { success = true, message = "Türk havaalanları yüklendi." });
        }

        [HttpGet]
        public async Task<IActionResult> GetDistanceBetweenAirports(string iata1, string iata2)
        {
            var distanceResult = await _airportDistanceService.GetDistanceAsync(iata1, iata2);

            if (distanceResult?.Data == null)
            {
                return Json(new { error = "Mesafe bilgisi alınamadı" });
            }

            var result = new
            {
                distanceKm = Math.Round(distanceResult.Data.DistanceKm, 2),
                distanceMiles = Math.Round(distanceResult.Data.DistanceMiles, 2),
                airport1 = new
                {
                    name = distanceResult.Data.Airport1?.Name ?? "",
                    iata = distanceResult.Data.Airport1?.Iata ?? "",
                    city = distanceResult.Data.Airport1?.City ?? ""
                },
                airport2 = new
                {
                    name = distanceResult.Data.Airport2?.Name ?? "",
                    iata = distanceResult.Data.Airport2?.Iata ?? "",
                    city = distanceResult.Data.Airport2?.City ?? ""
                }
            };

            return Json(result);
        }
    }
}
