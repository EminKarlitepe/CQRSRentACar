using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Queries.SliderQueries;
using CQRSRentACar.CQRSPattern.Results.SliderResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.SliderHandlers
{
    public class GetSliderByIdQueryHandler
    {
        private readonly CQRSContext _context;

        public GetSliderByIdQueryHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task<GetSliderQueryResult> Handle(GetSliderByIdQuery query)
        {
            var value = await _context.Sliders.AsNoTracking().FirstOrDefaultAsync(x => x.SliderId == query.Id);

            return new GetSliderQueryResult
            {
                SliderId = value.SliderId,
                SliderTitle = value.SliderTitle,
                SliderSubTitle = value.SliderSubTitle,
                SliderImageUrl = value.SliderImageUrl
            };
        }
    }
}
