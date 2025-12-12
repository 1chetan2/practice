using firstprogram.Data;
using firstprogram.Models;
using Microsoft.AspNetCore.Mvc;

namespace firstprogram.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly AppDbContext _db;

        public FeedbackController(AppDbContext db)
        {
            _db = db;
        }

        // GET: /Feedback/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Feedback/Create
        [HttpPost]
        public IActionResult Create(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                _db.Feedbacks.Add(feedback);
                _db.SaveChanges();
                return RedirectToAction("Success");
            }

            return View(feedback);
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
