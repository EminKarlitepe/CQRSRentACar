using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.AirportCommands;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.AirportHandlers
{
    public class UpdateAirportCommandHandler
    {
        private readonly CQRSContext _context;

        public UpdateAirportCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateAirportCommand command)
        {
            var value = await _context.Airports.FindAsync(command.AirportId);

            if (value != null)
            {
                value.Iata = command.Iata;
                value.Icao = command.Icao;
                value.Name = command.Name;
                value.Location = command.Location;
                value.City = command.City;
                value.State = command.State;
                value.CountryIso = command.CountryIso;
                value.Country = command.Country;
                value.Latitude = command.Latitude;
                value.Longitude = command.Longitude;
                value.Phone = command.Phone;
                value.Website = command.Website;
                value.IsActive = command.IsActive;

                await _context.SaveChangesAsync();
            }
        }
    }
}
