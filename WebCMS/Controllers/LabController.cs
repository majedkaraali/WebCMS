using Microsoft.AspNetCore.Mvc;
using WebCMS.Models;
using System.Security.Claims;
namespace WebCMS.Controllers
{
    public class LabController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LabController (ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var doctor = _context.Doctors.FirstOrDefault(d=> d.UserId==userId);

            if (doctor == null)
            {
                return NotFound("Doctor not found.");
            }


            var LabResults = _context.LabTestResults.ToList();



            ViewBag.Doctor= doctor;

            return View(LabResults);
        }
    }
}
