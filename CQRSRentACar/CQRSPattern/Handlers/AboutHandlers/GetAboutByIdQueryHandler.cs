using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Queries.AboutQueries;
using CQRSRentACar.CQRSPattern.Results.AboutResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.AboutHandlers
{
    public class GetAboutByIdQueryHandler
    {
        private readonly CQRSContext _context;

        public GetAboutByIdQueryHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task<GetAboutByIdQueryResult> Handle(GetAboutByIdQuery query)
        {
            var value = await _context.Abouts
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.AboutId == query.Id);

            if (value == null)
                return null;

            return new GetAboutByIdQueryResult
            {
                AboutId = value.AboutId,
                Description1 = value.Description1,
                Description2 = value.Description2,
                VisionDescription = value.VisionDescription,
                MisionDescription = value.MisionDescription
            };
        }
    }
}
