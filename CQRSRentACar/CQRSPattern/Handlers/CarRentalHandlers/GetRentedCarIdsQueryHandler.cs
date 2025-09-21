using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Queries.CarRentalQueries;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.CarRentalHandlers
{
    public class GetRentedCarIdsQueryHandler
    {
        private readonly CQRSContext _context;

        public GetRentedCarIdsQueryHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task<List<int>> Handle(GetRentedCarIdsQuery query)
        {
            var rentedCarIds = await _context.CarRentals
                .Where(cr => 
                    (query.PickUpDate <= cr.DropOffDate && query.DropOffDate >= cr.PickUpDate) &&
                    
                    (string.IsNullOrEmpty(query.PickUpLocation) || 
                     (cr.PickUpLocation != null && cr.PickUpLocation.ToLower().Contains(query.PickUpLocation.ToLower())) ||
                     (cr.DropOffLocation != null && cr.DropOffLocation.ToLower().Contains(query.PickUpLocation.ToLower()))) &&
                     
                    (string.IsNullOrEmpty(query.DropOffLocation) || 
                     (cr.PickUpLocation != null && cr.PickUpLocation.ToLower().Contains(query.DropOffLocation.ToLower())) ||
                     (cr.DropOffLocation != null && cr.DropOffLocation.ToLower().Contains(query.DropOffLocation.ToLower()))))
                .Select(cr => cr.CarId)
                .Distinct()
                .ToListAsync();

            return rentedCarIds;
        }
    }
}
