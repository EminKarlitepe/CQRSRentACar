using Microsoft.AspNetCore.Mvc;
using CQRSRentACar.CQRSPattern.Commands.TestimonialCommands;
using CQRSRentACar.CQRSPattern.Handlers.TestimonialHandlers;
using CQRSRentACar.CQRSPattern.Queries.TestimonialQueries;

namespace CQRSRentACar.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly GetTestimonialQueryHandler _getTestimonialQueryHandler;
        private readonly GetTestimonialByIdQueryHandler _getTestimonialByIdQueryHandler;
        private readonly CreateTestimonialCommandHandler _createTestimonialCommandHandler;
        private readonly UpdateTestimonialCommandHandler _updateTestimonialCommandHandler;
        private readonly RemoveTestimonialCommandHandler _removeTestimonialCommandHandler;

        public TestimonialController(GetTestimonialQueryHandler getTestimonialQueryHandler, GetTestimonialByIdQueryHandler getTestimonialByIdQueryHandler, CreateTestimonialCommandHandler createTestimonialCommandHandler, UpdateTestimonialCommandHandler updateTestimonialCommandHandler, RemoveTestimonialCommandHandler removeTestimonialCommandHandler)
        {
            _getTestimonialQueryHandler = getTestimonialQueryHandler;
            _getTestimonialByIdQueryHandler = getTestimonialByIdQueryHandler;
            _createTestimonialCommandHandler = createTestimonialCommandHandler;
            _updateTestimonialCommandHandler = updateTestimonialCommandHandler;
            _removeTestimonialCommandHandler = removeTestimonialCommandHandler;
        }

        public async Task<IActionResult> TestimonialList()
        {
            var values = await _getTestimonialQueryHandler.Handle();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateTestimonial()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialCommand command)
        {
            await _createTestimonialCommandHandler.Handle(command);
            return RedirectToAction("TestimonialList");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            await _removeTestimonialCommandHandler.Handle(new RemoveTestimonialCommand(id));
            return RedirectToAction("TestimonialList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTestimonial(int id)
        {
            var dto = await _getTestimonialByIdQueryHandler.Handle(new GetTestimonialByIdQuery(id));

            var command = new UpdateTestimonialCommand
            {
                TestimonialId = dto.TestimonialId,
                TestimonialNameSurname = dto.TestimonialNameSurname,
                TestimonialPosition = dto.TestimonialPosition,
                TestimonialRating = dto.TestimonialRating,
                TestimonialComment = dto.TestimonialComment,
                TestimonialImageUrl = dto.TestimonialImageUrl
            };

            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialCommand command)
        {
            await _updateTestimonialCommandHandler.Handle(command);
            return RedirectToAction("TestimonialList");
        }
    }
}
