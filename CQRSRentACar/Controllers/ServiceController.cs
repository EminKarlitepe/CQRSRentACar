using Microsoft.AspNetCore.Mvc;
using CQRSRentACar.CQRSPattern.Commands.ServiceCommands;
using CQRSRentACar.CQRSPattern.Handlers.ServiceHandlers;
using CQRSRentACar.CQRSPattern.Queries.ServiceQueries;

namespace CQRSRentACar.Controllers
{
    public class ServiceController : Controller
    {
        private readonly GetServiceQueryHandler _getServiceQueryHandler;
        private readonly GetServiceByIdQueryHandler _getServiceByIdQueryHandler;
        private readonly CreateServiceCommandHandler _createServiceCommandHandler;
        private readonly UpdateServiceCommandHandler _updateServiceCommandHandler;
        private readonly RemoveServiceCommandHandler _removeServiceCommandHandler;

        public ServiceController(GetServiceQueryHandler getServiceQueryHandler, GetServiceByIdQueryHandler getServiceByIdQueryHandler, CreateServiceCommandHandler createServiceCommandHandler, UpdateServiceCommandHandler updateServiceCommandHandler, RemoveServiceCommandHandler removeServiceCommandHandler)
        {
            _getServiceQueryHandler = getServiceQueryHandler;
            _getServiceByIdQueryHandler = getServiceByIdQueryHandler;
            _createServiceCommandHandler = createServiceCommandHandler;
            _updateServiceCommandHandler = updateServiceCommandHandler;
            _removeServiceCommandHandler = removeServiceCommandHandler;
        }

        public async Task<IActionResult> ServiceList()
        {
            var values = await _getServiceQueryHandler.Handle();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateService()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateService(CreateServiceCommand command)
        {
            await _createServiceCommandHandler.Handle(command);
            return RedirectToAction("ServiceList");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteService(int id)
        {
            await _removeServiceCommandHandler.Handle(new RemoveServiceCommand(id));
            return RedirectToAction("ServiceList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateService(int id)
        {
            var dto = await _getServiceByIdQueryHandler.Handle(new GetServiceByIdQuery(id));

            var command = new UpdateServiceCommand
            {
                ServiceId = dto.ServiceId,
                ServiceTitle = dto.ServiceTitle,
                ServiceDescription = dto.ServiceDescription,
                ServiceIcon = dto.ServiceIcon
            };

            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateService(UpdateServiceCommand command)
        {
            await _updateServiceCommandHandler.Handle(command);
            return RedirectToAction("ServiceList");
        }
    }
}
