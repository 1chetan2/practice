using Microsoft.AspNetCore.Mvc;
using firstprogram.Data;
using firstprogram.Models;

namespace firstprogram.Controllers
{
    public class RegisterController : Controller
    {
        private readonly AppDbContext _db;

        public RegisterController(AppDbContext db)
        {
            _db = db;
        }

        // GET: /Register/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Register/Register
        [HttpPost]
        public IActionResult Register(Register register)
        {
            if (ModelState.IsValid)
            {
                _db.Registers.Add(register);  // Use DbSet for Register model
                _db.SaveChanges();
                return RedirectToAction("Success");
            }
            return View(register);
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
