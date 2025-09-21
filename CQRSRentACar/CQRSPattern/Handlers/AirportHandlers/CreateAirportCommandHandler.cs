using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.AirportCommands;
using CQRSRentACar.Entities;

namespace CQRSRentACar.CQRSPattern.Handlers.AirportHandlers
{
    public class CreateAirportCommandHandler
    {
        private readonly CQRSContext _context;

        public CreateAirportCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateAirportCommand command)
        {
            _context.Airports.Add(new Airport
            {
                Iata = command.Iata,
                Icao = command.Icao,
                Name = command.Name,
                Location = command.Location,
                City = command.City,
                State = command.State,
                CountryIso = command.CountryIso,
                Country = command.Country,
                Latitude = command.Latitude,
                Longitude = command.Longitude,
                Phone = command.Phone,
                Website = command.Website,
                CreatedDate = DateTime.Now,
                IsActive = true
            });

            await _context.SaveChangesAsync();
        }
    }
}
