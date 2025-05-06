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

            var labResults = _context.LabOrders.Where(o=>o.Status=="Completed").Include(p=>p.Patient).Include(t=>t.Test).ToList();

            ViewBag.LabWorker = labWorker;

            return View("~/Views/Lab/LabResults.cshtml", labResults);
        }


        public IActionResult ViewAllResults(int OrderId)
        {

            var order= _context.LabOrders
                .Include(l => l.Test)
                .Include(l => l.Patient)
         
                .FirstOrDefault(l => l.Id == OrderId);

            var labResults = _context.LabTestResults
                .Include(t=>t.LabTest)
                .Include(l => l.LapOrder)
                .Where(o=>o.LabOrderId == OrderId)
                .ToList();

            ViewBag.UpdatedDate= order.UpdatedDate;
            ViewBag.PatientName = order.Patient.FullName;
            ViewBag.PatientGender = order.Patient.Gender;
            ViewBag.TestName = order.Test.Name;

            return View("~/Views/Lab/ShowLabResults.cshtml", labResults);

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

        [HttpPost]
        public IActionResult NewCategory(LabTestCategory UpLabTestCategory)
        {
            var cat = new LabTestCategory();
            cat.Name=UpLabTestCategory.Name;
            cat.Description = UpLabTestCategory.Description;

            _context.LabTestCategories.Add(cat);
            _context.SaveChanges();
          
            return RedirectToAction("LabTests");
        }


        [HttpPost]
        public IActionResult NewTest(LabTest upTest)
        {

            var test = new LabTest();
            test.TestName = upTest.TestName;
            test.CategoryId = upTest.CategoryId;
            test.NormalValue = upTest.NormalValue;
            test.MaxRange = upTest.MaxRange;
            test.MinRange = upTest.MinRange;
            test.Unit = upTest.Unit;
            test.sex = upTest.sex;

            _context.LabTests.Add(test);
            _context.SaveChanges();

            return RedirectToAction("LabTestsByCategory", new { catId = upTest.CategoryId });

        }

        [HttpGet]
        public IActionResult GetLabTestsByCategory(int catId)
        {
            var Lab_tests = _context.LabTests.Where(c=> c.CategoryId==catId).ToList();
            return PartialView("~/Views/Lab/_GetLabTests.cshtml",Lab_tests);
        }

        [HttpGet]
        public IActionResult GetLabTestCategory()
        {
            var LabTestCate = _context.LabTestCategories.ToList();
            return PartialView(LabTestCate);
        }


        public IActionResult LabTests()
        {
            return View();
        }

        public IActionResult LabTestsByCategory(int catId)
        {
            var catName = _context.LabTestCategories.FirstOrDefault(c => c.Id == catId);

            ViewBag.CategoryName = catName.Name;
            ViewBag.CategoryId = catId;
            return View();
        }




        public IActionResult CreateLabOrder(int DoctorId, int PatientId)
        {

            var tests = new SelectList(_context.LabTestCategories, "Id", "Name");

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
                LabTestCategoryId = Order.LabTestCategoryId,
                PatientId = Order.PatientId,
                DoctorId = Order.DoctorId,
                Status = "Pending",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _context.LabOrders.Add(NewOrder);
            _context.SaveChanges();

            return RedirectToAction("LabOrdersForOnePatient", "Doctor", new { PatientId = Order.PatientId});
        }


        public IActionResult PreformTests(int id)
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

            var patient_sex = order.Patient.Gender;

         

            var requiredTests = _context.LabTests
                .Where(t => t.CategoryId == order.LabTestCategoryId && (t.sex==patient_sex||t.sex=="Both"))
                .ToList();

            var viewModel = new LabTestResultViewModel
            {
                OrderId = order.Id,
                Results = requiredTests.Select(t => new LabTestResult
                {
                    LabTestId = t.Id,
                    LabTest = t
                    
                }).ToList()
            };

            ViewBag.PatientName = order.Patient.FullName;
            ViewBag.PatientGender= order.Patient.Gender;
            ViewBag.TestName = order.Test.Name;
            return View(viewModel);
        }



        [HttpPost]
        public IActionResult SendResult(LabTestResultViewModel model)
        {
            foreach (var result in model.Results)
            {
                result.LabOrderId = model.OrderId;

                var labTest = _context.LabTests.FirstOrDefault(t => t.Id == result.LabTestId);
              
                double Dresult = double.Parse(result.Result);

                Console.WriteLine(result.Result);

                result.Result = result.Result;

                if (Dresult < labTest.MinRange)
                {
                    result.Remark = "Low";
                }
                else if (Dresult > labTest.MaxRange)
                {
                    result.Remark = "High";
                }
                else
                {
                    result.Remark = "Normal";
                }


                _context.LabTestResults.Add(result);
            }

            var order = _context.LabOrders.FirstOrDefault(l => l.Id == model.OrderId);

           

            order.Status = "Completed";
            order.UpdatedDate = DateTime.Now;
            _context.LabOrders.Update(order);

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public IActionResult SendResult(LabTestResult UpResult)
        //{
            
        //    var result = new LabTestResult()
        //    {
        //        LabOrderId = UpResult.LabOrderId,
        //        LabTestId = UpResult.LabTestId,
        //        Result = UpResult.Result,
        //        Remark = UpResult.Remark,
        //        Interpretation = UpResult.Interpretation,
             
        //    };
        //    var order = _context.LabOrders.FirstOrDefault(l => l.Id == UpResult.LabOrderId);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }
        //    order.Status = "Completed";
        //    order.UpdatedDate = DateTime.Now;
            


        //    _context.LabTestResults.Add(result);
        //    _context.SaveChanges();
        //    return RedirectToAction("LabOrders");
            
          

        //}


    }
}
