using CQRSRentACar.Models;

namespace CQRSRentACar.Services
{
    public interface IFuelPriceService
    {
        Task<FuelPriceViewModel?> GetFuelPricesAsync();
        Task<double?> GetFuelPriceByCountryAsync(string countryName);
    }
}
