using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using WebCMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace WebCMS.Controllers
{
    [Authorize] // Ensures only logged-in users can access doctor features
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DoctorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Get the logged-in doctor's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var doctor = _context.Doctors.FirstOrDefault(d => d.UserId == userId);

            if (doctor == null)
            {
                return NotFound("Doctor not found.");
            }

            // Fetch upcoming appointments for the doctor
            var appointments = _context.Appointments
             .Include(a => a.Patient) // Include Patient details

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
                return NotFound(); // Handle case where appointment doesn't exist
            }
            appointment.IsCancelled = true;
            appointment.UpdatedDate = DateTime.Now;
            appointment.Status = "Cancelled";
            _context.SaveChanges();



            return RedirectToAction("Index");
        }

        public IActionResult Profile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var doctor = _context.Doctors.FirstOrDefault(d => d.UserId == userId);

            if (doctor == null)
            {
                return NotFound("Doctor not found.");
            }

            return View(doctor);
        }


        public IActionResult View(int id)
        {
            var appintment = _context.Appointments
                .Include(a => a.Patient) // Ensure you include related data  
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
            var appoitment = _context.Appointments
                .FirstOrDefault(a => a.Id == id);

            ViewBag.appoitment = appoitment;


            return View();
        }


        [HttpPost]
        public IActionResult CreatePrescription([Bind("AppointmentId,MedicationName,Dosage,Frequency,Duration,Notes,CreatedDate")] Prescription prescription)
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

        public IActionResult LabResults() {
            return View();
        }

    }
}
