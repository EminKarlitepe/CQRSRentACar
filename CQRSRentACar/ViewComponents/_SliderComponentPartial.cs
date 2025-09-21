using CQRSRentACar.Context;
using CQRSRentACar.Entities;
using CQRSRentACar.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.ViewComponents
{
    public class _SliderComponentPartial : ViewComponent
    {
        private readonly CQRSContext _context;
        private readonly IAirportService _airportService;

        public _SliderComponentPartial(CQRSContext context, IAirportService airportService)
        {
            _context = context;
            _airportService = airportService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sliders = await _context.Sliders.ToListAsync();
            var airports = await _airportService.GetTurkishAirportsAsync();
            
            ViewBag.Airports = airports;
            return View(sliders);
        }
    }
}
