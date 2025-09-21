using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.CarCommands;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.CarHandlers
{
    public class RemoveCarCommandHandler
    {
        private readonly CQRSContext _context;

        public RemoveCarCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveCarCommand command)
        {
            var value = await _context.Cars.FindAsync(command.Id);
            _context.Cars.Remove(value);
            await _context.SaveChangesAsync();
        }
    }
}
