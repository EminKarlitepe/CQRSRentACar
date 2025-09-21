using CQRSRentACar.Entities;
using System.Net.Http;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.Services
{
    public class AirportService : IAirportService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "your_key";

        public AirportService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Airport>> SearchAirportsAsync(string searchTerm)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://airports15.p.rapidapi.com/airports?name={searchTerm}&page=1&page_size=20&sorted_by=icao"),
                Headers =
                {
                    { "x-rapidapi-key", _apiKey },
                    { "x-rapidapi-host", "airports15.p.rapidapi.com" },
                },
            };
            using var response = await _httpClient.SendAsync(request);
            var body = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<JsonElement>(body);
            var list = new List<Airport>();
            foreach (var a in json.GetProperty("data").EnumerateArray())
            {
                list.Add(new Airport
                {
                    Name = a.GetProperty("name").GetString(),
                    City = a.GetProperty("city").GetString(),
                    Iata = a.GetProperty("iata_code").GetString(),
                    Icao = a.GetProperty("icao_code").GetString(),
                    CountryIso = a.GetProperty("country_code").GetString(),
                    Latitude = a.GetProperty("lat").GetDouble(),
                    Longitude = a.GetProperty("lon").GetDouble(),
                    IsActive = true,
                    CreatedDate = DateTime.Now
                });
            }
            return list;
        }

        public async Task<List<Airport>> GetTurkishAirportsAsync()
        {
            return await SearchAirportsAsync("Turkey");
        }

        public async Task<List<Airport>> GetWorldwideAirportsAsync()
        {
            return await SearchAirportsAsync("World");
        }

        public async Task<Airport?> GetAirportByIataAsync(string iata)
        {
            var airports = await SearchAirportsAsync(iata);
            return airports.FirstOrDefault(a => a.Iata == iata);
        }

        public async Task<List<Airport>> SearchTurkishAirportsAsync(string searchTerm)
        {
            var airports = await SearchAirportsAsync(searchTerm);
            return airports.Where(a => a.CountryIso == "TR").ToList();
        }

        public async Task<List<Airport>> GetAirportsByCityFromApiAsync(string city)
        {
            var airports = await SearchAirportsAsync(city);
            var savedAirports = new List<Airport>();
            foreach (var airport in airports)
            {
                var savedAirport = await AddAirportAsync(airport);
                savedAirports.Add(savedAirport);
            }
            return savedAirports;
        }

        public async Task<Airport> AddAirportAsync(Airport airport)
        {
            using var context = new CQRSRentACar.Context.CQRSContext();
            var existingAirport = context.Airports.FirstOrDefault(a => a.Iata == airport.Iata);
            if (existingAirport != null)
            {
                return existingAirport;
            }
            
            airport.AirportId = 0;
            context.Airports.Add(airport);
            await context.SaveChangesAsync();
            
            return context.Airports.FirstOrDefault(a => a.Iata == airport.Iata) ?? airport;
        }

        public async Task SaveAllTurkishAirportsAsync()
        {
            var airports = await SearchAirportsAsync("Turkey");
            foreach (var airport in airports.Where(a => a.CountryIso == "TR"))
            {
                await AddAirportAsync(airport);
            }
        }
    }
}
