using CQRSRentACar.Entities;

namespace CQRSRentACar.Services
{
    public interface IChatGptService
    {
        Task<string> GetResponseAsync(string userMessage, string userEmail);
        Task<string> GetCarRecommendationAsync(string userMessage, List<Car> availableCars);
        Task<string> GetRealTimeSupportAsync(string userMessage, string userEmail);
    }
}
