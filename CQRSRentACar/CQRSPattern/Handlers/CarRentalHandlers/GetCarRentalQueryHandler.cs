using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Queries.CarRentalQueries;
using CQRSRentACar.Entities;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.CarRentalHandlers
{
    public class GetCarRentalQueryHandler
    {
        private readonly CQRSContext _context;

        public GetCarRentalQueryHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task<List<CarRental>> Handle()
        {
            return await _context.CarRentals
                .Include(cr => cr.Car)
                .Include(cr => cr.PickUpAirport)
                .Include(cr => cr.DropOffAirport)
                .OrderByDescending(cr => cr.PickUpDate)
                .ToListAsync();
        }
    }
}