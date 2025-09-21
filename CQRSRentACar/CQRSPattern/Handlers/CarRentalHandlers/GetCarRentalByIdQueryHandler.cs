using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Queries.CarRentalQueries;
using CQRSRentACar.CQRSPattern.Results.CarRentalResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.CarRentalHandlers
{
    public class GetCarRentalByIdQueryHandler
    {
        private readonly CQRSContext _context;

        public GetCarRentalByIdQueryHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task<GetCarRentalQueryResult> Handle(GetCarRentalByIdQuery query)
        {
            var value = await _context.CarRentals.AsNoTracking().Include(x => x.Car).FirstOrDefaultAsync(x => x.CarRentalId == query.Id);

            if (value == null)
                return null;

            return new GetCarRentalQueryResult
            {
                CarRentalId = value.CarRentalId,
                PickUpLocation = value.PickUpLocation ?? string.Empty,
                DropOffLocation = value.DropOffLocation ?? string.Empty,
                PickUpDate = value.PickUpDate,
                DropOffDate = value.DropOffDate,
                CarId = value.CarId,
                CarName = value.Car?.CarName ?? string.Empty
            };
        }
    }
}
