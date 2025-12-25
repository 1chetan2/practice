using firstprogram.Data;
using firstProgram.Models;
using firstProgram.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace firstProgram.Controllers
{
    public class FeesDetailController : Controller
    {
        private readonly AppDbContext _context;

        public FeesDetailController(AppDbContext context)
        {
            _context = context;
        }

        //display page
        public IActionResult Index()
        {
            var data = _context.FeesDetails
                .Select(f => new FeesDetailDto
                {
                    Id = f.Id,
                    Name = f.Name,
                    Amount = f.Amount
                })
                .ToList();

            //return Ok(data);
            return View(data);
            
        }

        // get Create
        public IActionResult Create()
        {
            return View();
        }

        // post Create Form
        [HttpPost]
        public IActionResult Create(FeesDetailDto dto)
        {
            if (ModelState.IsValid)
            {
                var fees = new FeesDetail
                {
                    Name = dto.Name,
                    Amount = dto.Amount,
                    Gst = ""  
                };

                _context.FeesDetails.Add(fees);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }
    }
}
