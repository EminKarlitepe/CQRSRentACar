using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.AboutCommand;
using System.Threading.Tasks;

namespace CQRSRentACar.CQRSPattern.Handlers.AboutHandlers
{
    public class RemoveAboutCommandHandler
    {
        private readonly CQRSContext _context;

        public RemoveAboutCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveAboutCommand command)
        {
            var value = await _context.Abouts.FindAsync(command.Id);
            if (value == null)
                return;

            _context.Abouts.Remove(value);
            await _context.SaveChangesAsync();
        }
    }
}
