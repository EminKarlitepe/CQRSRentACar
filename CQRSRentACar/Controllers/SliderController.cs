using Microsoft.AspNetCore.Mvc;
using CQRSRentACar.CQRSPattern.Commands.SliderCommands;
using CQRSRentACar.CQRSPattern.Handlers.SliderHandlers;
using CQRSRentACar.CQRSPattern.Queries.SliderQueries;

namespace CQRSRentACar.Controllers
{
    public class SliderController : Controller
    {
        private readonly GetSliderQueryHandler _getSliderQueryHandler;
        private readonly GetSliderByIdQueryHandler _getSliderByIdQueryHandler;
        private readonly CreateSliderCommandHandler _createSliderCommandHandler;
        private readonly UpdateSliderCommandHandler _updateSliderCommandHandler;
        private readonly RemoveSliderCommandHandler _removeSliderCommandHandler;

        public SliderController(GetSliderQueryHandler getSliderQueryHandler, GetSliderByIdQueryHandler getSliderByIdQueryHandler, CreateSliderCommandHandler createSliderCommandHandler, UpdateSliderCommandHandler updateSliderCommandHandler, RemoveSliderCommandHandler removeSliderCommandHandler)
        {
            _getSliderQueryHandler = getSliderQueryHandler;
            _getSliderByIdQueryHandler = getSliderByIdQueryHandler;
            _createSliderCommandHandler = createSliderCommandHandler;
            _updateSliderCommandHandler = updateSliderCommandHandler;
            _removeSliderCommandHandler = removeSliderCommandHandler;
        }

        public async Task<IActionResult> SliderList()
        {
            var values = await _getSliderQueryHandler.Handle();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateSlider()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSlider(CreateSliderCommand command)
        {
            await _createSliderCommandHandler.Handle(command);
            return RedirectToAction("SliderList");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteSlider(int id)
        {
            await _removeSliderCommandHandler.Handle(new RemoveSliderCommand(id));
            return RedirectToAction("SliderList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSlider(int id)
        {
            var dto = await _getSliderByIdQueryHandler.Handle(new GetSliderByIdQuery(id));

            var command = new UpdateSliderCommand
            {
                SliderId = dto.SliderId,
                SliderTitle = dto.SliderTitle,
                SliderSubTitle = dto.SliderSubTitle,
                SliderImageUrl = dto.SliderImageUrl
            };

            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSlider(UpdateSliderCommand command)
        {
            await _updateSliderCommandHandler.Handle(command);
            return RedirectToAction("SliderList");
        }
    }
}
