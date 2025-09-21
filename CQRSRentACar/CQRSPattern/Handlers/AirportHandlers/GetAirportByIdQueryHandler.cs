using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Queries.AirportQueries;
using CQRSRentACar.CQRSPattern.Results.AirportResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.AirportHandlers
{
    public class GetAirportByIdQueryHandler
    {
        private readonly CQRSContext _context;

        public GetAirportByIdQueryHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task<GetAirportByIdQueryResult> Handle(GetAirportByIdQuery query)
        {
            var value = await _context.Airports.AsNoTracking().FirstOrDefaultAsync(x => x.AirportId == query.Id);

            return new GetAirportByIdQueryResult
            {
                AirportId = value?.AirportId ?? 0,
                Iata = value?.Iata ?? "",
                Icao = value?.Icao ?? "",
                Name = value?.Name ?? "",
                Location = value?.Location ?? "",
                City = value?.City ?? "",
                State = value?.State ?? "",
                CountryIso = value?.CountryIso ?? "",
                Country = value?.Country ?? "",
                Latitude = value?.Latitude ?? 0,
                Longitude = value?.Longitude ?? 0,
                Phone = value?.Phone ?? "",
                Website = value?.Website ?? "",
                CreatedDate = value?.CreatedDate ?? DateTime.Now,
                IsActive = value?.IsActive ?? false
            };
        }
    }
}
