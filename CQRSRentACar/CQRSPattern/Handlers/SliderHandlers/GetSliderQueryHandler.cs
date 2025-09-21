using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Results.SliderResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.SliderHandlers
{
    public class GetSliderQueryHandler
    {
        private readonly CQRSContext _context;

        public GetSliderQueryHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task<List<GetSliderQueryResult>> Handle()
        {
            var values = await _context.Sliders.AsNoTracking().ToListAsync();

            return values.Select(x => new GetSliderQueryResult
            {
                SliderId = x.SliderId,
                SliderTitle = x.SliderTitle,
                SliderSubTitle = x.SliderSubTitle,
                SliderImageUrl = x.SliderImageUrl
            }).ToList();
        }
    }
}
