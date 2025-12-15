using Microsoft.AspNetCore.Mvc;
using firstprogram.Data;
using firstprogram.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace firstprogram.Controllers
{
    public class RegisterController : Controller
    {
        private readonly AppDbContext _db;

        public RegisterController(AppDbContext db)
        {
            _db = db;
        }

        // GET: Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        public IActionResult Register(Register register)
        {
            if (ModelState.IsValid)
            {
                _db.Registers.Add(register);
                _db.SaveChanges();
                return RedirectToAction("Login","Register");
            }
            return View(register);
        }

        public IActionResult Success()
        {
            return View();
        }

        // GET: Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public IActionResult Login(Register model)
        {
            var user = _db.Registers
                          .FirstOrDefault(x => x.Email == model.Email &&
                                               x.Password == model.Password);

            if (user != null)
            {
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserName", user.FullName);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Invalid Email or Password";
                return View();
            }
        }

        // Logout
        //public IActionResult Logout()
        //{
        //    HttpContext.Session.Clear();
        //    return RedirectToAction("Login");
        //}
    }
}
