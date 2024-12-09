using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromptProfiller.Data;
using PromptProfiller.Models;

namespace PromptProfiller.Controllers
{
    public class AppUsersController : Controller
    {
        private readonly AppDbContext _context;
        public AppUsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var appUsers = _context.AppUsers.ToList();
            return View(appUsers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(AppUser model)
        {
            try
            {
                var newAppUser = new AppUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Username = model.Username,
                    Password = model.Password,
                    IsActive = true,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                };

                _context.AppUsers.Add(newAppUser);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View(model);
            }
            TempData["Message"] = "User created successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var existingAppUser = _context.AppUsers.FirstOrDefault(x => x.Id == Id);
            return View(existingAppUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, AppUser model)
        {
            try
            {
                var existingAppUser = _context.AppUsers.FirstOrDefault(x => x.Id == Id);
                existingAppUser.FirstName = model.FirstName;
                existingAppUser.LastName = model.LastName;
                existingAppUser.Email = model.Email;
                existingAppUser.Username = model.Username;
                existingAppUser.Password = model.Password;
                existingAppUser.DateModified = DateTime.Now;

                _context.AppUsers.Update(existingAppUser);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View(model);
            }
            TempData["Message"] = "User updated successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            var existingAppUser = _context.AppUsers.FirstOrDefault(x => x.Id == Id);
            return View(existingAppUser);
        }

        [HttpGet]
        public async Task<IActionResult> UnavailableRoles(int Id)
        {
            var unAvailableRoles = await _context.Roles.Where(role => 
            _context.AppUserRoles.
            Where(ur => ur.AppUserID == Id)
            .Select(ar => ar.RoleID)
            .Contains(role.Id))
                .ToListAsync();

            ViewData["UserID"] = Id;
            return PartialView("_UnRoles", unAvailableRoles);
        }

        [HttpGet]
        public async Task<IActionResult> AvailableRoles(int Id)
        {
            var availableRoles = await _context.Roles.Where(role =>
            !_context.AppUserRoles.
            Where(ur => ur.AppUserID == Id)
            .Select(ar => ar.RoleID)
            .Contains(role.Id))
                .ToListAsync();

            ViewData["UserID"] = Id;
            return PartialView("_AvRoles", availableRoles);
        }
    }
}
