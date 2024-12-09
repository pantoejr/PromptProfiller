using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromptProfiller.Data;
using PromptProfiller.Models;

namespace PromptProfiller.Controllers
{
    public class RolesController : Controller
    {
        private readonly AppDbContext _context;
        public RolesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var roles = _context.Roles.ToList();
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Role role)
        {
            if (role == null)
            {
                return View();
            }
            var newRole = new Role()
            {
                Name = role.Name
            };
            _context.Roles.Add(newRole);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var role = _context.Roles.FirstOrDefault(x => x.Id.Equals(Id));
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, Role role)
        {
            try
            {
                var existingRole = _context.Roles.FirstOrDefault(x => x.Id.Equals(Id));
                if (existingRole != null)
                {
                    existingRole.Name = role.Name;
                    _context.Roles.Update(existingRole);
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View(role);
            }

            TempData["Message"] = "Record updated successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            try
            {
                var existingRole = _context.Roles.FirstOrDefault(x=>x.Id==Id);
                _context.Roles.Remove(existingRole);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["Message"] = "Record deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
