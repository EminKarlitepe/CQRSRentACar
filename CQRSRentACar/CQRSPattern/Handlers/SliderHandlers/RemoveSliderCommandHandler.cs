using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.SliderCommands;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.SliderHandlers
{
    public class RemoveSliderCommandHandler
    {
        private readonly CQRSContext _context;

        public RemoveSliderCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveSliderCommand command)
        {
            var value = await _context.Sliders.FindAsync(command.Id);
            _context.Sliders.Remove(value);
            await _context.SaveChangesAsync();
        }
    }
}
