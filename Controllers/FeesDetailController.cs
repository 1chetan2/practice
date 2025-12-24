using firstprogram.Data;
using firstProgram.Models;
using firstProgram.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace firstProgram.Controllers
{
    [Route("FeesDetailRoute")]
    public class FeesDetailController : Controller
    {
        private readonly AppDbContext _db;

        public FeesDetailController(AppDbContext db)
        {
            _db = db;
        }

        // GET: /FeesDetail
        [Route("Index")]
        public IActionResult Index()
        {
            var list = _db.FeesDetails
                .Select(f => new FeesDetailDto
                {
                    Id = f.Id,
                    Name = f.Name,
                    Amount = f.Amount
                })
                .ToList();

            return View(list);
        }

        [Route("")]
        [Route("Details")]
        public IActionResult Details()
        {
            return View();

        }
        // GET: /FeesDetail/Details/5
        [Route("Details/{id}")]
        public IActionResult Details(int id)
        {
            var fdetail = _db.FeesDetails
                .Where(f => f.Id == id)
                .Select(f => new FeesDetailDto
                {
                    Id = f.Id,
                    Name = f.Name,
                    Amount = f.Amount
                })
                .FirstOrDefault();

            if (fdetail == null)
            {
                return NotFound();
            }

            return View(fdetail);
        }
    }
}
