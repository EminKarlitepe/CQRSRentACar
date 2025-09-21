using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.SliderCommands;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.SliderHandlers
{
    public class UpdateSliderCommandHandler
    {
        private readonly CQRSContext _context;

        public UpdateSliderCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateSliderCommand command)
        {
            var value = await _context.Sliders.FindAsync(command.SliderId);

            value.SliderTitle = command.SliderTitle;
            value.SliderSubTitle = command.SliderSubTitle;
            value.SliderImageUrl = command.SliderImageUrl;

            await _context.SaveChangesAsync();
        }
    }
}
