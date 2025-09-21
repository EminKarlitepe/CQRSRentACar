using CQRSRentACar.Entities;

namespace CQRSRentACar.Services
{
    public interface IAirportService
    {
        Task<List<Airport>> GetTurkishAirportsAsync();
        Task<List<Airport>> GetWorldwideAirportsAsync();
        Task<Airport?> GetAirportByIataAsync(string iata);
        Task<List<Airport>> SearchAirportsAsync(string searchTerm);
        Task<List<Airport>> SearchTurkishAirportsAsync(string searchTerm);
        Task<List<Airport>> GetAirportsByCityFromApiAsync(string city);
        Task<Airport> AddAirportAsync(Airport airport);
        Task SaveAllTurkishAirportsAsync();
    }
}
