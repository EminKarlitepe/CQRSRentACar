using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.ServiceCommands;
using CQRSRentACar.Entities;

namespace CQRSRentACar.CQRSPattern.Handlers.ServiceHandlers
{
    public class CreateServiceCommandHandler
    {
        private readonly CQRSContext _context;

        public CreateServiceCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateServiceCommand command)
        {
            _context.Services.Add(new Service
            {
                ServiceTitle = command.ServiceTitle,
                ServiceDescription = command.ServiceDescription,
                ServiceIcon = command.ServiceIcon
            });

            await _context.SaveChangesAsync();
        }
    }
}
