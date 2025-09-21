using CQRSRentACar.Models;

namespace CQRSRentACar.Services
{
    public interface IAirportDistanceService
    {
        Task<AirportDistanceViewModel?> GetDistanceAsync(string iata1, string iata2);
        double CalculateDistance(double lat1, double lon1, double lat2, double lon2);
    }
}
