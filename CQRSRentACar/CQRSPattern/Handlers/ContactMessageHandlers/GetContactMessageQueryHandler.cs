using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Queries.ContactMessageQueries;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.ContactMessageHandlers
{
    public class GetContactMessageQueryHandler
    {
        private readonly CQRSContext _context;

        public GetContactMessageQueryHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task<List<Entities.ContactMessage>> Handle()
        {
            return await _context.ContactMessages
                .OrderByDescending(x => x.SentDate)
                .ToListAsync();
        }
    }
}
