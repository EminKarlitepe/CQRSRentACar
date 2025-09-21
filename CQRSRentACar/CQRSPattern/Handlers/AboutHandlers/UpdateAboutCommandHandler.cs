using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.AboutCommand;
using System.Threading.Tasks;

namespace CQRSRentACar.CQRSPattern.Handlers.AboutHandlers
{
    public class UpdateAboutCommandHandler
    {
        private readonly CQRSContext _context;

        public UpdateAboutCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateAboutCommand command)
        {
            var value = await _context.Abouts.FindAsync(command.AboutId);
            if (value == null)
                return;

            value.Description1 = command.Description1;
            value.Description2 = command.Description2;
            value.VisionDescription = command.VisionDescription;
            value.MisionDescription = command.MisionDescription;

            await _context.SaveChangesAsync();
        }
    }
}
