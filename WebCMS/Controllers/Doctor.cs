using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using WebCMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebCMS.Controllers
{
    [Authorize]
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DoctorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var doctor = _context.Doctors.FirstOrDefault(d => d.UserId == userId);

            if (doctor == null)
            {
                return NotFound("Doctor not found.");
            }

            var appointments = _context.Appointments
             .Include(a => a.Patient) 

             .Where(a => a.DoctorId == doctor.Id)
             .OrderByDescending(a => a.CreatedDate)
             .ToList();

          

            ViewBag.Doctor = doctor;
            return View(appointments);
        }


        public IActionResult Cancel(int id)
        {
            var appointment = _context.Appointments.FirstOrDefault(a => a.Id == id);
            if (appointment == null)
            {
                return NotFound(); 
            }
            appointment.IsCancelled = true;
            appointment.UpdatedDate = DateTime.Now;
            appointment.Status = "Cancelled";
            _context.SaveChanges();



            return RedirectToAction("Index");
        }

        public IActionResult UpdateProf()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var doctor = _context.Doctors.FirstOrDefault(d => d.UserId == userId);

            if (doctor == null)
            {
                return NotFound("Doctor not found.");
            }

            return View(doctor);
        }

        [HttpPost]
        public IActionResult UpdateProf(Doctor upDoctor)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var doctor = _context.Doctors.FirstOrDefault(d => d.UserId == userId);

            if (doctor == null)
            {
                return NotFound("Doctor not found.");
            }

            doctor.FullName = upDoctor.FullName;
            doctor.Specialization = upDoctor.Specialization;
           

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult DiseaseView(int id, int doctorId)
        {
            var patientDiseases = _context.PatiensDiseases
                .Where(p => p.PatientId == id)
                .ToList();

         

            ViewBag.PatientId = id;
            ViewBag.doctorId = doctorId;

            return View("~/Views/Doctor/DiseaseView.cshtml", patientDiseases);
        }



        [HttpGet]
        public IActionResult SearchDiseases(string term, int page = 1)
        {
            const int pageSize = 50;

            var loweredTerm = term?.ToLower();

            var query = _context.Diseases
                .Where(d => string.IsNullOrEmpty(loweredTerm) || d.DiseaseName.ToLower().StartsWith(loweredTerm))
                .OrderBy(d => d.DiseaseName)
                .Select(d => new { id = d.Id, text = d.DiseaseName });

            var totalCount = query.Count();

            var diseases = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Json(new
            {
                results = diseases,
                pagination = new { more = (page * pageSize) < totalCount }
            });
        }


        [HttpPost]
        public IActionResult AddDiagnoseToPatient(int patientId, int diseaseId,int doctorId)
        {
            
            var doctorName= _context.Doctors
                .Where(d => d.Id == doctorId)
                .Select(d => d.FullName)
                .FirstOrDefault();
            var exists = _context.PatiensDiseases
                .Any(p => p.PatientId == patientId && p.DiseaseId == diseaseId);

            var diseaseName= _context.Diseases
                .Where(d => d.Id == diseaseId)
                .Select(d => d.DiseaseName)
                .FirstOrDefault();
            var icd10Code = _context.Diseases.Where(d => d.Id == diseaseId)
                .Select(d => d.ICD10Code)
                .FirstOrDefault();

            if (!exists)
            {
                _context.PatiensDiseases.Add(new PatiensDiseases
                {
                    PatientId = patientId,
                    DiseaseId = diseaseId,
                    DiseaseName = diseaseName,
                    ICD10Code = icd10Code,
                    DoctorId=doctorId,
                    DiagnosedbyDr = doctorName,
                    DiagnosedDate = DateTime.Now
                  
                });

                _context.SaveChanges();
            }

            return RedirectToAction("DiseaseView", new { id = patientId , doctorId = doctorId});
        }


        public IActionResult View(int id)
        {
            var appintment = _context.Appointments
                .Include(a => a.Patient) 
                .Include(a => a.Doctor)
                .FirstOrDefault(a => a.Id == id);

            if (appintment == null)
            {
                return NotFound();
            }

            return View(appintment);


        }

        public async Task<IActionResult> Logout()
        {

            return RedirectToAction("Logout", "Account"); 
        }


        public IActionResult Complete(int id) { 
            var appointment = _context.Appointments
                .Include(a => a.Patient) 
                .Include(a => a.Doctor)
                .FirstOrDefault(a => a.Id == id);

            if (appointment == null)
            {
                return NotFound();
            }

            appointment.Status = "Completed";
            appointment.UpdatedDate = DateTime.Now;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Prescriptions(int id)
        {
            var prescriptions = _context.Prescriptions
                .Where(a => a.AppointmentId == id)
                .ToList();
            var appoitment = _context.Appointments
                 
                .FirstOrDefault(a => a.Id == id);

            ViewBag.appoitment = appoitment;

            return View(prescriptions);
        }


        public IActionResult CreatePrescription(int id)
        {
            var appoitment = _context.Appointments.Include(d=> d.Doctor).Include(p => p.Patient)
              
                .FirstOrDefault(a => a.Id == id);

            

            var diseases = _context.PatiensDiseases
                .Where(p => p.PatientId == appoitment.PatientId);



            ViewBag.PatientDiseases = new SelectList(diseases, "DiseaseName", "DiseaseName");
            ViewBag.appoitment = appoitment;


            return View();
        }


        [HttpPost]
        public IActionResult CreatePrescription([Bind("DoctorId,PatientId,AppointmentId,MedicationName,Quantity,ForDiseases,Status,Dosage,Frequency,Duration,Notes,CreatedDate")] Prescription prescription)
        {
           
               
            _context.Prescriptions.Add(prescription);
            _context.SaveChanges();
          
      
            ViewBag.appoitment = _context.Appointments
                .FirstOrDefault(a => a.Id == prescription.AppointmentId);

            return RedirectToAction("Prescriptions", new { id = prescription.AppointmentId });

        }


        public IActionResult EMR()
        {
            return View();
        }

        public IActionResult RadiologyReports() {
            return View();
        }


        public IActionResult LabOrdersForOnePatient(int PatientId) {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var doctor = _context.Doctors.FirstOrDefault(d => d.UserId == userId);

            var patient = _context.Patients.FirstOrDefault(p => p.Id == PatientId);

            var orders = _context.LabOrders.Where(i => i.PatientId == PatientId).Include(t=> t.Test).ToList();

            ViewBag.PatientId = patient.Id;
            ViewBag.DoctorId = doctor.Id;
            ViewBag.Patient = patient;

            return View(orders);
        }

        public IActionResult ViewLabResult(int id)
        {


            var labResult = _context.LabTestResults
                .Include(l => l.LabTest).Include(o=> o.LapOrder)
                .FirstOrDefault(l => l.LabOrderId == id);

 

            if (labResult == null)
            {
                return NotFound();
            }

            return View(labResult);
        }

    }
}
