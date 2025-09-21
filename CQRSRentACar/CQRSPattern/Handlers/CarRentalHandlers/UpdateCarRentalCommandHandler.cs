using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.CarRentalCommands;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.CarRentalHandlers
{
    public class UpdateCarRentalCommandHandler
    {
        private readonly CQRSContext _context;

        public UpdateCarRentalCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCarRentalCommand command)
        {
            var value = await _context.CarRentals.FindAsync(command.CarRentalId);

            value.PickUpLocation = command.PickUpLocation;
            value.DropOffLocation = command.DropOffLocation;
            value.PickUpDate = command.PickUpDate;
            value.DropOffDate = command.DropOffDate;
            value.CarId = command.CarId;

            await _context.SaveChangesAsync();
        }
    }
}
