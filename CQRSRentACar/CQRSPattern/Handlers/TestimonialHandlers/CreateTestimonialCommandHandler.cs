using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.TestimonialCommands;
using CQRSRentACar.Entities;

namespace CQRSRentACar.CQRSPattern.Handlers.TestimonialHandlers
{
    public class CreateTestimonialCommandHandler
    {
        private readonly CQRSContext _context;

        public CreateTestimonialCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateTestimonialCommand command)
        {
            _context.Testimonials.Add(new Testimonial
            {
                TestimonialNameSurname = command.TestimonialNameSurname,
                TestimonialPosition = command.TestimonialPosition,
                TestimonialRating = command.TestimonialRating,
                TestimonialComment = command.TestimonialComment,
                TestimonialImageUrl = command.TestimonialImageUrl
            });

            await _context.SaveChangesAsync();
        }
    }
}
