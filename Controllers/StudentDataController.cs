
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using firstProgram.Models;

namespace YourApp.Controllers
{
    public class StudentDataController : Controller
    {
        private readonly ILogger<StudentDataController> _logger;

        // memory data
        private static List<StudentData> students = new List<StudentData>();
        private static int _id = 1;

        public StudentDataController(ILogger<StudentDataController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Running Index Method : student list");
            return View(students);
        }

        public IActionResult Create()
        {
            _logger.LogInformation("Opening Create page");
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentData sd)
        {
            _logger.LogInformation("Creating new student: {Name}", sd.Name);

            sd.Id = _id++;
            students.Add(sd);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            _logger.LogInformation("Open Edit Page");
            _logger.LogInformation("Editing student ID is : {Id}", id);

            var student = students.FirstOrDefault(x => x.Id == id);
            if (student == null)
            {
                _logger.LogWarning("Student not found ID : {Id}", id);
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(StudentData sd)
        {
            var old = students.FirstOrDefault(x => x.Id == sd.Id);
            if (old == null)
            {
                _logger.LogWarning("Student not found for update: ID {Id}", sd.Id);
                return NotFound();
            }

            old.Name = sd.Name;
            old.Age = sd.Age;

            _logger.LogInformation("Student updated   ID  :{Id}", sd.Id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var sd = students.FirstOrDefault(x => x.Id == id);
            if (sd == null)
            {
                _logger.LogWarning("Student not found for delete: ID {Id}", id);
                return NotFound();
            }

            students.Remove(sd);
            _logger.LogInformation("Student deleted: ID {Id}", id);

            return RedirectToAction(nameof(Index));
        }
    }
}




































//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Linq;
//using firstProgram.Models;

//namespace YourApp.Controllers
//{
//    public class StudentDataController : Controller
//    {
//        // memory data
//        private static List<StudentData> students = new List<StudentData>();
//        private static int _id = 1;

//        // read
//        public IActionResult Index()
//        {
//            return View(students);
//        }

//        // create - get
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // create - post
//        [HttpPost]
//        public IActionResult Create(StudentData sd)
//        {
//            sd.Id = _id++;
//            students.Add(sd);
//            return RedirectToAction(nameof(Index));
//        }

//        // edit - get
//        public IActionResult Edit(int id)
//        {
//            var student = students.FirstOrDefault(x => x.Id == id);
//            if (student == null)
//                return NotFound();

//            return View(student);
//        }

//        // edit - post
//        [HttpPost]
//        public IActionResult Edit(StudentData sd)
//        {
//            var old = students.FirstOrDefault(x => x.Id == sd.Id);
//            if (old == null)
//                return NotFound();

//            old.Name = sd.Name;
//            old.Age = sd.Age;

//            return RedirectToAction(nameof(Index));
//        }

//        // delete
//        public IActionResult Delete(int id)
//        {
//            var sd = students.FirstOrDefault(x => x.Id == id);
//            if (sd == null)
//                return NotFound();

//            students.Remove(sd);
//            return RedirectToAction(nameof(Index));
//        }
//    }
//}