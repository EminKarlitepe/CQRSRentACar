using Microsoft.AspNetCore.Mvc;
using CQRSRentACar.CQRSPattern.Handlers.ContactMessageHandlers;
using CQRSRentACar.CQRSPattern.Queries.ContactMessageQueries;
using CQRSRentACar.CQRSPattern.Commands.ContactMessageCommands;
using CQRSRentACar.Context;

namespace CQRSRentACar.Controllers
{
    public class AdminContactController : Controller
    {
        private readonly GetContactMessageQueryHandler _getContactMessageQueryHandler;
        private readonly GetContactMessageByIdQueryHandler _getContactMessageByIdQueryHandler;
        private readonly RemoveContactMessageCommandHandler _removeContactMessageCommandHandler;
        private readonly CQRSContext _context;

        public AdminContactController(GetContactMessageQueryHandler getContactMessageQueryHandler, GetContactMessageByIdQueryHandler getContactMessageByIdQueryHandler, RemoveContactMessageCommandHandler removeContactMessageCommandHandler, CQRSContext context)
        {
            _getContactMessageQueryHandler = getContactMessageQueryHandler;
            _getContactMessageByIdQueryHandler = getContactMessageByIdQueryHandler;
            _removeContactMessageCommandHandler = removeContactMessageCommandHandler;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var messages = await _getContactMessageQueryHandler.Handle();
            return View(messages);
        }

        public async Task<IActionResult> Details(int id)
        {
            var message = await _getContactMessageByIdQueryHandler.Handle(new GetContactMessageByIdQuery(id));
            
            if (!message.IsRead)
            {
                message.IsRead = true;
                await _context.SaveChangesAsync();
            }
            
            return View(message);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _removeContactMessageCommandHandler.Handle(new RemoveContactMessageCommand(id));
            TempData["Success"] = "Mesaj başarıyla silindi.";
            
            return RedirectToAction("Index");
        }
    }
}
