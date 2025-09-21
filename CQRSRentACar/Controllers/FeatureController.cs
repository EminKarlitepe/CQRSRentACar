using Microsoft.AspNetCore.Mvc;
using CQRSRentACar.CQRSPattern.Commands.FeatureCommands;
using CQRSRentACar.CQRSPattern.Handlers.FeatureHandlers;
using CQRSRentACar.CQRSPattern.Queries.FeatureQueries;

namespace CQRSRentACar.Controllers
{
    public class FeatureController : Controller
    {
        private readonly GetFeatureQueryHandler _getFeatureQueryHandler;
        private readonly GetFeatureByIdQueryHandler _getFeatureByIdQueryHandler;
        private readonly CreateFeatureCommandHandler _createFeatureCommandHandler;
        private readonly UpdateFeatureCommandHandler _updateFeatureCommandHandler;
        private readonly RemoveFeatureCommandHandler _removeFeatureCommandHandler;

        public FeatureController(GetFeatureQueryHandler getFeatureQueryHandler, GetFeatureByIdQueryHandler getFeatureByIdQueryHandler, CreateFeatureCommandHandler createFeatureCommandHandler, UpdateFeatureCommandHandler updateFeatureCommandHandler, RemoveFeatureCommandHandler removeFeatureCommandHandler)
        {
            _getFeatureQueryHandler = getFeatureQueryHandler;
            _getFeatureByIdQueryHandler = getFeatureByIdQueryHandler;
            _createFeatureCommandHandler = createFeatureCommandHandler;
            _updateFeatureCommandHandler = updateFeatureCommandHandler;
            _removeFeatureCommandHandler = removeFeatureCommandHandler;
        }

        public async Task<IActionResult> FeatureList()
        {
            var values = await _getFeatureQueryHandler.Handle();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateFeature()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureCommand command)
        {
            await _createFeatureCommandHandler.Handle(command);
            return RedirectToAction("FeatureList");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteFeature(int id)
        {
            await _removeFeatureCommandHandler.Handle(new RemoveFeatureCommand(id));
            return RedirectToAction("FeatureList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateFeature(int id)
        {
            var dto = await _getFeatureByIdQueryHandler.Handle(new GetFeatureByIdQuery(id));

            var command = new UpdateFeatureCommand
            {
                FeatureId = dto.FeatureId,
                FeatureTitle = dto.FeatureTitle,
                FeatureDescription = dto.FeatureDescription,
                FeatureIcon = dto.FeatureIcon
            };

            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureCommand command)
        {
            await _updateFeatureCommandHandler.Handle(command);
            return RedirectToAction("FeatureList");
        }
    }
}
