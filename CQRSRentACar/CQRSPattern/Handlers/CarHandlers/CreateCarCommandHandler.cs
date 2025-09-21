using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.CarCommands;
using CQRSRentACar.Entities;

namespace CQRSRentACar.CQRSPattern.Handlers.CarHandlers
{
    public class CreateCarCommandHandler
    {
        private readonly CQRSContext _context;

        public CreateCarCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateCarCommand command)
        {
            _context.Cars.Add(new Car
            {
                CarName = command.CarName,
                CarImageUrl = command.CarImageUrl,
                Rating = command.Rating,
                Price = command.Price,
                Seat = command.Seat,
                Transmission = command.Transmission,
                CarType = command.CarType,
                FuelType = command.FuelType,
                ModelYear = command.ModelYear,
                Gear = command.Gear,
                Kilometer = command.Kilometer
            });

            await _context.SaveChangesAsync();
        }
    }
}
