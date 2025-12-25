using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using firstProgram.Models;

namespace YourApp.Controllers
{
    public class StudentDataController : Controller
    {
        // memory data
        private static List<StudentData> students = new List<StudentData>();
        private static int _id = 1;

        // read
        public IActionResult Index()
        {
            return View(students);
        }

        // create - get
        public IActionResult Create()
        {
            return View();
        }

        // create - post
        [HttpPost]
        public IActionResult Create(StudentData sd)
        {
            sd.Id = _id++;
            students.Add(sd);
            return RedirectToAction(nameof(Index));
        }

        // edit - get
        public IActionResult Edit(int id)
        {
            var student = students.FirstOrDefault(x => x.Id == id);
            if (student == null)
                return NotFound();

            return View(student);
        }

        // edit - post
        [HttpPost]
        public IActionResult Edit(StudentData sd)
        {
            var old = students.FirstOrDefault(x => x.Id == sd.Id);
            if (old == null)
                return NotFound();

            old.Name = sd.Name;
            old.Age = sd.Age;

            return RedirectToAction(nameof(Index));
        }

        // delete
        public IActionResult Delete(int id)
        {
            var sd = students.FirstOrDefault(x => x.Id == id);
            if (sd == null)
                return NotFound();

            students.Remove(sd);
            return RedirectToAction(nameof(Index));
        }
    }
}
