using CQRSRentACar.Context;
using CQRSRentACar.Entities;
using CQRSRentACar.CQRSPattern.Commands.ContactMessageCommands;

namespace CQRSRentACar.CQRSPattern.Handlers.ContactMessageHandlers
{
    public class CreateContactMessageCommandHandler
    {
        private readonly CQRSContext _context;

        public CreateContactMessageCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateContactMessageCommand command)
        {
            var contactMessage = new ContactMessage
            {
                Name = command.Name,
                Email = command.Email,
                Subject = command.Subject,
                Message = command.Message,
                SentDate = DateTime.Now,
                IsRead = false
            };

            _context.ContactMessages.Add(contactMessage);
            await _context.SaveChangesAsync();
            
            return contactMessage.ContactMessageId;
        }

        public async Task UpdateAiResponse(int messageId, string aiResponse)
        {
            var message = await _context.ContactMessages.FindAsync(messageId);
            if (message != null)
            {
                message.AiResponse = aiResponse;
                message.ResponseDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
    }
}
