using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCMS.Models;

namespace WebCMS.Controllers
{
    public class EMRController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EMRController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index(int patientId, int doctorId)
        {



            ViewBag.PatientId = patientId;
            ViewBag.DoctorId = doctorId;
            return View();
        }

        public IActionResult PatientHeader(int id)
        {
            Console.WriteLine(id);
            var patient = _context.Patients.Include(a => a.Appointments).Include(u => u.User).FirstOrDefault(p => p.Id == id);
            var lastVisit = _context.Appointments
                .Where(a => a.PatientId == id)
                .Include(d => d.Doctor)
                .OrderByDescending(a => a.AppointmentDate)
                .FirstOrDefault();

            ViewBag.LastVisit = lastVisit?.AppointmentDate.ToString("dd/MM/yyyy");
            ViewBag.VisitPrimaryPhysician = lastVisit?.Doctor?.FullName;

            return PartialView("~/Views/EMR/_PatientHeader.cshtml", patient);

        }

        public IActionResult QuickStatus(int id)
        {
            var patient = _context.Patients.Include(a => a.Appointments).Include(u => u.User).FirstOrDefault(p => p.Id == id);

            var height = patient.UserHeight;
            var weight = patient.Weight;


            double heightInMetersDouble = Convert.ToDouble(height);
            double weightDouble = Convert.ToDouble(weight);

            heightInMetersDouble = heightInMetersDouble / 100;

            var bmi = weightDouble / (heightInMetersDouble * heightInMetersDouble);

            ViewBag.BMI = bmi;
            return PartialView("~/Views/EMR/_QuickStatus.cshtml", patient);

        }

        public IActionResult UpcomingAppoitments(int id)
        {
            var appointments = _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Where(a => a.PatientId == id && a.AppointmentDate > DateTime.Now)
                .OrderBy(a => a.AppointmentDate)
                .ToList();

            return PartialView("~/Views/EMR/_UpcomingAppoitments.cshtml", appointments);

        }

        public IActionResult CurrentMedications(int id)
        {
            var medications = _context.Prescriptions
                .Include(a => a.Appointment)
                .Include(a => a.doctor)
                .Include(a => a.patient)
                .Where(a => a.PatientId == id)
                .OrderByDescending(a => a.CreatedDate)
                .ToList();


            return PartialView("~/Views/EMR/_CurrentMedications.cshtml", medications);
        }

        public IActionResult RecentActivity(int id)
        {

            return PartialView("~/Views/EMR/_RecentActivity.cshtml");



        }


        public IActionResult AppoitmentRecords(int id)
        {
            var appoitments = _context.Appointments.Where(a => a.PatientId == id).Include(d=>d.Doctor).ToList();


            return PartialView("~/Views/EMR/_AppoitmentRecords.cshtml",appoitments);

        }

        public IActionResult PastIllnesses(int id)
        {
            var diseases = _context.PatiensDiseases
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a => a.PatientId ==id)
                .OrderByDescending(a => a.DiagnosedDate)
                .ToList();

            return PartialView("~/Views/EMR/_PastIllnesses.cshtml", diseases);
        }


        public IActionResult Allergies(int id)
        {
           
            var allergies = _context.PatientAllergies
                .Include(a => a.Patient)
                .Include(a => a.Allergy)
                .Where(a => a.PatientId == id)
                .OrderByDescending(a => a.DateDiagnosed)
                .ToList();

            
            var FoodAllergiesCount= allergies.Count(allergies => allergies.Allergy.AllergyType == "Food");
            var DrugAllergiesCount = allergies.Count(allergies => allergies.Allergy.AllergyType == "Drug");
            var EnvironmentalAllergiesCount = allergies.Count(allergies => allergies.Allergy.AllergyType == "Environmental");
            var InsectAllergiesCount = allergies.Count(allergies => allergies.Allergy.AllergyType == "Insect");
            var MaterialAllergiesCount = allergies.Count(allergies => allergies.Allergy.AllergyType == "Material");

            ViewBag.FoodAllergiesCount = FoodAllergiesCount;
            ViewBag.DrugAllergiesCount = DrugAllergiesCount;
            ViewBag.EnvironmentalAllergiesCount = EnvironmentalAllergiesCount;
            ViewBag.InsectAllergiesCount = InsectAllergiesCount;
            ViewBag.MaterialAllergiesCount = MaterialAllergiesCount;
            ViewBag.TotalAllergiesCount = allergies.Count();

            return PartialView("~/Views/EMR/_Allergies.cshtml",allergies);
        }


        public IActionResult SocialHistory(int id)
        {
            var patient= _context.Patients.FirstOrDefault(p=> p.Id == id);
            return PartialView("~/Views/EMR/_SocialHistory.cshtml",patient);
        }


        public IActionResult ActiveMedications(int id)
        {
            var medications = _context.Prescriptions.Include(d=>d.doctor).Where(a => a.PatientId == id && a.Status=="Active")
                .OrderByDescending(a => a.CreatedDate).ToList();

            return PartialView("~/Views/EMR/_ActiveMedications.cshtml",medications);
        }

        public IActionResult MedicationHistory(int id)
        {
            var medications = _context.Prescriptions
             .Include(d => d.doctor)
             .Where(p => p.PatientId == id && (p.Status == "Discontinued" || p.Status == "Completed")).ToList();

            return PartialView("~/Views/EMR/_MedicationHistory.cshtml", medications);

        }


    }


}
