using Microsoft.AspNetCore.Mvc;
using WebCMS.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace WebCMS.Controllers
{
    public class LabController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LabController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var labWorker = _context.LabUsers.FirstOrDefault(l => l.UserId == userId);

            if (labWorker == null)
            {
                return NotFound("Lab worker not found.");
            }

            ViewBag.LabWorker = labWorker;

            return View();
        }

        public IActionResult LabResults()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var labWorker = _context.LabUsers.FirstOrDefault(l => l.UserId == userId);

            if (labWorker == null)
            {
                return NotFound("Lab worker not found.");
            }

            var labResults = _context.LabTestResults.ToList();

            ViewBag.LabWorker = labWorker;

            return View(labResults);
        }

        public IActionResult LabOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var labWorker = _context.LabUsers.FirstOrDefault(l => l.UserId == userId);

            if (labWorker == null)
            {
                return NotFound("Lab worker not found.");
            }

            var labOrder = _context.LabOrders
                        .Include(lo => lo.Patient).Include(d=> d.Doctor).Include(l=> l.Test).ToList();
                       



            return View(labOrder);
        }

        public IActionResult LabTests()
        {
            var abTests = _context.LabTests.ToList();
            return View(abTests);
        }



        public IActionResult NewTest()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewTest(LabTest upTest)
        {
            if (ModelState.IsValid)
            {
                _context.LabTests.Add(upTest);
                _context.SaveChanges();
                return RedirectToAction("LabTests");
            }

            return View(upTest);

        }


        public IActionResult CreateLabOrder(int DoctorId, int PatientId)
        {

            var tests = new SelectList(_context.LabTests, "Id", "TestName");

            ViewBag.Tests = tests;
            ViewBag.PatientId = PatientId;
            ViewBag.DoctorId = DoctorId;
            return View();
        }


        [HttpPost]
        public IActionResult CreateLabOrder(LabOrder Order)

        {
            var NewOrder = new LabOrder()
            {
                LabTestId = Order.LabTestId,
                PatientId =Order.PatientId,
                DoctorId = Order.DoctorId,
                Status = "Pending",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _context.LabOrders.Add(NewOrder);
            _context.SaveChanges();

            return RedirectToAction("Index","Doctor");
        }


        public IActionResult PreformTest(int id)
        {
            var order = _context.LabOrders
                .Include(l => l.Test)
                .Include(l => l.Patient)
                .Include(l => l.Doctor)
                .FirstOrDefault(l => l.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            ViewBag.Order = order;
            var Result = new LabTestResult
            {
                LabOrderId = order.Id,
                LabTestId = order.Test.Id
            };

            return View(Result);
        }

        [HttpPost]
        public IActionResult SendResult(LabTestResult UpResult)
        {
            
            var result = new LabTestResult()
            {
                LabOrderId = UpResult.LabOrderId,
                LabTestId = UpResult.LabTestId,
                Result = UpResult.Result,
                Remark = UpResult.Remark,
                Interpretation = UpResult.Interpretation,
             
            };
            var order = _context.LabOrders.FirstOrDefault(l => l.Id == UpResult.LabOrderId);
            if (order == null)
            {
                return NotFound();
            }
            order.Status = "Completed";
            order.UpdatedDate = DateTime.Now;
            


            _context.LabTestResults.Add(result);
            _context.SaveChanges();
            return RedirectToAction("LabOrders");
            
          

        }


    }
}
