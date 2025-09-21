using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Results.FeatureResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.FeatureHandlers
{
    public class GetFeatureQueryHandler
    {
        private readonly CQRSContext _context;

        public GetFeatureQueryHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task<List<GetFeatureQueryResult>> Handle()
        {
            var values = await _context.Features.AsNoTracking().ToListAsync();

            return values.Select(x => new GetFeatureQueryResult
            {
                FeatureId = x.FeatureId,
                FeatureTitle = x.FeatureTitle,
                FeatureDescription = x.FeatureDescription,
                FeatureIcon = x.FeatureIcon
            }).ToList();
        }
    }
}
