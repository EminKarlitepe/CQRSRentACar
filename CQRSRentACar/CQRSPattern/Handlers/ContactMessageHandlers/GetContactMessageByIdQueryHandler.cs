using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Queries.ContactMessageQueries;

namespace CQRSRentACar.CQRSPattern.Handlers.ContactMessageHandlers
{
    public class GetContactMessageByIdQueryHandler
    {
        private readonly CQRSContext _context;

        public GetContactMessageByIdQueryHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task<Entities.ContactMessage?> Handle(GetContactMessageByIdQuery query)
        {
            return await _context.ContactMessages.FindAsync(query.Id);
        }
    }
}
