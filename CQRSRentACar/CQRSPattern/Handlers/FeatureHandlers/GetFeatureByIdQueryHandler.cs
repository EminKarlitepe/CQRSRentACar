using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Queries.FeatureQueries;
using CQRSRentACar.CQRSPattern.Results.FeatureResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.FeatureHandlers
{
    public class GetFeatureByIdQueryHandler
    {
        private readonly CQRSContext _context;

        public GetFeatureByIdQueryHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task<GetFeatureQueryResult> Handle(GetFeatureByIdQuery query)
        {
            var value = await _context.Features.AsNoTracking().FirstOrDefaultAsync(x => x.FeatureId == query.Id);

            return new GetFeatureQueryResult
            {
                FeatureId = value.FeatureId,
                FeatureTitle = value.FeatureTitle,
                FeatureDescription = value.FeatureDescription,
                FeatureIcon = value.FeatureIcon
            };
        }
    }
}
