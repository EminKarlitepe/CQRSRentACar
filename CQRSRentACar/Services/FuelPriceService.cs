using CQRSRentACar.Models;
using Newtonsoft.Json;

namespace CQRSRentACar.Services
{
    public class FuelPriceService : IFuelPriceService
    {
        private readonly HttpClient _httpClient;

        public FuelPriceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<FuelPriceViewModel?> GetFuelPricesAsync()
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://gas-price.p.rapidapi.com/europeanCountries"),
                    Headers =
                    {
                        { "x-rapidapi-key", "your_key" },
                        { "x-rapidapi-host", "gas-price.p.rapidapi.com" },
                    },
                };

                using var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var body = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<FuelPriceViewModel>(body);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting fuel prices: {ex.Message}");
                return null;
            }
        }

        public async Task<double?> GetFuelPriceByCountryAsync(string countryName)
        {
            try
            {
                var fuelPrices = await GetFuelPricesAsync();
                if (fuelPrices?.Success == true && fuelPrices.Result != null)
                {
                    var countryData = fuelPrices.Result.FirstOrDefault(c => 
                        c.Country?.Equals(countryName, StringComparison.OrdinalIgnoreCase) ?? false);
                    
                    return countryData?.DieselPrice;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting fuel price for {countryName}: {ex.Message}");
                return null;
            }
        }
    }
}
