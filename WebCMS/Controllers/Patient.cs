using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebCMS.Models;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace WebCMS.Controllers
{
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = _context.Patients.FirstOrDefault(p => p.UserId == userId);

            if (patient == null)
            {
                return NotFound("Patient not found.");
            }


            if (patient.updated == false)
            {
                return RedirectToAction("Profile");
            }

            var appointments = _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Where(a => a.PatientId == patient.Id)
                .OrderBy(a => a.AppointmentDate)
                .ToList();

            ViewBag.Patient = patient;

            return View(appointments);
        }
     
        public IActionResult Book()
        {
            ViewBag.Doctors = new SelectList(_context.Doctors, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public IActionResult Book(Appointment appointment)
        {
         

            var patientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (patientId == null)
            {
                return Unauthorized(); 
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = _context.Patients.FirstOrDefault(p => p.UserId == userId);

            if (patient == null)
            {
                return BadRequest("Patient not found.");
            }

            appointment.PatientId = patient.Id;

            appointment.Status = "Scheduled"; 

           
            var isDoctorAvailable = !_context.Appointments
                .Any(a => a.DoctorId == appointment.DoctorId &&
                          a.AppointmentDate == appointment.AppointmentDate);

            if (!isDoctorAvailable)
            {
                ModelState.AddModelError("", "The selected doctor is not available at this time.");
                ViewBag.Doctors = new SelectList(_context.Doctors, "Id", "FullName");
                return View(appointment);
            }

       
            _context.Appointments.Add(appointment);
            _context.SaveChanges();


            TempData["SuccessMessage"] = "Appointment booked successfully!";
            return RedirectToAction("Index");
        }

    
        public IActionResult Profile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = _context.Patients.FirstOrDefault(p => p.UserId == userId);

            if (patient == null)
            {
                return BadRequest("Patient not found.");
            }

            ViewBag.Allergies = _context.Allergies.ToList();




            return View(patient);

    
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


      

        [HttpPost]
        public IActionResult UpdateProfile(Patient updatedPatient)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = _context.Patients.FirstOrDefault(p => p.UserId == userId);

            if (patient == null)
            {
                return BadRequest("Patient not found.");
            }

            patient.FullName = updatedPatient.FullName;
            patient.Email = updatedPatient.Email;
            patient.Address = updatedPatient.Address;
            patient.PhoneNumber = updatedPatient.PhoneNumber;
            patient.DateOfBirth = updatedPatient.DateOfBirth;
            patient.Gender = updatedPatient.Gender;
            patient.EmergencyContactName = updatedPatient.EmergencyContactName;
            patient.EmergencyContactPhone = updatedPatient.EmergencyContactPhone;
            patient.InsuranceProvider = updatedPatient.InsuranceProvider;
            patient.PolicyNumber = updatedPatient.PolicyNumber;
            patient.Occupation = updatedPatient.Occupation;
            patient.Smoking = updatedPatient.Smoking;
            patient.Alcohol = updatedPatient.Alcohol;
            patient.SporExercise = updatedPatient.SporExercise;
            patient.SelectedAllergyIds = updatedPatient.SelectedAllergyIds;
            patient.UserHeight = updatedPatient.UserHeight;
            patient.Weight = updatedPatient.Weight;
            patient.BloodType = updatedPatient.BloodType;
            patient.CompanyName = updatedPatient.CompanyName;
            patient.SocialSecurityNumber = updatedPatient.SocialSecurityNumber;
            patient.updated = true;

            if (updatedPatient.SelectedAllergyIds != null) { 

                foreach (var allergy in updatedPatient.SelectedAllergyIds)
                {
                   var newAllergy = new PatientAllergy
                    {
                        PatientId = patient.Id,
                        AllergyId = allergy,
                        Notes = "Allergy noted"
                    };

                    _context.PatientAllergies.Add(newAllergy);
                }
            }



            _context.SaveChanges(); 

            return RedirectToAction("Index");   
        }



        public async Task<IActionResult> Logout()
        {

            return RedirectToAction("Logout", "Account"); // Redirect to login page after logout
        }


    }
}
