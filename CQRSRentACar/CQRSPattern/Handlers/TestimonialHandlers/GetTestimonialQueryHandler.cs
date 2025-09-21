using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Results.TestimonialResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.TestimonialHandlers
{
    public class GetTestimonialQueryHandler
    {
        private readonly CQRSContext _context;

        public GetTestimonialQueryHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task<List<GetTestimonialQueryResult>> Handle()
        {
            var values = await _context.Testimonials.AsNoTracking().ToListAsync();

            return values.Select(x => new GetTestimonialQueryResult
            {
                TestimonialId = x.TestimonialId,
                TestimonialNameSurname = x.TestimonialNameSurname,
                TestimonialPosition = x.TestimonialPosition,
                TestimonialRating = x.TestimonialRating,
                TestimonialComment = x.TestimonialComment,
                TestimonialImageUrl = x.TestimonialImageUrl
            }).ToList();
        }
    }
}
