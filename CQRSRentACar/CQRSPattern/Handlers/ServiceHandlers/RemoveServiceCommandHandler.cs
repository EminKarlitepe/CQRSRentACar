using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.ServiceCommands;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.ServiceHandlers
{
    public class RemoveServiceCommandHandler
    {
        private readonly CQRSContext _context;

        public RemoveServiceCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveServiceCommand command)
        {
            var value = await _context.Services.FindAsync(command.Id);
            _context.Services.Remove(value);
            await _context.SaveChangesAsync();
        }
    }
}
