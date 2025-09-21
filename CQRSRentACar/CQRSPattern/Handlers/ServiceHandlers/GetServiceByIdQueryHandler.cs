using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Queries.ServiceQueries;
using CQRSRentACar.CQRSPattern.Results.ServiceResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.ServiceHandlers
{
    public class GetServiceByIdQueryHandler
    {
        private readonly CQRSContext _context;

        public GetServiceByIdQueryHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task<GetServiceQueryResult> Handle(GetServiceByIdQuery query)
        {
            var value = await _context.Services.AsNoTracking().FirstOrDefaultAsync(x => x.ServiceId == query.Id);

            return new GetServiceQueryResult
            {
                ServiceId = value.ServiceId,
                ServiceTitle = value.ServiceTitle,
                ServiceDescription = value.ServiceDescription,
                ServiceIcon = value.ServiceIcon
            };
        }
    }
}
