using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Results.CarResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.CarHandlers
{
    public class GetCarQueryHandler
    {
        private readonly CQRSContext _context;

        public GetCarQueryHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task<List<GetCarQueryResult>> Handle()
        {
            var values = await _context.Cars.AsNoTracking().ToListAsync();

            return values.Select(x => new GetCarQueryResult
            {
                CarId = x.CarId,
                CarName = x.CarName,
                CarImageUrl = x.CarImageUrl,
                Rating = x.Rating,
                Price = x.Price,
                Seat = x.Seat,
                Transmission = x.Transmission,
                CarType = x.CarType,
                FuelType = x.FuelType,
                ModelYear = x.ModelYear,
                Gear = x.Gear,
                Kilometer = x.Kilometer
            }).ToList();
        }
    }
}
