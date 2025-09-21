using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.ServiceCommands;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.ServiceHandlers
{
    public class UpdateServiceCommandHandler
    {
        private readonly CQRSContext _context;

        public UpdateServiceCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateServiceCommand command)
        {
            var value = await _context.Services.FindAsync(command.ServiceId);

            value.ServiceTitle = command.ServiceTitle;
            value.ServiceDescription = command.ServiceDescription;
            value.ServiceIcon = command.ServiceIcon;

            await _context.SaveChangesAsync();
        }
    }
}
