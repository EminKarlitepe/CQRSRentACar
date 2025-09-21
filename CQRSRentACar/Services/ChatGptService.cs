using System.Net.Http.Headers;
using System.Text.Json;
using CQRSRentACar.Entities;
using Microsoft.Extensions.Logging;

namespace CQRSRentACar.Services
{
    public class ChatGptService : IChatGptService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ChatGptService> _logger;
        private readonly string _apiKey;
        private readonly string _apiHost;

        public ChatGptService(HttpClient httpClient, ILogger<ChatGptService> logger, string apiKey, string apiHost)
        {
            _httpClient = httpClient;
            _logger = logger;
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            _apiHost = apiHost ?? throw new ArgumentNullException(nameof(apiHost));
        }

        public async Task<string> GetResponseAsync(string userMessage, string userEmail)
        {
            try
            {
                _logger.LogInformation($"Sending request to ChatGPT API for message: {userMessage}");

                string cleanMessage = CleanUserMessage(userMessage);
                string detectedLanguage = DetectLanguage(cleanMessage);
                _logger.LogInformation($"Detected language: {detectedLanguage}");

                var response = await SendRequestToChatGptAsync(cleanMessage, detectedLanguage);
                return await HandleResponseAsync(response, detectedLanguage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while calling ChatGPT service.");
                return GetDefaultResponse("tr");
            }
        }

        private async Task<HttpResponseMessage> SendRequestToChatGptAsync(string cleanMessage, string detectedLanguage)
        {
            var request = CreateChatGptRequest(cleanMessage, detectedLanguage);

            using var response = await _httpClient.SendAsync(request);
            return response;
        }

        private HttpRequestMessage CreateChatGptRequest(string cleanMessage, string detectedLanguage)
        {
            var requestContent = new
            {
                messages = new[]
                {
                    new
                    {
                        role = "system",
                        content = GetSystemPrompt(detectedLanguage)
                    },
                    new
                    {
                        role = "user",
                        content = cleanMessage
                    }
                },
                web_access = false,
                stream = false
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://chatgpt-42.p.rapidapi.com/chatgpt"),
                Headers =
                {
                    { "x-rapidapi-key", _apiKey },
                    { "x-rapidapi-host", _apiHost }
                },
                Content = new StringContent(JsonSerializer.Serialize(requestContent))
            };

            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return request;
        }

        private string CleanUserMessage(string message)
        {
            string cleanMessage = message?.Trim();
            if (string.IsNullOrEmpty(cleanMessage))
            {
                cleanMessage = "Araç kiralama hizmetleriniz hakkında bilgi almak istiyorum.";
            }
            return cleanMessage;
        }

        private string DetectLanguage(string text)
        {
            var turkishChars = new[] { 'ç', 'ğ', 'ı', 'ö', 'ş', 'ü', 'Ç', 'Ğ', 'İ', 'Ö', 'Ş', 'Ü' };
            return turkishChars.Any(c => text.Contains(c)) ? "tr" : "en";
        }

        private string GetSystemPrompt(string language)
        {
            return language == "tr" ? GetTurkishSystemPrompt() : GetEnglishSystemPrompt();
        }

        private string GetTurkishSystemPrompt() =>
            @"Sen CQRS Rent A Car şirketinin resmi müşteri hizmetleri temsilcisisin. 
            Profesyonel, saygılı ve yardımsever bir yaklaşım sergile.
            Araç kiralama konusunda uzman bir danışmansın ve şirket politikalarını iyi biliyorsun.
            Müşterinin ihtiyaçlarını anlayıp en uygun çözümü sun.
            Her zaman şirket adına konuştuğunu unutma ve resmi bir dil kullan.
            Müşteri memnuniyeti senin önceliğin.
            Türkçe cevap ver.
            Eğer soru belirsizse, araç kiralama hizmetlerimiz hakkında detaylı bilgi ver.";

        private string GetEnglishSystemPrompt() =>
            @"You are an official customer service representative for CQRS Rent A Car company.
            Maintain a professional, respectful and helpful approach.
            You are an expert consultant in car rental and know company policies well.
            Understand customer needs and provide the most suitable solution.
            Always remember that you speak on behalf of the company and use formal language.
            Customer satisfaction is your priority.
            Answer in English.
            If the question is unclear, provide detailed information about our car rental services.";

        private async Task<string> HandleResponseAsync(HttpResponseMessage response, string detectedLanguage)
        {
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Error from API: {response.StatusCode} - {responseBody}");
                return GetDefaultResponse(detectedLanguage);
            }

            return ParseResponse(responseBody, detectedLanguage);
        }

