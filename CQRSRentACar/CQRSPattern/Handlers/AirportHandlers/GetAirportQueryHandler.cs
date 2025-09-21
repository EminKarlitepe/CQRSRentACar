using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Queries.AirportQueries;
using CQRSRentACar.CQRSPattern.Results.AirportResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.AirportHandlers
{
    public class GetAirportQueryHandler
    {
        private readonly CQRSContext _context;

        public GetAirportQueryHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task<List<GetAirportQueryResult>> Handle(GetAirportQuery query)
        {
            var queryable = _context.Airports.AsNoTracking();

            if (!string.IsNullOrEmpty(query.CountryCode))
            {
                queryable = queryable.Where(x => x.CountryIso == query.CountryCode);
            }

            if (!query.IncludeInactive)
            {
                queryable = queryable.Where(x => x.IsActive);
            }

            var airports = await queryable
                .OrderBy(x => x.Name)
                .Select(x => new GetAirportQueryResult
                {
                    AirportId = x.AirportId,
                    Iata = x.Iata,
                    Icao = x.Icao,
                    Name = x.Name,
                    Location = x.Location,
                    City = x.City,
                    State = x.State,
                    CountryIso = x.CountryIso,
                    Country = x.Country,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    Phone = x.Phone,
                    Website = x.Website,
                    CreatedDate = x.CreatedDate,
                    IsActive = x.IsActive
                })
                .ToListAsync();

            return airports;
        }
    }
}
