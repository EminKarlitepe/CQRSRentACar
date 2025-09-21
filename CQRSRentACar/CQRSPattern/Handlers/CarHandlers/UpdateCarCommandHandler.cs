using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.CarCommands;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.CarHandlers
{
    public class UpdateCarCommandHandler
    {
        private readonly CQRSContext _context;

        public UpdateCarCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCarCommand command)
        {
            var value = await _context.Cars.FindAsync(command.CarId);

            value.CarName = command.CarName;
            value.CarImageUrl = command.CarImageUrl;
            value.Rating = command.Rating;
            value.Price = command.Price;
            value.Seat = command.Seat;
            value.Transmission = command.Transmission;
            value.CarType = command.CarType;
            value.FuelType = command.FuelType;
            value.ModelYear = command.ModelYear;
            value.Gear = command.Gear;
            value.Kilometer = command.Kilometer;

            await _context.SaveChangesAsync();
        }
    }
}
