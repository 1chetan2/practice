using firstprogram.Data;
using firstProgram.Models;
using firstProgram.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace firstProgram.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        // ================= LIST WITH PAGINATION =================
        public IActionResult Index(int page = 1)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            string name = HttpContext.Session.GetString("UserName");

            if (email == null)
            {
                return RedirectToAction("Login", "Register");
            }

            ViewBag.UserEmail = email;
            ViewBag.UserName = name;

            int pageSize = 2;

            var query = _context.Students
                                .AsNoTracking()
                                .OrderBy(s => s.Id);

            int totalCount = query.Count();

            var students = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var model = new PaginatedList<Student>(students,totalCount,page,pageSize);

            return View(model);
        }

        // ================= CREATE =================
        public IActionResult Create()
        {
            return View();
        }

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

        // ================= EDIT =================
        public IActionResult Edit(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
                return NotFound();
            return View(student);
        }

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

        // ================= DELETE =================
        public IActionResult Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
                return NotFound();
            return View(student);
        }

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
