using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.AirportCommands;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.AirportHandlers
{
    public class RemoveAirportCommandHandler
    {
        private readonly CQRSContext _context;

        public RemoveAirportCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveAirportCommand command)
        {
            var value = await _context.Airports.FindAsync(command.AirportId);

            if (value != null)
            {
                _context.Airports.Remove(value);
                await _context.SaveChangesAsync();
            }
        }
    }
}
