using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Queries.TestimonialQueries;
using CQRSRentACar.CQRSPattern.Results.TestimonialResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.TestimonialHandlers
{
    public class GetTestimonialByIdQueryHandler
    {
        private readonly CQRSContext _context;

        public GetTestimonialByIdQueryHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task<GetTestimonialByIdQueryResult> Handle(GetTestimonialByIdQuery query)
        {
            var value = await _context.Testimonials.AsNoTracking().FirstOrDefaultAsync(x => x.TestimonialId == query.Id);

            return new GetTestimonialByIdQueryResult
            {
                TestimonialId = value.TestimonialId,
                TestimonialNameSurname = value.TestimonialNameSurname,
                TestimonialPosition = value.TestimonialPosition,
                TestimonialRating = value.TestimonialRating,
                TestimonialComment = value.TestimonialComment,
                TestimonialImageUrl = value.TestimonialImageUrl
            };
        }
    }
}
