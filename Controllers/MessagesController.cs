using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PromptProfiller.Data;
using PromptProfiller.Models;
using PromptProfiller.ViewModels;

namespace PromptProfiller.Controllers
{
    public class MessagesController : Controller
    {
        private readonly AppDbContext _context;
        public MessagesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var messages = _context.Messages.Include(x=>x.User).ToList();
            return View(messages);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["UserList"] = _context.Users.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MessageViewModel model, List<int> selectedUsers, IFormFile imageFile)
        {
            if (model != null)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "messages", imageFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    model.ImagePath = filePath;
                }

                foreach (var userId in selectedUsers)
                {
                    var user = _context.Users.Find(userId);
                    var newMessage = new Message()
                    {
                        ImagePath = model.ImagePath,
                        UserID = userId,
                        Title = model.Title,
                        DisplayTime = model.DisplayTime,
                        DateCreated = DateTime.Now,
                    };
                    _context.Messages.Add(newMessage);
                    await _context.SaveChangesAsync();
                }
                TempData["Message"] = "Message sent successfully";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "Error creating message";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var existingMessage = await _context.Messages.FindAsync(Id);
            var user = await _context.Users.FindAsync(existingMessage.UserID);
            var message = new MessageViewModel()
            {
                Id = existingMessage.Id,
                UserFullName = user.FirstName + " " + user.LastName,
                UserID = user.Id,
                Title = existingMessage.Title,
                DisplayTime = existingMessage.DisplayTime,
                ImagePath = existingMessage.ImagePath,
            };
            return View(message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, Message model)
        {
            var existingMessage = await _context.Messages.FindAsync(Id);
            var user = await _context.Users.FindAsync(existingMessage.UserID);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int Id)
        {
            var existingMessage = await _context.Messages.FindAsync(Id);
            var user = await _context.Users.FindAsync(existingMessage.UserID);
            var message = new MessageViewModel()
            {
                Id = existingMessage.Id,
                UserFullName = user.FirstName + " " + user.LastName,
                UserID = user.Id,
                Title = existingMessage.Title,
                DisplayTime = existingMessage.DisplayTime,
                ImagePath = existingMessage.ImagePath,
            };
            return View(message);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var existingMessage = _context.Messages.Find(Id);
            _context.Messages.Remove(existingMessage);
            _context.SaveChanges();
            TempData["Message"] = "Message removed successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
