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

        public IActionResult Index( int page = 1, int pageSize = 4, string searchString = "", string sortColumn = "Id",string sortOrder = "asc")
        {
            // session
            if (HttpContext.Session.GetString("UserEmail") == null)
                return RedirectToAction("Login", "Register");

            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            // data pass on view
            ViewBag.CurrentFilter = searchString;
            ViewBag.SortColumn = sortColumn;
            ViewBag.SortOrder = sortOrder;

            IQueryable<Student> query = _context.Students.AsNoTracking();

            // search
            if (!string.IsNullOrEmpty(searchString))
            {
                    query = query.Where(s =>
                    s.StudentName.Contains(searchString) || s.Email.Contains(searchString) || s.Course.Contains(searchString));
            }

            // sorting
            switch (sortColumn)
            {
                case "StudentName":  
                    query = sortOrder == "asc" ?
                         query.OrderBy(s => s.StudentName) : query.OrderByDescending(s => s.StudentName);
                    break;

                case "Email":  
                    query = sortOrder == "asc" ?
                         query.OrderBy(s => s.Email) : query.OrderByDescending(s => s.Email);
                    break;
                      
                case "Course":  
                    query = sortOrder == "asc" ?
                         query.OrderBy(s => s.Course)  :  query.OrderByDescending(s => s.Course);
                    break;  

                case "EnrollmentDate":
                    query = sortOrder == "asc" ?
                         query.OrderBy(s => s.EnrollmentDate)  :  query.OrderByDescending(s => s.EnrollmentDate);
                    break;

                default:   
                    query = sortOrder == "asc" ? 
                         query.OrderBy(s => s.Id)  :  query.OrderByDescending(s => s.Id);
                    break;

            }

            // pagination
              
            int totalCount = query.Count();
                                                        
            var students = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                                                                                            
            var model = new PaginatedList<Student>(students,totalCount,page,pageSize,sortColumn,sortOrder);    
                                           
            return View(model);

        }
        // for lazy loading
        public IActionResult LoadMore(
            int page=1,
            int pageSize=4,
            string searchString = "",
            string sortColumn = "Id",
            string sortOrder = "asc")
        {
            IQueryable<Student> query = _context.Students.AsNoTracking();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(s =>
                    s.StudentName.Contains(searchString) ||
                    s.Email.Contains(searchString) ||
                    s.Course.Contains(searchString));
            }

            query = sortColumn switch
            {
                "StudentName" => sortOrder == "asc"
                    ? query.OrderBy(s => s.StudentName)
                    : query.OrderByDescending(s => s.StudentName),

                "Email" => sortOrder == "asc"
                    ? query.OrderBy(s => s.Email)
                    : query.OrderByDescending(s => s.Email),

                "Course" => sortOrder == "asc"
                    ? query.OrderBy(s => s.Course)
                    : query.OrderByDescending(s => s.Course),

                "EnrollmentDate" => sortOrder == "asc"
                    ? query.OrderBy(s => s.EnrollmentDate)
                    : query.OrderByDescending(s => s.EnrollmentDate),

                _ => sortOrder == "asc"
                    ? query.OrderBy(s => s.Id)
                    : query.OrderByDescending(s => s.Id)
            };

            var students = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return PartialView("_StudentRows", students); 
        }

        //************************** crud ********************************

        //insert
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

        //update
        public IActionResult Edit(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null) return NotFound();
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

        //delete
        public IActionResult Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null) return NotFound();
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
