using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.FeatureCommands;
using CQRSRentACar.Entities;

namespace CQRSRentACar.CQRSPattern.Handlers.FeatureHandlers
{
    public class CreateFeatureCommandHandler
    {
        private readonly CQRSContext _context;

        public CreateFeatureCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateFeatureCommand command)
        {
            _context.Features.Add(new Feature
            {
                FeatureTitle = command.FeatureTitle,
                FeatureDescription = command.FeatureDescription,
                FeatureIcon = command.FeatureIcon
            });

            await _context.SaveChangesAsync();
        }
    }
}