        private string ParseResponse(string responseBody, string language)
        {
            try
            {
                var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseBody);

                if (jsonResponse.TryGetProperty("choices", out var choices) && choices.ValueKind == JsonValueKind.Array && choices.GetArrayLength() > 0)
                {
                    var firstChoice = choices[0];
                    if (firstChoice.TryGetProperty("message", out var message) && message.TryGetProperty("content", out var content))
                    {
                        var result = content.GetString();
                        return result ?? GetDefaultResponse(language);
                    }
                }

                return responseBody.Length > 500 ? responseBody.Substring(0, 500) + "..." : responseBody;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while parsing response.");
                return GetDefaultResponse(language);
            }
        }

        private string GetDefaultResponse(string language)
        {
            var responses = language == "tr"
                ? new[] { "Ekonomik araç: 200 TL/gün", "Araç kategorileri: Ekonomik, Konfor, Lüks" }
                : new[] { "Economy car: 200 TL/day", "Car categories: Economy, Comfort, Luxury" };

            var random = new Random();
            return responses[random.Next(responses.Length)];
        }

        public async Task<string> GetCarRecommendationAsync(string userMessage, List<Car> availableCars)
        {
            try
            {
                _logger.LogInformation($"Getting car recommendation for message: {userMessage}");

                string cleanMessage = CleanUserMessage(userMessage);
                string detectedLanguage = DetectLanguage(cleanMessage);

                var carInfo = string.Join(", ", availableCars.Select(car => 
                    $"{car.Brand} {car.Model} - {car.DailyPrice} TL/gün"));

                var recommendationPrompt = detectedLanguage == "tr" 
                    ? $"Müşteri mesajı: {cleanMessage}\n\nMevcut araçlar: {carInfo}\n\nBu araçlar arasından müşteri için en uygun aracı öner."
                    : $"Customer message: {cleanMessage}\n\nAvailable cars: {carInfo}\n\nRecommend the most suitable car for the customer from these options.";

                var response = await SendRequestToChatGptAsync(recommendationPrompt, detectedLanguage);
                return await HandleResponseAsync(response, detectedLanguage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting car recommendation.");
                return GetDefaultResponse("tr");
            }
        }

        public async Task<string> GetRealTimeSupportAsync(string userMessage, string userEmail)
        {
            try
            {
                _logger.LogInformation($"Providing real-time support for user: {userEmail}, message: {userMessage}");

                string cleanMessage = CleanUserMessage(userMessage);
                string detectedLanguage = DetectLanguage(cleanMessage);

                var supportPrompt = detectedLanguage == "tr"
                    ? $"Müşteri email: {userEmail}\nMüşteri mesajı: {cleanMessage}\n\nBu müşteriye gerçek zamanlı destek sağla ve sorununu çöz."
                    : $"Customer email: {userEmail}\nCustomer message: {cleanMessage}\n\nProvide real-time support to this customer and solve their issue.";

                var response = await SendRequestToChatGptAsync(supportPrompt, detectedLanguage);
                return await HandleResponseAsync(response, detectedLanguage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while providing real-time support.");
                return GetDefaultResponse("tr");
            }
        }
    }
}
