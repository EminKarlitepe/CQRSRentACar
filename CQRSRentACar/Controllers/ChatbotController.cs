using CQRSRentACar.Services;
using CQRSRentACar.CQRSPattern.Handlers.CarHandlers;
using CQRSRentACar.CQRSPattern.Queries.CarQueries;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CQRSRentACar.Controllers
{
    public class ChatbotController : Controller
    {
        private readonly IChatGptService _chatGptService;
        private readonly GetCarQueryHandler _getCarQueryHandler;

        public ChatbotController(
            IChatGptService chatGptService, 
            GetCarQueryHandler getCarQueryHandler)
        {
            _chatGptService = chatGptService;
            _getCarQueryHandler = getCarQueryHandler;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] ChatbotRequest request)
        {
            string response;

            var messageType = DetermineMessageType(request.Message);

            switch (messageType)
            {
                case MessageType.CarRecommendation:
                    var carsResult = await _getCarQueryHandler.Handle();
                    var cars = carsResult.Select(c => new Entities.Car
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
                    response = await _chatGptService.GetCarRecommendationAsync(request.Message, cars);
                    break;

                case MessageType.RealTimeSupport:
                    response = await _chatGptService.GetRealTimeSupportAsync(request.Message, request.UserEmail ?? "");
                    break;

                default:
                    response = await _chatGptService.GetResponseAsync(request.Message, request.UserEmail ?? "");
                    break;
            }

            return Json(new { success = true, response = response, messageType = messageType.ToString() });
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        private MessageType DetermineMessageType(string message)
        {
            var lowerMessage = message.ToLowerInvariant();

            var carRecommendationKeywords = new[]
            {
                "araç öner", "araba öner", "hangi araç", "hangi araba", "öneri", "tavsiye",
                "ekonomik", "lüks", "konfor", "kişilik", "yakıt", "şanzıman", "manuel", "otomatik",
                "fiyat", "ucuz", "pahalı", "bütçe", "aralık", "kategori", "tip", "model"
            };

            var realTimeSupportKeywords = new[]
            {
                "acil", "sorun", "problem", "yardım", "destek", "rezervasyon", "iptal", "teslimat",
                "teknik", "arıza", "çalışmıyor", "hata", "beklemiyor", "gecikme", "kayıp", "bulamıyorum",
                "nasıl", "ne yapmalı", "çözüm", "düzelt", "onarım", "servis"
            };

            if (carRecommendationKeywords.Any(keyword => lowerMessage.Contains(keyword)))
            {
                return MessageType.CarRecommendation;
            }

            if (realTimeSupportKeywords.Any(keyword => lowerMessage.Contains(keyword)))
            {
                return MessageType.RealTimeSupport;
            }

            return MessageType.General;
        }
    }

    public class ChatbotRequest
    {
        public string Message { get; set; } = "";
        public string? UserEmail { get; set; }
    }

    public enum MessageType
    {
        General,
        CarRecommendation,
        RealTimeSupport
    }
}
