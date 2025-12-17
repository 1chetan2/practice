using firstprogram.Data;
using firstProgram.Models;
using Microsoft.AspNetCore.Mvc;

namespace firstProgram.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        // READ (List)
        public IActionResult Index()
        {
            string email = HttpContext.Session.GetString("UserEmail");
            string name = HttpContext.Session.GetString("UserName");
            if (email == null)
            {
                return RedirectToAction("Login", "Register");
            }

            ViewBag.UserEmail = email;
            ViewBag.UserName = name;

            var students = _context.Students.ToList();
            return View(students);
        }

        // CREATE - GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE - POST
        [HttpPost]
    
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // EDIT - GET
        public IActionResult Edit(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null) 
                return NotFound();
            return View(student);
        }

        // EDIT - POST
        [HttpPost]
       
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Update(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // DELETE - GET
        public IActionResult Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
                return NotFound();
            return View(student);
        }

        // DELETE - POST
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _context.Students.Find(id);
            _context.Students.Remove(student);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
