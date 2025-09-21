using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.TestimonialCommands;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.TestimonialHandlers
{
    public class UpdateTestimonialCommandHandler
    {
        private readonly CQRSContext _context;

        public UpdateTestimonialCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateTestimonialCommand command)
        {
            var value = await _context.Testimonials.FindAsync(command.TestimonialId);

            value.TestimonialNameSurname = command.TestimonialNameSurname;
            value.TestimonialPosition = command.TestimonialPosition;
            value.TestimonialRating = command.TestimonialRating;
            value.TestimonialComment = command.TestimonialComment;
            value.TestimonialImageUrl = command.TestimonialImageUrl;

            await _context.SaveChangesAsync();
        }
    }
}
