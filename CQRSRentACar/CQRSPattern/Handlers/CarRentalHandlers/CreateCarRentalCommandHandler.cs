using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.CarRentalCommands;
using CQRSRentACar.Entities;

namespace CQRSRentACar.CQRSPattern.Handlers.CarRentalHandlers
{
    public class CreateCarRentalCommandHandler
    {
        private readonly CQRSContext _context;

        public CreateCarRentalCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateCarRentalCommand command)
        {
            _context.CarRentals.Add(new CarRental
            {
                PickUpLocation = command.PickUpLocation,
                DropOffLocation = command.DropOffLocation,
                PickUpDate = command.PickUpDate,
                DropOffDate = command.DropOffDate,
                CarId = command.CarId,
                PickUpAirportId = command.PickUpAirportId,
                DropOffAirportId = command.DropOffAirportId
            });

            await _context.SaveChangesAsync();
        }
    }
}
