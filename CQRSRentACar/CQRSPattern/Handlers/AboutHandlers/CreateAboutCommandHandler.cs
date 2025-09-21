using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.AboutCommand;
using CQRSRentACar.Entities;

namespace CQRSRentACar.CQRSPattern.Handlers.AboutHandlers
{
    public class CreateAboutCommandHandler
    {
        private readonly CQRSContext _context;

        public CreateAboutCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateAboutCommand command)
        {
            _context.Abouts.Add(new About
            {
                Description1 = command.Description1,
                Description2 = command.Description2,
                VisionDescription = command.VisionDescription,
                MisionDescription = command.MisionDescription
            });

            await _context.SaveChangesAsync();
        }
    }
}
