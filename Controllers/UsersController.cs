using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromptProfiller.Data;
using PromptProfiller.Models;

namespace PromptProfiller.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User model)
        {
            try
            {
                var newUser = new User()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Branch = model.Branch,
                    IPAddress = model.IPAddress,
                    IsActive = true,
                };
                _context.Users.Add(newUser);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.ToString();
                return View(model);
            }
            TempData["Message"] = "User created successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
