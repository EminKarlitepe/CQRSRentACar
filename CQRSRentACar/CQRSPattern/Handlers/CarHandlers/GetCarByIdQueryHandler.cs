using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Queries.CarQueries;
using CQRSRentACar.CQRSPattern.Results.CarResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.CarHandlers
{
    public class GetCarByIdQueryHandler
    {
        private readonly CQRSContext _context;

        public GetCarByIdQueryHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task<GetCarByIdQueryResult> Handle(GetCarByIdQuery query)
        {
            var value = await _context.Cars.AsNoTracking().FirstOrDefaultAsync(x => x.CarId == query.Id);

            return new GetCarByIdQueryResult
            {
                CarId = value.CarId,
                CarName = value.CarName,
                CarImageUrl = value.CarImageUrl,
                Rating = value.Rating,
                Price = value.Price,
                Seat = value.Seat,
                Transmission = value.Transmission,
                CarType = value.CarType,
                FuelType = value.FuelType,
                ModelYear = value.ModelYear,
                Gear = value.Gear,
                Kilometer = value.Kilometer
            };
        }
    }
}
