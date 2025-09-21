using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.FeatureCommands;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.FeatureHandlers
{
    public class UpdateFeatureCommandHandler
    {
        private readonly CQRSContext _context;

        public UpdateFeatureCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateFeatureCommand command)
        {
            var value = await _context.Features.FindAsync(command.FeatureId);

            value.FeatureTitle = command.FeatureTitle;
            value.FeatureDescription = command.FeatureDescription;
            value.FeatureIcon = command.FeatureIcon;

            await _context.SaveChangesAsync();
        }
    }
}
