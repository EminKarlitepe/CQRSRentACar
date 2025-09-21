using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.CarRentalCommands;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.CarRentalHandlers
{
    public class RemoveCarRentalCommandHandler
    {
        private readonly CQRSContext _context;

        public RemoveCarRentalCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveCarRentalCommand command)
        {
            var value = await _context.CarRentals.FindAsync(command.Id);
            _context.CarRentals.Remove(value);
            await _context.SaveChangesAsync();
        }
    }
}
