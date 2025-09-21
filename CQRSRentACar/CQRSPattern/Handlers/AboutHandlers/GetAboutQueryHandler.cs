using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Results.AboutResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.AboutHandlers
{
    public class GetAboutQueryHandler
    {
        private readonly CQRSContext _context;

        public GetAboutQueryHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task<List<GetAboutQueryResult>> Handle()
        {
            var values = await _context.Abouts.AsNoTracking().ToListAsync();

            return values.Select(x => new GetAboutQueryResult
            {
                AboutId = x.AboutId,
                Description1 = x.Description1,
                Description2 = x.Description2,
                VisionDescription = x.VisionDescription,
                MisionDescription = x.MisionDescription
            }).ToList();
        }
    }
}
