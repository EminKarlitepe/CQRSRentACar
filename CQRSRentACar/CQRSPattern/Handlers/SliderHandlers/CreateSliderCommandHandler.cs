using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.SliderCommands;
using CQRSRentACar.Entities;

namespace CQRSRentACar.CQRSPattern.Handlers.SliderHandlers
{
    public class CreateSliderCommandHandler
    {
        private readonly CQRSContext _context;

        public CreateSliderCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateSliderCommand command)
        {
            _context.Sliders.Add(new Slider
            {
                SliderTitle = command.SliderTitle,
                SliderSubTitle = command.SliderSubTitle,
                SliderImageUrl = command.SliderImageUrl
            });

            await _context.SaveChangesAsync();
        }
    }
}
