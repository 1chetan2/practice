using firstprogram.Data;
using Microsoft.AspNetCore.Mvc;

namespace firstProgram.Controllers
{
    public class StudentController1 : Controller
    {
        private readonly AppDbContext _context;

        public StudentController1(AppDbContext context)
        {
            _context = context;
        }

        // READ
        public IActionResult Index()
        {
            var student = _context.Students.ToList();
            return View();
        }
        
    }
}
