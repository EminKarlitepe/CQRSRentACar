using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.ContactMessageCommands;

namespace CQRSRentACar.CQRSPattern.Handlers.ContactMessageHandlers
{
    public class RemoveContactMessageCommandHandler
    {
        private readonly CQRSContext _context;

        public RemoveContactMessageCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveContactMessageCommand command)
        {
            var contactMessage = await _context.ContactMessages.FindAsync(command.Id);
            if (contactMessage != null)
            {
                _context.ContactMessages.Remove(contactMessage);
                await _context.SaveChangesAsync();
            }
        }
    }
}
