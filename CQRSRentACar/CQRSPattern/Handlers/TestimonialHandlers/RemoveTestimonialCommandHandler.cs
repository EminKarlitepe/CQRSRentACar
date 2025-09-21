using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.TestimonialCommands;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.TestimonialHandlers
{
    public class RemoveTestimonialCommandHandler
    {
        private readonly CQRSContext _context;

        public RemoveTestimonialCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveTestimonialCommand command)
        {
            var value = await _context.Testimonials.FindAsync(command.Id);
            _context.Testimonials.Remove(value);
            await _context.SaveChangesAsync();
        }
    }
}
