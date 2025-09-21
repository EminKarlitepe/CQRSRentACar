using CQRSRentACar.Models;
using Newtonsoft.Json;

namespace CQRSRentACar.Services
{
    public class AirportDistanceService : IAirportDistanceService
    {
        private readonly HttpClient _httpClient;
        private const double EarthRadiusKm = 6371;

        public AirportDistanceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AirportDistanceViewModel?> GetDistanceAsync(string iata1, string iata2)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://airport-distance-api-apiverve.p.rapidapi.com/v1/iata?iata1={iata1}&iata2={iata2}"),
                    Headers =
                    {
                        { "x-rapidapi-key", "your_key" },
                        { "x-rapidapi-host", "airport-distance-api-apiverve.p.rapidapi.com" },
                    },
                };

                using var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Distance API Response: {body}");
                
                var result = JsonConvert.DeserializeObject<AirportDistanceViewModel>(body);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting distance from API: {ex.Message}");
                return null;
            }
        }

        //Koordinatlar arasý mesafe Distance hesaplayabilmek için
        public double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return EarthRadiusKm * c;
        }

        private static double ToRadians(double angle) => angle * Math.PI / 180.0;
    }
}
