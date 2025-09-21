using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.FeatureCommands;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.FeatureHandlers
{
    public class RemoveFeatureCommandHandler
    {
        private readonly CQRSContext _context;

        public RemoveFeatureCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveFeatureCommand command)
        {
            var value = await _context.Features.FindAsync(command.Id);
            _context.Features.Remove(value);
            await _context.SaveChangesAsync();
        }
    }
}
