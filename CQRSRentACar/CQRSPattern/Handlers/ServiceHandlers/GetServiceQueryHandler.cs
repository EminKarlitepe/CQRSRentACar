using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Results.ServiceResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.ServiceHandlers
{
    public class GetServiceQueryHandler
    {
        private readonly CQRSContext _context;

        public GetServiceQueryHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task<List<GetServiceQueryResult>> Handle()
        {
            var values = await _context.Services.AsNoTracking().ToListAsync();

            return values.Select(x => new GetServiceQueryResult
            {
                ServiceId = x.ServiceId,
                ServiceTitle = x.ServiceTitle,
                ServiceDescription = x.ServiceDescription,
                ServiceIcon = x.ServiceIcon
            }).ToList();
        }
    }
}
