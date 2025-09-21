using Microsoft.AspNetCore.Mvc;
using CQRSRentACar.CQRSPattern.Commands.EmployeeCommands;
using CQRSRentACar.CQRSPattern.Handlers.EmployeeHandlers;
using CQRSRentACar.CQRSPattern.Queries.EmployeeQueries;

namespace CQRSRentACar.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly GetEmployeeQueryHandler _getEmployeeQueryHandler;
        private readonly GetEmployeeByIdQueryHandler _getEmployeeByIdQueryHandler;
        private readonly CreateEmployeeCommandHandler _createEmployeeCommandHandler;
        private readonly UpdateEmployeeCommandHandler _updateEmployeeCommandHandler;
        private readonly RemoveEmployeeCommandHandler _removeEmployeeCommandHandler;

        public EmployeeController(GetEmployeeQueryHandler getEmployeeQueryHandler, GetEmployeeByIdQueryHandler getEmployeeByIdQueryHandler, CreateEmployeeCommandHandler createEmployeeCommandHandler, UpdateEmployeeCommandHandler updateEmployeeCommandHandler, RemoveEmployeeCommandHandler removeEmployeeCommandHandler)
        {
            _getEmployeeQueryHandler = getEmployeeQueryHandler;
            _getEmployeeByIdQueryHandler = getEmployeeByIdQueryHandler;
            _createEmployeeCommandHandler = createEmployeeCommandHandler;
            _updateEmployeeCommandHandler = updateEmployeeCommandHandler;
            _removeEmployeeCommandHandler = removeEmployeeCommandHandler;
        }

        public async Task<IActionResult> EmployeeList()
        {
            var values = await _getEmployeeQueryHandler.Handle();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeCommand command)
        {
            await _createEmployeeCommandHandler.Handle(command);
            return RedirectToAction("EmployeeList");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _removeEmployeeCommandHandler.Handle(new RemoveEmployeeCommand(id));
            return RedirectToAction("EmployeeList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateEmployee(int id)
        {
            var dto = await _getEmployeeByIdQueryHandler.Handle(new GetEmployeeByIdQuery(id));

            var command = new UpdateEmployeeCommand
            {
                EmployeeId = dto.EmployeeId,
                EmployeeNameSurname = dto.EmployeeNameSurname,
                EmployeePosition = dto.EmployeePosition,
                EmployeeImageUrl = dto.EmployeeImageUrl
            };

            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeCommand command)
        {
            await _updateEmployeeCommandHandler.Handle(command);
            return RedirectToAction("EmployeeList");
        }
    }
}
