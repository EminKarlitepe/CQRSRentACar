using Microsoft.AspNetCore.Mvc;
using CQRSRentACar.CQRSPattern.Commands.ContactMessageCommands;
using CQRSRentACar.CQRSPattern.Handlers.ContactMessageHandlers;
using CQRSRentACar.Services;

namespace CQRSRentACar.Controllers
{
    public class ContactController : Controller
    {
        private readonly CreateContactMessageCommandHandler _createContactMessageCommandHandler;
        private readonly IChatGptService _chatGptService;
        private readonly IEmailService _emailService;
        private readonly ILogger<ContactController> _logger;

        public ContactController(
            CreateContactMessageCommandHandler createContactMessageCommandHandler, 
            IChatGptService chatGptService,
            IEmailService emailService,
            ILogger<ContactController> logger)
        {
            _createContactMessageCommandHandler = createContactMessageCommandHandler;
            _chatGptService = chatGptService;
            _emailService = emailService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> TestEmail(string email)
        {
            try
            {
                var result = await _emailService.SendEmailAsync(
                    email, 
                    "Test Kullanıcı", 
                    "Test Konu", 
                    "Bu bir test mesajıdır.", 
                    "Bu bir test AI yanıtıdır.");
                
                return Json(new { success = result, message = result ? "Test e-posta gönderildi!" : "Test e-posta gönderilemedi!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Hata: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] CreateContactMessageCommand command)
        {
            try
            {
                var messageId = await _createContactMessageCommandHandler.Handle(command);
                
                var aiResponse = await _chatGptService.GetResponseAsync(command.Message, command.Email);
                
                await _createContactMessageCommandHandler.UpdateAiResponse(messageId, aiResponse);
                
                var emailSent = await _emailService.SendEmailAsync(
                    command.Email, 
                    command.Name, 
                    command.Subject, 
                    command.Message, 
                    aiResponse);
                
                TempData["Success"] = emailSent 
                    ? "Mesajınız başarıyla gönderildi! E-posta adresinize yanıt gönderildi."
                    : "Mesajınız kaydedildi! En kısa sürede size dönüş yapacağız.";
                
                return Json(new { 
                    success = true, 
                    message = emailSent 
                        ? "Mesajınız başarıyla gönderildi! E-posta adresinize yanıt gönderildi."
                        : "Mesajınız kaydedildi! En kısa sürede size dönüş yapacağız.",
                    aiResponse = aiResponse,
                    emailSent = emailSent
                });
            }
            catch (Exception ex)
            {
                return Json(new { 
                    success = false, 
                    message = "Bir hata oluştu. Lütfen tekrar deneyin.",
                    error = ex.Message
                });
            }
        }
    }
}
